using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EifelMono.Core.Extensions;
using EifelMono.Core.Log;
using EifelMono.Core.System;

namespace EifelMono.Core.Tools
{
    public static partial class Tools
    {
        public static async Task LocalDotBackupFileAsync(string filename, int days = 30)
        {
            await Task.Run(() =>
            {
                LocalDotBackupFile(filename, days);
            }).ConfigureAwait(false);
        }
        public static void LocalDotBackupFile(string filename, int days = 30)
        {
            Func<DateTime, string> DateFilename = (timestamp) =>
            {
                string backupDir = Path.Combine(Path.GetDirectoryName(filename), $".backup", $"{Path.GetFileName(filename)}");
                EmcDirectory.EnsureDirectoryExists(backupDir);
                var listOfNames = new List<string>();
                var filenameExtenstion = Path.GetExtension(filename);
                for (int index = 0; index < 30; index++)
                    listOfNames.Add(DateTime.Now.AddDays(-index).ToString("yyyyMMdd") + filenameExtenstion);
                foreach (var file in Directory.GetFiles(backupDir))
                {
                    var foundFilename = Path.GetFileName(file);
                    if (!listOfNames.Contains(foundFilename))
                        try
                        {
                            var foundDatePartFilename = foundFilename.Before("-");
                            if (!string.IsNullOrEmpty(foundDatePartFilename))
                                foundDatePartFilename += filenameExtenstion;
                            if (!listOfNames.Contains(foundDatePartFilename))
                                File.Delete(file);
                        }
                        catch (Exception ex)
                        {
                            ex.LogException();
                        }
                }
                return Path.Combine(backupDir, timestamp.ToString("yyyyMMdd-HHmmss") + Path.GetExtension(filename));
            };

            if (File.Exists(filename))
            {
                var savedFilename = Path.ChangeExtension(filename, "." + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                try
                {
                    File.Copy(filename, savedFilename, true);
                    var backupFilename = DateFilename(DateTime.Now); // clears the original filename
                    File.Copy(savedFilename, backupFilename, true);
                    // File with original filename also to backup to.....
                    File.Copy(filename, Path.Combine(Path.GetDirectoryName(backupFilename), Path.GetFileName(filename)), true);
                }
                catch (Exception ex)
                {
                    ex.LogException();
                }
                finally
                {
                    try
                    {
                        EmcFile.EnsureExistDelete(savedFilename);
                    }
                    catch (Exception ex)
                    {
                        ex.LogException();
                    }
                }
            }
        }
    }
}
