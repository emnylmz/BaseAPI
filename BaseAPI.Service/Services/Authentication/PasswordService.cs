using System.Security.Cryptography;
using BaseAPI.Core.Interfaces;
using BaseAPI.Core.Model;
using Microsoft.Extensions.Options;

namespace BaseAPI.Service.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly Settings _settings;

        public PasswordService(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }

        public bool CompareEncrytedAndUnencryptedPassword(string encryptedPass,string vectorBase64, string unEncryptedPass)
        {
            string encryptedPassForCheck= EncryptPassword(unEncryptedPass,vectorBase64);

            if (encryptedPassForCheck == encryptedPass)
                return true;
            return false;
        }

        public string DecryptPassword(string encryptedPassBase64,string vectorBase64)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = Convert.FromBase64String(_settings.SecretKeyBase64);
                aesAlgorithm.IV = Convert.FromBase64String(vectorBase64);

                // Create decryptor object
                ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor();

                byte[] cipher = Convert.FromBase64String(encryptedPassBase64);

                //Decryption will be done in a memory stream through a CryptoStream object
                using (MemoryStream ms = new MemoryStream(cipher))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Şifrelenmemiş stringi secret key ile deşifreler random iv key oluşturup döner
        /// </summary>
        /// <param name="unEncryptedPass"></param>
        /// <param name="vectorBase64"></param>
        /// <returns></returns>
        public string EncryptPassword(string unEncryptedPass,out string vectorBase64)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = Convert.FromBase64String(_settings.SecretKeyBase64);
                aesAlgorithm.GenerateIV();

                //set the parameters with out keyword
                vectorBase64 = Convert.ToBase64String(aesAlgorithm.IV);

                // Create encryptor object
                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor();

                byte[] encryptedData;

                //Encryption will be done in a memory stream through a CryptoStream object
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(unEncryptedPass);
                        }
                        encryptedData = ms.ToArray();
                    }
                }

                return Convert.ToBase64String(encryptedData);
            }
        }

        /// <summary>
        /// Şifrelenmemiş stringi secret key ve vereceğimiz iv ile deşifreler
        /// </summary>
        /// <param name="unEncryptedPass"></param>
        /// <param name="vectorBase64"></param>
        /// <returns></returns>
        public string EncryptPassword(string unEncryptedPass,string vectorBase64)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = Convert.FromBase64String(_settings.SecretKeyBase64);
                aesAlgorithm.IV=Convert.FromBase64String(vectorBase64);

                // Create encryptor object
                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor();

                byte[] encryptedData;

                //Encryption will be done in a memory stream through a CryptoStream object
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(unEncryptedPass);
                        }
                        encryptedData = ms.ToArray();
                    }
                }

                return Convert.ToBase64String(encryptedData);
            }
        }
    }
}

