﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace CNY_BaseSys.Common
{
    public static class ByteCompressor
    {
        /// <summary>
        /// Compress a byte array using GZip.
        /// </summary>
        /// <param name="buffer">The byte array to compress.</param>
        public static byte[] Compress(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream();
            GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);
            zip.Write(buffer, 0, buffer.Length);
            zip.Close();
            ms.Position = 0;

            MemoryStream outStream = new MemoryStream();

            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            byte[] gzBuffer = new byte[compressed.Length + 4];
            Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);

            return gzBuffer;
        }

        /// <summary>
        /// Decompress a byte array using GZip.
        /// </summary>
        /// <param name="gzBuffer">The byte array to decompress.</param>
        public static byte[] Decompress(byte[] gzBuffer)
        {
            MemoryStream ms = new MemoryStream();
            int msgLength = BitConverter.ToInt32(gzBuffer, 0);
            ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

            byte[] buffer = new byte[msgLength];

            ms.Position = 0;
            GZipStream zip = new GZipStream(ms, CompressionMode.Decompress);
            zip.Read(buffer, 0, buffer.Length);

            return buffer;
        }
    }
}
