using System;
using System.Text.RegularExpressions;

namespace EifelMono.Core.System
{
    public static class EmcPath
    {
        public static string Combine(params string[] paths)
            => global::System.IO.Path.Combine(paths); // Combine Unix???

        public static char PathSeparator = Environment.OSVersion.Platform == PlatformID.Unix ? '/' : '\\';

        public static char WindowsPathSeparator = '\\';
        public static char UnixPathSeprator = '/';

        public static string[] SplitPath(string path)
            => string.IsNullOrEmpty(path) ? new string[] { "" } : path.Split(PathSeparator);

        public static string OSPath(string path)
            => Environment.OSVersion.Platform == PlatformID.Unix
                ? path.Replace(WindowsPathSeparator, UnixPathSeprator)
                : path.Replace(UnixPathSeprator, WindowsPathSeparator);

        public static string GetFileName(string path)
            => global::System.IO.Path.GetFileName(path);
        public static string ChangeExtension(string path, string extension)
            => global::System.IO.Path.ChangeExtension(path, extension);

        public static string GetDirectoryName(string path)
          => global::System.IO.Path.GetDirectoryName(path);

        public static string GetExtension(string path)
       => global::System.IO.Path.GetExtension(path);

        public static bool MatchPath(string path, string matchPath)
        {
            var pathSplitChars = $"\\\\";
            var pathChars = "\\w .\\(\\)§$%&{}\\+\\-_#@~";
            var matchPattern = matchPath
                .Replace("\\", "\\\\")
                .Replace(".", "\\.")
                .Replace("**", $"[{pathSplitChars}{pathChars}]+")
                .Replace("?", $"[{pathChars}]")
                .Replace("*", $"[{pathChars}]+");
            var matches = Regex.Matches(path, matchPattern);
            return matches.Count > 0;
        }
    }
}
