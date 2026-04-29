using System;
using System.Security.Cryptography;
using System.Text;

namespace CrimeRiskWeb.Services
{
    // Single point of entry for password hashing across the whole app.
    // Temorary SHA-256 stand-in.
    public static class HashUtility
    {
        public static string HashPassword(string password)
        {
            if (password == null) password = "";

            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}