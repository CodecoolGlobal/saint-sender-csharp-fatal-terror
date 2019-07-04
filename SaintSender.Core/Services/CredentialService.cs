using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SaintSender.Core.Services
{
    public class CredentialService
    {
        private AesCryptoServiceProvider myAes;

        private AesCryptoServiceProvider CreateAesCryptoServiceProvider()
        {
            AesCryptoServiceProvider aesCryptoServiceProvider = new AesCryptoServiceProvider();
            aesCryptoServiceProvider.Key = Encoding.UTF8.GetBytes("WnZr4u7w!z%C*F-J");
            aesCryptoServiceProvider.IV = Encoding.UTF8.GetBytes("*F-JaNdRgUkXp2s5");
            return aesCryptoServiceProvider;
        }

        public CredentialService()
        {
            myAes = CreateAesCryptoServiceProvider();
        }

        public string[] GetSavedCredentials()
        {

            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
            if (isoStore.FileExists("login_cred.data"))
            {
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("login_cred.data", FileMode.Open, isoStore))
                using (StreamReader reader = new StreamReader(isoStream))
                using (MemoryStream ms = new MemoryStream())
                {
                    reader.BaseStream.CopyTo(ms);
                    byte[] encrypted = ms.ToArray();
                    string decrypted = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);
                    string[] cred = decrypted.Split('/');
                    return cred;
                }

            }
            else { return null; }
        }

        public void SaveCredentials(string username, string password)
        {
            string original = $"{username}/{password}";
            byte[] encrypted;
            using (myAes)
            {
                encrypted = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);
            }

            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
            using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("login_cred.data", FileMode.Create, isoStore))
            using (StreamWriter writer = new StreamWriter(isoStream))
            {
                writer.BaseStream.Write(encrypted, 0, encrypted.Length);
            }
        }

        private byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        private string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }



        public void DeleteCredentials()
        {
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            isoStore.DeleteFile("login_cred.data");
        }

    }
}
