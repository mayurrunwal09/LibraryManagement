using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public static class Encryptor
    {
        private static string key = "24a412bc5b694ef4ada52599541067a5";

        public static string EncryptString(string input)
        {
            byte[] iv = new byte[16];
            byte[] array;

            //System.Security.Cryptography;
            using (Aes aes = Aes.Create() )
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key , aes.IV);

                using(MemoryStream memoryStream = new MemoryStream())
                {
                    using(CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using(StreamWriter  writer = new StreamWriter(cryptoStream))
                        {
                            writer.Write(input);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string output)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(output);

            using(Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using(MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using(CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using(StreamReader reader = new StreamReader(memoryStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }

        }
    }
}
