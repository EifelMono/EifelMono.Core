using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EifelMono.Core.System
{
    public static class EmcFile
    {
        public static bool Exist(string fileName)
        {
            return File.Exists(fileName);
        }
        public static async Task<bool> ExistAsync(string fileName)
        {
            return await Task<bool>.Run(() =>
            {
                return Exist(fileName);
            }).ConfigureAwait(false);
        }

        public static void Delete(string fileName)
        {
            File.Delete(fileName);
        }
        public static async Task DeleteAsync(string fileName)
        {
            await Task.Run(() =>
            {
                Delete(fileName);
            }).ConfigureAwait(false);
        }

        public static void EnsureExistDelete(string fileName)
        {
            if (Exist(fileName))
                Delete(fileName);
        }

        public static async Task EnsureExistDeleteAsync(string fileName)
        {
            await Task.Run(() =>
            {
                EnsureExistDelete(fileName);
            }).ConfigureAwait(false);
        }

        public static void Copy(string sourceFileName, string destinationFileName, bool overwrite = false)
        {
            File.Copy(sourceFileName, destinationFileName, overwrite);
        }
        public static async Task CopyAsync(string sourceFileName, string destinationFileName, bool overwrite = false)
        {
            await Task.Run(() =>
            {
                Copy(sourceFileName, destinationFileName, overwrite);
            }).ConfigureAwait(false);
        }


        #region AllText 
        public static string ReadAllText(string fileName)
        {
            return File.ReadAllText(fileName);
        }
        public static async Task<string> ReadAllTextAsync(string fileName)
        {
            return await Task<string>.Run(() =>
            {
                return ReadAllText(fileName);
            }).ConfigureAwait(false);
        }

        public static void WriteAllText(string fileName, string text)
        {
            File.WriteAllText(fileName, text);
        }
        public static async Task WriteAllTextAsync(string fileName, string text)
        {
            await Task.Run(() =>
            {
                WriteAllText(fileName, text);
            }).ConfigureAwait(false);
        }

        public static void AppendAllText(string fileName, string text)
        {
            File.AppendAllText(fileName, text);
        }
        public static async Task AppendAllTextAsync(string fileName, string text)
        {
            await Task.Run(() =>
            {
                AppendAllText(fileName, text);
            }).ConfigureAwait(false);
        }

        #endregion

        #region Json
        public static T ReadJson<T>(string filename)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(filename));
        }
        public static async Task<T> ReadJsonAsync<T>(string filename)
        {
            return await Task<T>.Run(() =>
            {
                return ReadJson<T>(filename);
            }).ConfigureAwait(false);
        }

        public static void WriteJson(string filename, object value, bool formatting = false)
        {
            File.WriteAllText(filename, JsonConvert.SerializeObject(value, formatting ? Formatting.Indented : Formatting.None));
        }

        public static async Task WriteJsonAsync(string filename, object value, bool formatting = false)
        {
            await Task.Run(() =>
            {
                WriteJson(filename, value, formatting);
            }).ConfigureAwait(false);
        }
        #endregion
    }
}
