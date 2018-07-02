using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ProLog2.Essentials.EifelMono.Core.Extension;

namespace EifelMono.Core.System
{
    public static class EmcDirectory
    {
        // TODO Wildcards 
        //   DeleteDirectory
        //   EnumerateDirectories
        //   EnumerateFiles
        // x/**/bin
        //
        public static bool Exists(string path)
        {
            return Directory.Exists(path);
        }
        public static async Task<bool> ExistsAsync(string path)
        {
            return await Task<bool>.Run(() =>
            {
                return Exists(path);
            }).ConfigureAwait(false);
        }
        public static void CreateDirectory(string path)
        {
            Directory.CreateDirectory(path);
        }

        public static async Task CreateDirectoryAsync(string path)
        {
            await Task.Run(() =>
            {
                CreateDirectory(path);
            }).ConfigureAwait(false);
        }

        public static void Delete(string path)
        {
            Directory.Delete(path);
        }

        public static async Task DeleteAsync(string path)
        {
            await Task.Run(() =>
            {
                Delete(path);
            }).ConfigureAwait(false);
        }

        public static void Clear(string path)
        {
            ;
        }

        public static async Task ClearAsync(string path)
        {
            await Task.Run(() =>
            {
                Clear(path);
            }).ConfigureAwait(false);
        }

        public static void Delete(string path, bool recursive)
        {
            Directory.Delete(path, recursive);
        }

        public static async Task DeleteAsync(string path, bool recursive)
        {
            await Task.Run(() =>
            {
                Delete(path, recursive);
            }).ConfigureAwait(false);
        }

        public static string EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public static async Task<string> EnsureDirectoryExistsAsync(string path)
        {
            return await Task<string>.Run(() =>
            {
                return EnsureDirectoryExists(path);
            }).ConfigureAwait(false);
        }

        public static void Copy(string sourcePath, string destinationPath, bool overwrite)
        {
            if (Directory.Exists(sourcePath))
            {
                if (Directory.Exists(destinationPath))
                    if (!overwrite)
                        return;

                EnsureDirectoryExists(destinationPath);
                foreach (var file in Directory.GetFiles(sourcePath))
                    File.Copy(file, Path.Combine(destinationPath, Path.GetFileName(file)), true);
            }
            else
            {
                throw new EmcException($"No source directory {sourcePath}");
            }
        }
        public static async Task CopyAsync(string sourcePath, string destinationPath, bool overwrite)
        {
            await Task.Run(() =>
            {
                Copy(sourcePath, destinationPath, overwrite);
            }).ConfigureAwait(false);
        }

        #region EnumerateFiles
        public static IEnumerable<string> EnumerateFiles(string path)
        {
            return Directory.EnumerateFiles(path);
        }

        public static async Task<IEnumerable<string>> EnumerateFilesAsync(string path)
        {
            return await Task<IEnumerable<string>>.Run(() =>
            {
                return EnumerateFiles(path);
            }).ConfigureAwait(false);
        }
        public static IEnumerable<string> EnumerateFiles(string path, string searchPattern)
        {
            return Directory.EnumerateFiles(path, searchPattern);
        }
        public static async Task<IEnumerable<string>> EnumerateFilesAsync(string path, string searchPattern)
        {
            return await Task<IEnumerable<string>>.Run(() =>
            {
                return EnumerateFiles(path, searchPattern);
            }).ConfigureAwait(false);
        }

        public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateFiles(path, searchPattern, searchOption);
        }

        public static async Task<IEnumerable<string>> EnumerateFilesAsync(string path, string searchPattern, SearchOption searchOption)
        {
            return await Task<IEnumerable<string>>.Run(() =>
            {
                return EnumerateFiles(path, searchPattern, searchOption);
            }).ConfigureAwait(false);
        }
        #endregion

        #region EnumerateDirectories
        public static IEnumerable<string> EnumerateDirectories(string path)
        {
            return Directory.EnumerateDirectories(path);
        }
        public static async Task<IEnumerable<string>> EnumerateDirectoriesAsync(string path)
        {
            return await Task<IEnumerable<string>>.Run(() =>
            {
                return EnumerateDirectories(path);
            }).ConfigureAwait(false);
        }

        public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern)
        {
            return Directory.GetDirectories(path, searchPattern);
        }

        public static async Task<IEnumerable<string>> EnumerateDirectoriesAsync(string path, string searchPattern)
        {
            return await Task<IEnumerable<string>>.Run(() =>
            {
                return EnumerateDirectories(path, searchPattern);
            }).ConfigureAwait(false);
        }

        public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateDirectories(path, searchPattern, searchOption);
        }
        public static async Task<IEnumerable<string>> EnumerateDirectoriesAsync(string path, string searchPattern, SearchOption searchOption)
        {
            return await Task<string[]>.Run(() =>
            {
                return EnumerateDirectories(path, searchPattern, searchOption);
            }).ConfigureAwait(false);
        }
        #endregion

        #region Wildcard GetDirectories

        public static List<string> GetDirectories(string filter)
        {
            if (string.IsNullOrEmpty(filter))
                filter = ".";

            var dirs = new List<string>();
            var separateFilter = EmcPath.SplitPath(filter);
            var startPath = "";
            foreach (var detailFilter in separateFilter)
                if (detailFilter.OrContains("*", "?"))
                    break;
                else
                    startPath = EmcPath.Combine(startPath, detailFilter);

            var result = new List<string>();
            foreach (var dir in EnumerateDirectories(startPath, "*", SearchOption.AllDirectories))
            {
                if (EmcPath.MatchPath(dir, filter))
                    result.Add(dir);
                else
                    if (EmcPath.MatchPath(dir + $"{EmcPath.PathSeparator} ", filter))
                    result.Add(dir);
            }
            return result;
        }

        public static List<string> GetFiles(string filter)
        {
            if (string.IsNullOrEmpty(filter))
                filter = ".";

            var dirs = new List<string>();
            var separateFilter = EmcPath.SplitPath(filter);
            var startPath = "";
            foreach (var detailFilter in separateFilter)
                if (detailFilter.OrContains("*", "?"))
                    break;
                else
                    startPath = EmcPath.Combine(startPath, detailFilter);

            var result = new List<string>();
            foreach (var dir in EnumerateFiles(startPath, "*", SearchOption.AllDirectories))
            {
                if (EmcPath.MatchPath(dir, filter))
                    result.Add(dir);
                else
                    if (EmcPath.MatchPath(dir + $"{EmcPath.PathSeparator} ", filter))
                    result.Add(dir);
            }
            return result;
        }
        #endregion
    }
}
