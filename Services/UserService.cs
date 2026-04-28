using System;
using System.Xml.Linq;

namespace CrimeRiskWeb.Services
{
    // Account management API. Pages and teammates call this — never XmlUserStore directly.
    // hashFunc is passed in as a delegate so this stays decoupled from the DLL.
    //
    // Teammate wiring example once the DLL is integrated:
    //   Func<string, string> hash = PasswordHasher.Hash;
    //   bool ok = UserService.RegisterUser(username, password, "Member", email, hash);
    public static class UserService
    {
        // Registers a new member. Returns false if the username is taken or anything fails.
        public static bool RegisterUser(string username, string plaintextPassword,
                                        string role, string email,
                                        Func<string, string> hashFunc)
        {
            try
            {
                // Only allow Member role for self-registration — prevents role escalation
                if (!string.Equals(role, "Member", StringComparison.OrdinalIgnoreCase))
                    role = "Member";

                if (XmlUserStore.UsernameExists(username))
                    return false;

                // Hash before storing — never save plaintext
                string hash = hashFunc(plaintextPassword);
                XmlUserStore.AddUser(username, hash, role, email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Checks both Users.xml and Staff.xml; returns true if credentials match.
        public static bool AuthenticateUser(string username, string plaintextPassword,
                                            Func<string, string> hashFunc)
        {
            try
            {
                string attempt = hashFunc(plaintextPassword);

                // Search both files — staff credentials live in Staff.xml
                XElement user = XmlUserStore.FindUser(XmlUserStore.UsersFilePath, username)
                             ?? XmlUserStore.FindUser(XmlUserStore.StaffFilePath, username);

                if (user == null)
                    return false;

                return string.Equals(
                    (string)user.Element("PasswordHash"),
                    attempt,
                    StringComparison.Ordinal);
            }
            catch
            {
                return false;
            }
        }

        // Returns "Member", "Staff", or null if the user doesn't exist in either file.
        public static string GetRole(string username)
        {
            try
            {
                XElement user = XmlUserStore.FindUser(XmlUserStore.UsersFilePath, username)
                             ?? XmlUserStore.FindUser(XmlUserStore.StaffFilePath, username);

                return user != null ? (string)user.Element("Role") : null;
            }
            catch
            {
                return null;
            }
        }

        // Hashes the new password and updates the stored hash in Users.xml.
        // Members only — staff passwords are not changed through this method.
        public static bool UpdatePassword(string username, string newPlaintextPassword,
                                          Func<string, string> hashFunc)
        {
            try
            {
                string newHash = hashFunc(newPlaintextPassword);
                XmlUserStore.UpdatePassword(username, newHash);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Returns true if the username exists in either Users.xml or Staff.xml.
        public static bool UserExists(string username)
        {
            try
            {
                return XmlUserStore.FindUser(XmlUserStore.UsersFilePath, username) != null
                    || XmlUserStore.FindUser(XmlUserStore.StaffFilePath, username) != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
