using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;


namespace CNY_BaseSys.Common
{
       /// <summary>
    /// A compressed byte array representation of a binary file.
    /// </summary>
    public class TP_File
    {
        public static char[] SIGNATURE = new char[] { '2', 'P', 'X' };

        private byte[] bytes;
        private TP_Header fileHeader;

        /// <summary>
        /// The encrypted array of bytes that represent this file.
        /// </summary>
        public byte[] Contents
        {
            get { return bytes; }
            set { bytes = value; }
        }

        /// <summary>
        /// Create a new TP file for use in stenography from a file.
        /// </summary>
        /// <param name="file">The location of the file.</param>
        /// <param name="key">The key to use in encrypting the data file.</param>
        public TP_File(string file, string key)
        {
            byte[] fileBytes = TP_Encrypt.Encrypt(File.ReadAllBytes(file), key);
            string fileName = ShortFileName(file);

            // Create the fixed size header
            byte[] header = new byte[128];
            int fileNameMaxLength = header.Length - 4 - 1 - 3;

            // First 4 bytes are for the file length
            header[0] = (byte)(fileBytes.Length >> 24);
            header[1] = (byte)((fileBytes.Length >> 16) & 0xFF);
            header[2] = (byte)((fileBytes.Length >> 8) & 0xFF);
            header[3] = (byte)(fileBytes.Length & 0xFF);

            // Next byte is for the file name length
            header[4] = (byte)Math.Min(fileNameMaxLength, fileName.Length);

            // Last 3 bytes for the signature code
            header[header.Length - 3] = (byte)(SIGNATURE[0]);
            header[header.Length - 2] = (byte)(SIGNATURE[1]);
            header[header.Length - 1] = (byte)(SIGNATURE[2]);

            // Save the file name
            for (int i = 0; i < Math.Min(fileNameMaxLength, fileName.Length); i++)
            {
                header[5 + i] = (byte)(fileName.ToCharArray()[i]);
            }

            // Encrypt the header
            header = TP_Encrypt.Encrypt(header, key);

            // Make enough space for both the header and the file
            byte[] allBytes = new byte[header.Length + fileBytes.Length + 1];

            // Save the header to the data to be written to the image
            for (int i = 0; i < header.Length; i++)
            {
                allBytes[i] = header[i];
            }

            // Save the file data
            for (int i = 0; i < fileBytes.Length; i++)
            {
                allBytes[i + header.Length] = fileBytes[i];
            }

            bytes = allBytes;

            PadFileBytes();
        }

        /// <summary>
        /// Create a new TP file for use in stenography from a file using already encrypted and formatted data.
        /// </summary>
        /// <param name="data">The data to convert into a file.</param>
        public TP_File(byte[] data, string key)
        {
            bytes = data;
            fileHeader = new TP_Header(bytes, key);
            PadFileBytes();
        }

        /// <summary>
        /// Save the file represented by this data object.
        /// </summary>
        /// <param name="location">The location to save the file.</param>
        public void Save(string location)
        {
            byte[] data = GetFileBytes();

            File.WriteAllBytes(location, TP_Encrypt.Decrypt(data, fileHeader.Key));
        }

        /// <summary>
        /// Get the byte array of the file data.
        /// </summary>
        public byte[] GetFileBytes()
        {
            byte[] data = new byte[fileHeader.FileLength];

            // Copy the file data out of the byte array
            for (int i = 0; i < fileHeader.FileLength; i++)
            {
                data[i] = bytes[i + 144];
            }

            return data;
        }

        /// <summary>
        /// Get the name of the original file.
        /// </summary>
        public string GetFileName()
        {
            return fileHeader.FileName;
        }

        /// <summary>
        /// Pad the byte count of the file out to multiples of 3 so that we can encode it properly.
        /// </summary>
        public void PadFileBytes()
        {
            if ((bytes.Length % 3) != 0)
            {
                byte[] newBytes = new byte[bytes.Length + (3 - (bytes.Length % 3))];

                for (int i = 0; i < bytes.Length; i++)
                    newBytes[i] = bytes[i];

                bytes = (byte[])newBytes.Clone();
            }
        }

        /// <summary>
        /// Returns the relative file name when given the absolute file name. Example: "C:\WINDOWS\test.txt" would return "test.txt"
        /// </summary>
        private string ShortFileName(string longFileName)
        {
            return longFileName.Substring(longFileName.LastIndexOf("\\") + 1,
                longFileName.Length - longFileName.LastIndexOf("\\") - 1);
        }
    }

    /// <summary>
    /// A header representing the data encoded in the image
    /// </summary>
    public class TP_Header
    {
        /// <summary>
        /// The byte length of the file encoded in the image
        /// </summary>
        public int FileLength { get; set; }

        /// <summary>
        /// The original name of the file that was encoded in the image
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Whether the byte array this header was initialized on represents a valid header
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// The key used to encrypt this file
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Create a new Header decoder
        /// </summary>
        /// <param name="input">The byte array extracted from the image</param>
        /// <param name="key">The key used to initially encrypt the data</param>
        public TP_Header(byte[] input, string key)
        {
            Key = key;

            // The header is 144 bytes encrypted, but only 128 bytes decrypted
            if (input.Length > 144)
            {
                // Get just the bytes for the header from the data
                byte[] headerPre = new byte[144];
                for (int i = 0; i < headerPre.Length; i++)
                {
                    headerPre[i] = input[i];
                }

                // Decrypt the header
                byte[] header = TP_Encrypt.Decrypt(headerPre, key);

                // See if the decrypted header is actually a header
                // If IsValid is true, then the image most likely contains a hidden file
                // There is only a 1 in 2^24 chance (about 0.00000006%) that this will falsely be true
                IsValid = header[header.Length - 3] == TP_File.SIGNATURE[0] &&
                          header[header.Length - 2] == TP_File.SIGNATURE[1] &&
                          header[header.Length - 1] == TP_File.SIGNATURE[2];

                if (IsValid)
                {
                    // Get the length of the hidden file
                    FileLength = 0;
                    FileLength |= header[0] << 24;
                    FileLength |= header[1] << 16;
                    FileLength |= header[2] << 8;
                    FileLength |= header[3];

                    // Get the length of the file name
                    int length = (int)header[4];
                    FileName = "";

                    // Get the original file name
                    for (int i = 0; i < length; i++)
                    {
                        FileName += (char)header[i + 5];
                    }
                }
            }
            else
            {
                IsValid = false;
            }
        }
    }
}
