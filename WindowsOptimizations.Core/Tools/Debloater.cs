using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using WindowsOptimizations.Core.GlobalData;

namespace WindowsOptimizations.Core.Tools
{
    /// <summary>
    /// A class containing methods for debloating windows.
    /// </summary>
    public class Debloater
    {
        /// <summary>
        /// Sets the execution policy to be unrestricted. Debloating won't happen if this isn't executed first.
        /// </summary>
        /// <returns>[<see cref="Task"/>] An asynchronous operation.</returns>
        public Task SetUnrestrictedExecutionPolicy()
        {
            using Process powershell = new();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.Arguments = "Set-ExecutionPolicy Unrestricted -Force";
            powershell.Start();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Debloats windows by downloading Windows10Debloater from github and running it through Powershell. Does not debloat everything so it's used as a 'first phase' of the debloating process.
        /// </summary>
        /// <returns>[<see cref="Task"/>] An asynchronous operation.</returns>
        public async Task DebloatWindowsFirstPhaseAsync()
        {
            string windows10DebloaterZipFilePath = $"{Paths.BasePath}\\windows10-debloater.zip";

            if (!File.Exists(windows10DebloaterZipFilePath) && !Directory.Exists($"{Paths.BasePath}\\Windows10Debloater-master"))
            {
                await Task.WhenAll(Task.Run(async () =>
                {
                    using HttpClient httpClient = new();

                    await using Stream httpStream = await httpClient.GetStreamAsync("https://github.com/Sycnex/Windows10Debloater/archive/refs/heads/master.zip");
                    await using FileStream fileStream = new(
                        path: windows10DebloaterZipFilePath,
                        mode: FileMode.CreateNew,
                        access: FileAccess.Write,
                        share: FileShare.Write,
                        bufferSize: 4096,
                        useAsync: true);

                    await httpStream.CopyToAsync(fileStream);
                }));

                ZipFile.ExtractToDirectory(windows10DebloaterZipFilePath, $"{Paths.BasePath}", true);
                File.Delete(windows10DebloaterZipFilePath);

                using Process powershell = new();
                powershell.StartInfo.FileName = "powershell.exe";
                powershell.StartInfo.Arguments = $"cd '{Paths.BasePath}\\Windows10Debloater-master'; " + @".\Windows10DebloaterGUI.ps1;";
                powershell.Start();
            }
        }

        /// <summary>
        /// Debloats windows by downloading Sophia Script from github and running it through Powershell. It's used as a 'second and final phase' of the debloating process.
        /// </summary>
        /// <returns>[<see cref="Task"/>] An asynchronous operation.</returns>
        public async Task DebloatWindowsSecondPhase()
        {
            string sophiaScriptZipFilePath = $"{Paths.BasePath}\\windows10-sophiascript.zip";

            if (!File.Exists(sophiaScriptZipFilePath) && !Directory.Exists($"{Paths.BasePath}\\Windows-10-Sophia-Script-master"))
            {
                await Task.WhenAll(Task.Run(async () =>
                {
                    using HttpClient httpClient = new();

                    await using Stream httpStream = await httpClient.GetStreamAsync("https://github.com/farag2/Windows-10-Sophia-Script/archive/refs/heads/master.zip");
                    await using FileStream fileStream = new(
                        path: sophiaScriptZipFilePath,
                        mode: FileMode.CreateNew,
                        access: FileAccess.Write,
                        share: FileShare.Write,
                        bufferSize: 4096,
                        useAsync: true);

                    await httpStream.CopyToAsync(fileStream);
                }));

                ZipFile.ExtractToDirectory(sophiaScriptZipFilePath, $"{Paths.BasePath}", true);
                File.Delete(sophiaScriptZipFilePath);

                using Process powershell = new();
                powershell.StartInfo.FileName = "powershell.exe";
                powershell.StartInfo.Arguments = $"cd '{Paths.BasePath}\\Windows-10-Sophia-Script-master\\Sophia\\PowerShell 5.1'; " + @".\Sophia.ps1;";
                powershell.Start();
            }
        }
    }
}
