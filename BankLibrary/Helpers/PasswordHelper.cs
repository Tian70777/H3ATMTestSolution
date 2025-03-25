using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Helpers
{
    public class PasswordHelper
    {
        private const int SALT_SIZE = 24;
        private const int HASH_SIZE = 24;
        private const int ITERATIONS = 100000;

        // generate salt and convert and return it as Base64 string
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[SALT_SIZE];
            RandomNumberGenerator.Fill(saltBytes);

            string salt = Convert.ToBase64String(saltBytes);
            return salt;
        }
        
        // Hash the password with given salt string, return hashed pass as strings
        public static string HashPassword(string password, string salt)
        {
            if (password == null)
            {
                throw new ArgumentNullException("Please enter a password");
            }

           
            byte[] saltBytes = Convert.FromBase64String(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                saltBytes,
                ITERATIONS,
                HashAlgorithmName.SHA256
            );

            byte[] hashedBytes = pbkdf2.GetBytes(HASH_SIZE);

            // convert hashedpwd to Base64 strings
            string hashedPwd = Convert.ToBase64String(hashedBytes);
            
            return hashedPwd;
        }

        // Verify password with salt
        public static bool VerifyPassword(string inputPassword, string storedHash, string storedSalt)
        {
            string inputPasswordHashed = HashPassword(inputPassword, storedSalt);
            return inputPasswordHashed == storedHash;
        }
    }
}
