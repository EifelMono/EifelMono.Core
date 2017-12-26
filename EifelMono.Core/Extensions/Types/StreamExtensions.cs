using System;
using System.IO;

namespace EifelMono.Core.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ToByteArray(this Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
