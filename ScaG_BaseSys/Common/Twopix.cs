using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
namespace CNY_BaseSys.Common
{
      public static class TP_Stenography
    {
        /// <summary>
        /// Stenographically hide a file in an image.
        /// </summary>
        /// <param name="image">The image to hide stuff in.</param>
        /// <param name="file">The file to hide in the image.</param>
        /// <param name="quality">The number of bits per byte to encode data into. (1-8)</param>
        /// <exception cref="OutOfMemoryException"></exception>
        /// <example>
        ///  // Format the image for encoding
        ///  Image originalImage = Bitmap.FromFile("image.bmp");
        ///  Bitmap image = new Bitmap(originalImage.Width, originalImage.Height);
        ///  Graphics g = Graphics.FromImage(image);
        ///  g.DrawImage(originalImage, 0, 0, originalImage.Width, originalImage.Height);
        ///  TP_Stenography.EncodeImage(image, new TP_File("secret.txt", "password"), 1);
        ///  image.Save("output.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
        /// </example>
        static public void EncodeImage(Bitmap image, TP_File file, int quality)
        {
            // Prepare the image for direct access
            BitmapData bmData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            IntPtr Scan0 = bmData.Scan0;
            int nOffset = stride - image.Width * 3;

            int data;                   // Represents 3 bytes of input data
            byte[] bits = new byte[24]; // Represents 24 bits from the 3 bytes of input data

            byte r, g, b;               // Pixel color values (in RGB)
            int xloc = 0;               // The X location of the working pixel in the image
            int yloc = 0;               // The Y location of the working pixel in the image
            int bit = 0;                // The bit location we are encoding data into
            byte mask = 0x01;           // The bit mask for where we are encoding data into

            // Make sure the file to encode into the image is padded to groups of 3 bytes
            file.PadFileBytes();

            // Make sure the image is large enough to hold the data file
            if (GetImageCapacity(image) * quality < file.Contents.Length)
                throw new FileTooLargeForImageException();

            unsafe // :)
            {
                // The pointer to the Bitmap pixel data of the image
                byte* p = (byte*)(void*)Scan0;

                for (int i = 0; i < file.Contents.Length; i += 3)
                {
                    // Read in 3 bytes of data from the file to be hidden as an int
                    data = file.Contents[i] << 16;
                    data |= file.Contents[i + 1] << 8;
                    data |= file.Contents[i + 2];

                    // Isolate 24 bits (from the 3 bytes) into an array
                    for (int j = 0; j < 24; j++)
                        bits[j] = (byte)((data >> (24 - j - 1)) & 1);

                    // Loop through the pixels and change the color's low order bit to a data bit
                    for (int location = 0; location < 8 * 3; location+=3)
                    {
                        // Get the RGB parts of the next pixel (It's actually stored as BGR, thus the indices)
                        r = p[2];
                        g = p[1];
                        b = p[0];

                        // Clear bit we are going to write to
                        r &= (byte)(0xFF - mask);
                        g &= (byte)(0xFF - mask);
                        b &= (byte)(0xFF - mask); 

                        // Set bits to a piece of data
                        r |= (byte)(bits[location] << bit);
                        g |= (byte)(bits[location + 1] << bit);
                        b |= (byte)(bits[location + 2] << bit);

                        // Save the pixel back to the image
                        p[2] = r;
                        p[1] = g;
                        p[0] = b;

                        // Increment the location of the pixels we are writing to (3 bytes)
                        p += 3;

                        // If we reach the end of a row, adjust the pointer further
                        xloc++;
                        if (xloc >= image.Width)
                        {
                            p += nOffset;
                            xloc = 0;

                            yloc++;
                            if (yloc >= image.Height)
                            {
                                // Start at the beginning of the image again
                                p = (byte*)(void*)Scan0;
                                xloc = 0;
                                yloc = 0;

                                // Use the next up bit in each pixel
                                bit++;
                                mask *= 2;
                            }
                        }
                    }
                }
            }

            image.UnlockBits(bmData);

            return;
        }

        /// <summary>
        /// Gets the amount of bytes that will fit into an image when using only 1 bit per byte.
        /// </summary>
        /// <param name="image">The image to check</param>
        static public int GetImageCapacity(Bitmap image)
        {
            return (int)((float)(image.Width * image.Height * 3) / 8.0f);
        }

        /// <summary>
        /// Extract a file that was hidden stenagraphically in an image.
        /// </summary>
        /// <param name="image">The image to extract an image from.</param>
        static public TP_File DecodeImage(Bitmap image, string key)
        {
            // Prepare the image for direct access
            BitmapData bmData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = bmData.Stride;
            IntPtr Scan0 = bmData.Scan0;
            int nOffset = stride - image.Width * 3;

            // The file that is extracted from the image
            byte[] hiddenData = new byte[GetImageCapacity(image) * 8];

            byte r, g, b;       // Pixel color values (in RGB)
            int xloc = 0;       // The X location of the working pixel in the image 
            int yloc = 0;       // The Y location of the working pixel in the image
            int data;           // Represents 3 bytes of input data
            int dIndex = 0;     // The index of the data file's byte array
            int bit = 0;        // The bit location to read encoded data from
            byte mask = 0x01;   // The bit mask for for masking data bits off

            unsafe // :)
            {
                // The pointer to the Bitmap pixel data of the image
                byte* p = (byte*)(void*)Scan0;

                // Loop through the pixels grabbing the hidden data bits
                for (int i = 0; i < hiddenData.Length - 3; i += 3)
                {
                    // Clear the data
                    data = 0;

                    for (int j = 0; j < 8; j++)
                    {
                        // Get the data bits of each pixel
                        r = (byte)(p[2] & mask);
                        g = (byte)(p[1] & mask);
                        b = (byte)(p[0] & mask);

                        // Shift the data over
                        r >>= bit;
                        g >>= bit;
                        b >>= bit;

                        // Move pre-found data over
                        data <<= 3;

                        // Add the 3 new bits to the data
                        data |= (r << 2) | (g << 1) | b;

                        // Increment the location of the pixels we are writing to (3 bytes)
                        p += 3;

                        // If we reach the end of a row, adjust the pointer further
                        xloc++;
                        if (xloc >= image.Width)
                        {
                            p += nOffset;
                            xloc = 0;

                            yloc++;
                            if (yloc >= image.Height)
                            {
                                // Start at the beginning of the image again
                                p = (byte*)(void*)Scan0;
                                xloc = 0;
                                yloc = 0;

                                // Use the next up bit in each pixel
                                bit++;
                                mask *= 2;
                            }
                        }
                    }

                    // Save the extracted data to the file bytes
                    hiddenData[dIndex] = (byte)(data >> 16);
                    hiddenData[dIndex + 1] = (byte)((data >> 8) & 0xFF);
                    hiddenData[dIndex + 2] = (byte)(data & 0xFF);

                    dIndex += 3;
                }
            }

            image.UnlockBits(bmData);

            return new TP_File(hiddenData, key);
        }

        /// <summary>
        /// Pad the byte count of an array to multiples of 3 so that we can encode it properly.
        /// </summary>
        static private void PadBytes(ref byte[] bytes)
        {
            if ((bytes.Length % 3) != 0)
            {
                byte[] newBytes = new byte[bytes.Length + (bytes.Length % 3)];

                for (int i = 0; i < bytes.Length; i++)
                    newBytes[i] = bytes[i];

                bytes = (byte[])newBytes.Clone();
            }
        }
    }

    public class FileTooLargeForImageException : OutOfMemoryException
    {
        public FileTooLargeForImageException()
            : base("The image is not large enough to hold a file of that size!")
        { }
    }
}
