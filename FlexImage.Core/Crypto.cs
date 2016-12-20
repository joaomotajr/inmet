// ============================================
// 
// FlexImage.Core
// Crypto.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace FlexImage.Core
{
    public class Security
    {
        private static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.Zeros;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
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

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.Zeros;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
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

        #region Nested type: CryptoAES

        public class CryptoAES
        {
            public string Encrypt(string original)
            {
                StringBuilder s;

                // Create a new instance of the RijndaelManaged
                // class.  This generates a new key and initialization 
                // vector (IV).
                using (var myRijndael = new RijndaelManaged())
                {
                    myRijndael.GenerateKey();
                    myRijndael.GenerateIV();

                    // Encrypt the string to an array of bytes.
                    byte[] encrypted = EncryptStringToBytes(original, myRijndael.Key, myRijndael.IV);

                    s = new StringBuilder();
                    foreach (byte item in encrypted)
                        s.Append(item.ToString("X2") + " ");
                }
                return s.ToString();
            }

            public string Decrypt(string encrypted)
            {
                string decrypted = "";

                // Create a new instance of the RijndaelManaged
                // class.  This generates a new key and initialization 
                // vector (IV).
                using (var myRijndael = new RijndaelManaged())
                {
                    myRijndael.GenerateKey();
                    myRijndael.GenerateIV();

                    byte[] byteArray = Encoding.ASCII.GetBytes(encrypted);

                    // Decrypt the bytes to a string.
                    decrypted = DecryptStringFromBytes(byteArray, myRijndael.Key, myRijndael.IV);
                }
                return decrypted;
            }
        }

        #endregion

        #region Nested type: Hash

        public class Hash
        {
            /// <summary>
            ///   Calculates SHA1 hash
            /// </summary>
            /// <param name="text"> input string </param>
            /// <param name="enc"> Character encoding </param>
            /// <returns> SHA1 hash </returns>
            /// 
            public string SHA1String(string text)
            {
                byte[] buffer = Encoding.Default.GetBytes(text);
                var cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
                string hash = BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");

                return hash;
            }

            private byte[] ConvertStringToByteArray(string data)
            {
                return (new UnicodeEncoding()).GetBytes(data);
            }

            public string SHA1File(string pathName)
            {
                string strResult = "";
                string strHashData = "";

                byte[] arrbytHashValue;
                FileStream oFileStream = null;

                var oSHA1Hasher =
                    new SHA1CryptoServiceProvider();

                try
                {
                    oFileStream = this.GetFileStream(pathName);
                    arrbytHashValue = oSHA1Hasher.ComputeHash(oFileStream);
                    oFileStream.Close();

                    strHashData = BitConverter.ToString(arrbytHashValue);
                    strHashData = strHashData.Replace("-", "");
                    strResult = strHashData;
                }
                catch (Exception e)
                {
                    Alert.Exclamation("SHA1File::Error: " + pathName + " - " + e.Message);
                    //System.Windows.Forms.MessageBox.Show(ex.Message, "Error!",
                    //         System.Windows.Forms.MessageBoxButtons.OK,
                    //         System.Windows.Forms.MessageBoxIcon.Error,
                    //         System.Windows.Forms.MessageBoxDefaultButton.Button1);
                }

                return (strResult);
            }

            private FileStream GetFileStream(string pathName)
            {
                return (new FileStream(pathName, FileMode.Open,
                                       FileAccess.Read, FileShare.ReadWrite));
            }

            public string encryptSHA2(String pwd, String internalUsrId, String randomSaltKey)
            {
                string SALT_FILL = "9%Al¨eB0)";

                try
                {
                    // Concatena três chaves para impedir ataques de aniversário e de dicionário. saltKey = usrInternalID, SALT_FILL = "9%Al¨eB0)", randomSaltKey = valor aletório gerado na criação inicial do hash.
                    // A norma PCKS#5 recomenda a utilização de um salt de 8 Bytes aleatórios.
                    string finalSalt = string.Concat(internalUsrId, SALT_FILL, randomSaltKey) + pwd;
                    string hash = Sha256(finalSalt);

                    for (int i = 0; i < 1000; i++)
                    {
                        hash = Sha256(hash);
                    }

                    return hash;

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.ToString());
                }

                return "";
            }

            public static string Sha256(string text)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                var cryptoTransformSHA256 = new SHA256CryptoServiceProvider();
                string hash = BitConverter.ToString(cryptoTransformSHA256.ComputeHash(buffer)).Replace("-", "");
                return hash;
            }
        }

        #endregion
    }
}