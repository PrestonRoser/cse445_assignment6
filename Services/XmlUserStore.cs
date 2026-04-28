using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace CrimeRiskWeb.Services
{
    // Handles all direct reads and writes to Users.xml and Staff.xml.
    // No business logic lives here — just the XML plumbing.
    public static class XmlUserStore
    {
        private const string UsersPath = "~/App_Data/Users.xml";
        private const string StaffPath = "~/App_Data/Staff.xml";

        // Prevents race conditions on concurrent XML writes
        private static readonly object _fileLock = new object();

        // Called once in Application_Start; guarantees both files exist before any request.
        public static void EnsureFilesExist()
        {
            string usersFile = MapPath(UsersPath);
            string staffFile = MapPath(StaffPath);

            if (!File.Exists(usersFile))
            {
                // Create Users.xml with an empty root element
                new XDocument(new XElement("Users")).Save(usersFile);
            }

            if (!File.Exists(staffFile))
            {
                // Seed the TA credential if Staff.xml doesn't exist yet.
                // teammates: replace this SHA-256 stand-in with the hashing DLL once it's integrated.
                string taHash = Sha256Hash("Cse445!");
                XDocument doc = new XDocument(
                    new XElement("Users",
                        BuildUserElement("TA", taHash, "Staff", "ta@asu.edu")
                    )
                );
                doc.Save(staffFile);
            }
        }

        // Returns all <User> elements from a given file path.
        public static XElement[] LoadUsers(string filePath)
        {
            try
            {
                XDocument doc = XDocument.Load(filePath);
                return doc.Root.Elements("User").ToArray();
            }
            catch
            {
                return new XElement[0];
            }
        }

        // Finds a single user by username in a given file; returns null if not found.
        public static XElement FindUser(string filePath, string username)
        {
            try
            {
                XDocument doc = XDocument.Load(filePath);
                return doc.Root
                    .Elements("User")
                    .FirstOrDefault(u => string.Equals(
                        (string)u.Element("Username"),
                        username,
                        StringComparison.OrdinalIgnoreCase));
            }
            catch
            {
                return null;
            }
        }

        // Appends a new user record to Users.xml.
        public static void AddUser(string username, string passwordHash, string role, string email)
        {
            string filePath = MapPath(UsersPath);
            lock (_fileLock)
            {
                XDocument doc = XDocument.Load(filePath);
                doc.Root.Add(BuildUserElement(username, passwordHash, role, email));
                doc.Save(filePath);
            }
        }

        // Updates password for member accounts only.
        // Staff accounts are managed separately and are not modified here.
        public static void UpdatePassword(string username, string newHash)
        {
            string filePath = MapPath(UsersPath);
            lock (_fileLock)
            {
                XDocument doc = XDocument.Load(filePath);
                XElement user = doc.Root
                    .Elements("User")
                    .FirstOrDefault(u => string.Equals(
                        (string)u.Element("Username"),
                        username,
                        StringComparison.OrdinalIgnoreCase));

                if (user != null)
                {
                    user.Element("PasswordHash").Value = newHash;
                    doc.Save(filePath);
                }
            }
        }

        // Returns true if the username already exists in either Users.xml or Staff.xml.
        public static bool UsernameExists(string username)
        {
            return FindUser(MapPath(UsersPath), username) != null
                || FindUser(MapPath(StaffPath), username) != null;
        }

        // Convenience properties so callers don't need to know the file paths.
        public static string UsersFilePath => MapPath(UsersPath);
        public static string StaffFilePath => MapPath(StaffPath);

        // --- private helpers ---

        private static string MapPath(string virtualPath)
        {
            if (HttpContext.Current != null)
                return HttpContext.Current.Server.MapPath(virtualPath);

            // Fallback for testing or non-web execution
            return Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                virtualPath.TrimStart('~', '/'));
        }

        private static XElement BuildUserElement(string username, string hash, string role, string email)
        {
            return new XElement("User",
                new XElement("Username",     username),
                new XElement("PasswordHash", hash),
                new XElement("Role",         role),
                new XElement("Email",        email)
            );
        }

        // SHA-256 stand-in used only for the TA seed. Replace with the DLL once available.
        private static string Sha256Hash(string input)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}
