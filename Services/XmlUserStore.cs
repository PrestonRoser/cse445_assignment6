using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CrimeRiskWeb.Services
{
    // Handles all direct reads and writes to Users.xml and Staff.xml.
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

            // App_Data may not exist on a fresh checkout — create it before writing
            string appData = Path.GetDirectoryName(usersFile);
            if (!Directory.Exists(appData))
            {
                Directory.CreateDirectory(appData);
            }

            if (!File.Exists(usersFile))
            {
                new XDocument(new XElement("Users")).Save(usersFile);
            }

            if (!File.Exists(staffFile))
            {
                // Seed the TA credential through HashUtility so seeding stays
                // consistent with login hashing whether SHA-256 or DLL.
                string taHash = hashpassword.hash.createpwdhash("Cse445!");
                XDocument doc = new XDocument(
                    new XElement("Users",
                        BuildUserElement("TA", taHash, "Staff", "ta@asu.edu")
                    )
                );
                doc.Save(staffFile);
            }
        }

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

        // Now returns true only if a matching user was actually found and updated.
        public static bool UpdatePassword(string username, string newHash)
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

                if (user == null) return false;

                user.Element("PasswordHash").Value = newHash;
                doc.Save(filePath);
                return true;
            }
        }

        public static bool UsernameExists(string username)
        {
            return FindUser(MapPath(UsersPath), username) != null
                || FindUser(MapPath(StaffPath), username) != null;
        }

        public static string UsersFilePath => MapPath(UsersPath);
        public static string StaffFilePath => MapPath(StaffPath);

        // --- private helpers ---

        private static string MapPath(string virtualPath)
        {
            if (HttpContext.Current != null)
                return HttpContext.Current.Server.MapPath(virtualPath);

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
    }
}