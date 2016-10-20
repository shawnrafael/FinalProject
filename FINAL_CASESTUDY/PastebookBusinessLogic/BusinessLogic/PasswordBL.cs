using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic.BusinessLogic
{
    public class PasswordBL
    {
        private RNGCryptoServiceProvider m_cryptoServiceProvider = new RNGCryptoServiceProvider();

        private const int SALT_SIZE = 24;

        public string GeneratePasswordHash(string password, out string salt)
        {
            salt = GetSaltString();
            string finalString = password + salt;
            return GetPasswordHashAndSalt(finalString);
        }

        public bool IsPasswordMatch(string password, string salt, string hash)
        {
            string finalString = password + salt;
            return hash == GetPasswordHashAndSalt(finalString);
        }

        public string GetPasswordHashAndSalt(string message)
        {
            // Let us use SHA256 algorithm to 
            // generate the hash from this salted password
            SHA256 sha = new SHA256CryptoServiceProvider();
            byte[] dataBytes = GetBytes(message);
            byte[] resultBytes = sha.ComputeHash(dataBytes);

            // return the hash string to the caller
            return GetString(resultBytes);
        }        

        public string GetSaltString()
        {
            //Utility util = new Utility();
            // Lets create a byte array to store the salt bytes
            byte[] saltBytes = new byte[SALT_SIZE];
            // lets generate the salt in the byte array
            m_cryptoServiceProvider.GetNonZeroBytes(saltBytes);
            // Let us get some string representation for this salt
            string saltString = GetString(saltBytes);
            // Now we have our salt string ready lets return it to the caller
            return saltString;
        }

        public byte[] GetBytes(string message)
        {
            byte[] toBytes = Encoding.ASCII.GetBytes(message);
            return toBytes;
        }

        public string GetString(byte[] byteParam)
        {
            string toString = Encoding.ASCII.GetString(byteParam);
            return toString;
        }
    }
}
