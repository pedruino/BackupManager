using System;
using System.Security.Cryptography;
using System.Text;

namespace BackupManager.Domain.Services
{
    public class HashService
    {
        private const string SALT_KEY = "Inovatech Soluções Tecnológicas";

        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (var i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static string GenerateHash(string input)
        {
            var md5Hash = MD5.Create();
            var hashBuilder = new StringBuilder();

            foreach (var data in md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input)))
            {
                hashBuilder.Append(data.ToString("x2"));
            }

            return hashBuilder.ToString();
        }

        public static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new MD5Cng();
            var saltWithPlainTextBytes = new byte[salt.Length + plainText.Length];

            for (var i = 0; i < salt.Length; i++)
            {
                saltWithPlainTextBytes[i] = salt[i];
            }

            for (var i = 0; i < plainText.Length; i++)
            {
                saltWithPlainTextBytes[salt.Length + i] = plainText[i];
            }

            return algorithm.ComputeHash(saltWithPlainTextBytes);
        }

        public static string GenerateSaltedHash(string password)
        {
            var provider = MD5.Create();
            var bytes = provider.ComputeHash(Encoding.UTF8.GetBytes(SALT_KEY + password));
            var computedHash = BitConverter.ToString(bytes);

            return computedHash.Replace("-", string.Empty).ToLower();
        }
    }
}