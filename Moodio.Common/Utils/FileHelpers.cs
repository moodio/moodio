using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Moodio.Utils
{
    public static class FileHelpers
    {
        /// <summary>
        /// List of signatures as taken from https://www.filesignatures.net/
        /// </summary>
        public static readonly Dictionary<string, List<byte[]>> FileSignatures = new Dictionary<string, List<byte[]>>
        {
            {
                "jpeg", new List<byte[]>()
                {
                    new byte[]{ 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[]{ 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[]{ 0xFF, 0xD8, 0xFF, 0xE3 }
                }
            },
            {
                "jpg", new List<byte[]>()
                {
                    new byte[]{ 0xFF, 0xD8, 0xFF, 0xE0 },
                    new byte[]{ 0xFF, 0xD8, 0xFF, 0xE2 },
                    new byte[]{ 0xFF, 0xD8, 0xFF, 0xE8 }
                }
            },
            {
                "png", new List<byte[]>()
                {
                    new byte[]{ 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }
                }
            },
            {
                "gif", new List<byte[]>()
                {
                    new byte[]{ 0x47, 0x49, 0x46, 0x38 }
                }
            },
            {
                "bmp", new List<byte[]>()
                {
                    new byte[]{ 0x42, 0x4D }
                }
            },
            {
                "webp", new List<byte[]>()
                {
                    new byte[]{ 0x52, 0x49, 0x46, 0x46 }
                }
            },
            {
                "pdf", new List<byte[]>()
                {
                    new byte[]{ 0x25, 0x50, 0x44, 0x46 }
                }
            },
            {
                "doc", new List<byte[]>()
                {
                    new byte[]{ 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 },
                    new byte[]{ 0x0D, 0x44, 0x4F, 0x43 },
                    new byte[]{ 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1, 0x00 },
                    new byte[]{ 0xDB, 0xA5, 0x2D, 0x00 },
                    new byte[]{ 0xEC, 0xA5, 0xC1, 0x00 }
                }
            },
            {
                "docx", new List<byte[]>()
                {
                    new byte[]{ 0x50, 0x4B, 0x03, 0x04 },
                    new byte[]{ 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00 }
                }
            }
        };

        /// <summary>
        /// Validate the signature of a file based on its file extension
        ///
        /// copied and modified from https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-3.1#validation
        /// Date of access: 15/04/2020
        /// </summary>
        /// <param name="extension"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool ValidationExtension(string extension, Stream file)
        {
            if (String.IsNullOrWhiteSpace(extension))
                return false;

            if (!FileSignatures.ContainsKey(extension))
            {
                return false;
            }

            using (var reader = new BinaryReader(file, encoding: Encoding.ASCII, leaveOpen: true))
            {
                var sigs = FileSignatures[extension];
                var headerBytes = reader.ReadBytes(sigs.Max(m => m.Length));

                //check if any equals
                var res = sigs.Any(s => headerBytes.Take(s.Length).SequenceEqual(s));

                //reset the stream position
                file.Position = 0;

                //return
                return res;
            }
        }
    }
}
