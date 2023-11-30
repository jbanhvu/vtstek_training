using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNY_BaseSys.Common
{
    public class TP_Encrypt
    {
        // Rinjdael Encryption Settings
        static private string saltValue = "m$f#?s?$hu?tastaph3kesEtRUsw@steseTra!+c9EWru?achA#2@5ECHuGeswE@";
        static private string hashAlgorithm = "SHA1";
        static private int passwordIterations = 7;
        static private string initVector = "vafRe$!at!8makac"; // Must be 16 bytes
        static private int keySize = 256;

        /// <summary>
        /// Encrypt a byte array using the Rinjdael (AES) encryption algorithm.
        /// </summary>
        /// <param name="data">The data to encrypt.</param>
        /// <param name="key">The key to use.</param>\
        static public byte[] Encrypt(byte[] data, string key)
        {
            try
            {
                return RijndaelSimple.Encrypt(data,
                                               key,
                                               saltValue,
                                               hashAlgorithm,
                                               passwordIterations,
                                               initVector,
                                               keySize);
            }
            catch (Exception e)
            {
                throw new Exception("Rinjdael \"Encrypt()\": " + e.Message);
            }
        }

        /// <summary>
        /// Decrypt a byte array that was encrypted using the Rinjdael encryption algorithm.
        /// </summary>
        /// <param name="data">The encrypted data.</param>
        /// <param name="key">The key to decrypt with.</param>
        static public byte[] Decrypt(byte[] data, string key)
        {
            try
            {
                return RijndaelSimple.Decrypt(data,
                                               key,
                                               saltValue,
                                               hashAlgorithm,
                                               passwordIterations,
                                               initVector,
                                               keySize);
            }
            catch (Exception e)
            {
                throw new Exception("Rinjdael \"Decrypt()\": " + e.Message);
            }
        }
    }
}
