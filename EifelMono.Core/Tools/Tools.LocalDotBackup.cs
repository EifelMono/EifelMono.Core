using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using EifelMono.Core.Extension;
using EifelMono.Core.Log;
using EifelMono.Core.System;

namespace EifelMono.Core.Tools
{
    public static partial class Tools
    {
        public static void LocalDotBackupFile(string filename, int days = 30)
        {
            Func<DateTime, string> DateFilename = (timestamp) =>
            {
                string backupDir = Path.Combine(EmcPath.GetDirectoryName(filename), $".backup", $"{Path.GetFileName(filename)}");
                EmcDirectory.EnsureDirectoryExists(backupDir);
                var listOfNames = new List<string>();
                var filenameExtenstion = EmcPath.GetExtension(filename);
                for (int index = 0; index < 30; index++)
                    listOfNames.Add(DateTime.Now.AddDays(-index).ToString("yyyyMMdd") + filenameExtenstion);
                foreach (var file in EmcDirectory.EnumerateFiles(backupDir))
                {
                    var foundFilename = Path.GetFileName(file);
                    if (!listOfNames.Contains(foundFilename))
                        try
                        {
                            var foundDatePartFilename = foundFilename.Before("-");
                            if (!string.IsNullOrEmpty(foundDatePartFilename))
                                foundDatePartFilename += filenameExtenstion;
                            if (!listOfNames.Contains(foundDatePartFilename))
                                EmcFile.Delete(file);
                        }
                        catch (Exception ex)
                        {
                            ex.LogException();
                        }
                }
                return EmcPath.Combine(backupDir, timestamp.ToString("yyyyMMdd-HHmmss") + Path.GetExtension(filename));
            };

            if (File.Exists(filename))
            {
                var savedFilename = EmcPath.ChangeExtension(filename, "." + DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                try
                {
                    EmcFile.Copy(filename, savedFilename, true);
                    var backupFilename = DateFilename(DateTime.Now); // clears the original filename
                    EmcFile.Copy(savedFilename, backupFilename, true);
                    // File with original filename also to backup to.....
                    EmcFile.Copy(filename, Path.Combine(Path.GetDirectoryName(backupFilename), Path.GetFileName(filename)), true);
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

        public static async Task LocalDotBackupFileAsync(string filename, int days = 30)
        {
            await Task.Run(() =>
            {
                LocalDotBackupFile(filename, days);
            }).ConfigureAwait(false);
        }
    }
}
