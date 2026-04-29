using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace hashpassword
{
    public class hash
    {
        public static string createpwdhash(string password)
        {
            //checking to make sure password isnt empty
            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Password cannot be empty");
            }
            //converting string to byte array 
            byte[] passwordbyte = Encoding.UTF8.GetBytes(password);
            //creating an instance of sha then computing hash of password
            var sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(passwordbyte);

            //converting to hex string for storage
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
            
        }
        public static bool hashcompare(string password, string hash)
        {
            //generating hash of entered password and comparing them to one another
            string entered_password = createpwdhash(password);
            return entered_password == hash;
        }
    }
}
