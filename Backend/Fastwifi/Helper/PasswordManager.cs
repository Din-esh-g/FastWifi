using System.Security.Cryptography;
using System.Text;

namespace Fastwifi.Helper
{
    public class PasswordManager
    {
        private static readonly string EncryptionKey = "!SecureKey123456#FastwifiCompanyLLC@2024";

        private static byte[] EncryptString(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Derive a 256-bit key from the password string
                using (SHA256 sha256 = SHA256.Create())
                {
                    aesAlg.Key = sha256.ComputeHash(Encoding.UTF8.GetBytes(EncryptionKey)); // 32 bytes
                }

                aesAlg.IV = new byte[16]; // Still using a zero IV, though using a random IV would be more secure

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return msEncrypt.ToArray();
                }
            }
        }

        private static string DecryptString(byte[] cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Derive a 256-bit key from the password string
                using (SHA256 sha256 = SHA256.Create())
                {
                    aesAlg.Key = sha256.ComputeHash(Encoding.UTF8.GetBytes(EncryptionKey)); // 32 bytes
                }

                aesAlg.IV = new byte[16]; // Still using the same IV

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }


        public static string HashPassword(string password)
        {
            byte[] encryptedBytes = EncryptString(password);
            return Convert.ToBase64String(encryptedBytes);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            string decryptedPassword = DecryptString(Convert.FromBase64String(hash));
            return password == decryptedPassword;
        }

        public static string GetPassword(string hash)
        {
            return DecryptString(Convert.FromBase64String(hash));
        }
        //create method that will create a random password for the user
        public static string GenerateRandomPassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }


    }

}

