using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
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
        /// <returns>[<see cref="Debloater"/>] The same class for allowing method chaining.</returns>
        public Debloater SetUnrestrictedExecutionPolicy()
        {
            using Process powershell = new Process();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.Arguments = "Set-ExecutionPolicy Unrestricted -Force";
            powershell.Start();

            return this;
        }

        /// <summary>
        /// Debloats windows by downloading Windows10Debloater from github and running it through Powershell. Does not debloat everything so it's used as a 'first phase' of the debloating process.
        /// </summary>
        /// <returns>[<see cref="Debloater"/>] The same class for allowing method chaining.</returns>
        public Debloater DebloatWindowsFirstPhase()
        {
            string windows10DebloaterZipFilePath = $"{Paths.BasePath}\\windows10-debloater.zip";

            if (!File.Exists(windows10DebloaterZipFilePath) && !Directory.Exists($"{Paths.BasePath}\\Windows10Debloater-master"))
            {
                using WebClient webClient = new WebClient();
                webClient.DownloadFile("https://github.com/Sycnex/Windows10Debloater/archive/refs/heads/master.zip", windows10DebloaterZipFilePath);

                ZipFile.ExtractToDirectory(windows10DebloaterZipFilePath, $"{Paths.BasePath}", true);
                File.Delete(windows10DebloaterZipFilePath);
            }

            using Process powershell = new Process();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.Arguments = $"cd '{Paths.BasePath}\\Windows10Debloater-master'; " + @".\Windows10DebloaterGUI.ps1;";
            powershell.Start();

            return this;
        }

        /// <summary>
        /// Debloats windows by downloading Sophia Script from github and running it through Powershell. It's used as a 'second and final phase' of the debloating process.
        /// </summary>
        /// <returns>[<see cref="Debloater"/>] The same class for allowing method chaining.</returns>
        public Debloater DebloatWindowsSecondPhase()
        {
            string sophiaScriptZipFilePath = $"{Paths.BasePath}\\windows10-sophiascript.zip";

            if (!File.Exists(sophiaScriptZipFilePath) && !Directory.Exists($"{Paths.BasePath}\\Windows-10-Sophia-Script-master"))
            {
                using WebClient webClient = new WebClient();
                webClient.DownloadFile("https://github.com/farag2/Windows-10-Sophia-Script/archive/refs/heads/master.zip", sophiaScriptZipFilePath);

                ZipFile.ExtractToDirectory(sophiaScriptZipFilePath, $"{Paths.BasePath}", true);
                File.Delete(sophiaScriptZipFilePath);
            }

            using Process powershell = new Process();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.Arguments = $"cd '{Paths.BasePath}\\Windows-10-Sophia-Script-master\\Sophia\\PowerShell 5.1'; " + @".\Sophia.ps1;";
            powershell.Start();

            return this;
        }
    }
}
