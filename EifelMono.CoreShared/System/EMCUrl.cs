using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EifelMono.Core.System
{
    public static class EMCUrl
    {
        public static string Combine(this string url, params string[] paths)
            => $"{url.TrimEnd('/')}/"+ paths.Aggregate((furl, path) => $"{furl.TrimEnd('/')}/{path.TrimStart('/').TrimEnd('/')}").TrimStart('/').TrimEnd('/');
    }
}
