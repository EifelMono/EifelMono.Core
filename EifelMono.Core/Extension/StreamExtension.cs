using System;
using System.IO;

namespace EifelMono.Core.Extension
{
    public static class StreamExtension
    {
        public static byte[] ToByteArray(this Stream thisValue)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                thisValue.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
