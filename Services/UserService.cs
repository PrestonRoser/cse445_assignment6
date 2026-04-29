using System;
using System.Xml.Linq;

namespace CrimeRiskWeb.Services
{
    // Account management API. Pages call this — never XmlUserStore directly.
    // All hashing is routed through HashUtility
    public static class UserService
    {
        public static bool RegisterUser(string username, string plaintextPassword,
                                        string role, string email)
        {
            try
            {
                // Only allow Member role for self-registration — prevents role escalation
                if (!string.Equals(role, "Member", StringComparison.OrdinalIgnoreCase))
                    role = "Member";

                if (XmlUserStore.UsernameExists(username))
                    return false;

                string hash = hashpassword.hash.createpwdhash(plaintextPassword);
                XmlUserStore.AddUser(username, hash, role, email);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        public static bool AuthenticateUser(string username, string plaintextPassword)
        {
            try
            {
                string attempt = hashpassword.hash.createpwdhash(plaintextPassword);

                XElement user = XmlUserStore.FindUser(XmlUserStore.UsersFilePath, username)
                             ?? XmlUserStore.FindUser(XmlUserStore.StaffFilePath, username);

                if (user == null) return false;

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

        public static string GetUserEmail(string username)
        {
            try
            {
                return XmlUserStore.GetUserEmail(username);
            }
            catch
            {
                return "";
            }
        }

        public static bool UpdateEmail(string username, string newEmail)
        {
            try
            {
                return XmlUserStore.UpdateEmail(username, newEmail);
            }
            catch
            {
                return false;
            }
        }

        // Verifies old password before allowing update.
        public static bool UpdatePassword(string username, string oldPlaintextPassword,
                                        string newPlaintextPassword)
        {
            try
            {
                if (!AuthenticateUser(username, oldPlaintextPassword))
                    return false;

                string newHash = hashpassword.hash.createpwdhash(newPlaintextPassword);
                return XmlUserStore.UpdatePassword(username, newHash);
            }
            catch
            {
                return false;
            }
        }

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