using System.Diagnostics;
using WindowsOptimizations.Core.Models;

namespace WindowsOptimizations.Core.Patches
{
    /// <summary>
    /// Handles the way Windows services will run.
    /// </summary>
    public class WindowsServicePatch
    {
        /// <summary>
        /// Disables a specific Windows service.
        /// </summary>
        public void DisableService(WindowsService windowsServiceModel)
        {
            using Process powershell = new();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.CreateNoWindow = true;

            powershell.StartInfo.Arguments = $"Set-Service -Name" + $" \"{windowsServiceModel.Name}\" " + "-StartupType Disabled -Status Stopped";
            powershell.Start();
        }

        /// <summary>
        /// Enables a specific Windows service.
        /// </summary>
        /// <param name="windowsServiceModel"></param>
        public void EnableService(WindowsService windowsServiceModel)
        {
            using Process powershell = new();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.CreateNoWindow = true;

            powershell.StartInfo.Arguments = $"Set-Service -Name" + $" \"{windowsServiceModel.Name}\" " + "-StartupType Manual -Status Running";
            powershell.Start();
        }
    }
}