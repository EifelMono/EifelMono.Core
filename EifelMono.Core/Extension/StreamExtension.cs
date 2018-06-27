using System;
using System.IO;

namespace EifelMono.Core.Extension
{
    public static class StreamExtension
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
