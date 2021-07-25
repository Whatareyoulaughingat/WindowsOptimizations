using System.Diagnostics;
using System.Threading.Tasks;
using WindowsOptimizations.Core.Models;

namespace WindowsOptimizations.Core.Tweaks
{
    /// <summary>
    /// Handles the way Windows services will run.
    /// </summary>
    public static class WindowsServiceTweaks
    {
        /// <summary>
        /// Disabled a specific Windows service.
        /// </summary>
        /// <param name="service">The Windows service.</param>
        /// <returns>[<see cref="Task"/>] An asynchronous operation.</returns>
        public static Task DisableService(WindowsService service)
        {
            using Process powershell = new ();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.CreateNoWindow = true;
            powershell.StartInfo.Arguments = $"Set-Service -Name" + $" \"{service.Name}\" " + "-StartupType Disabled -Status Stopped";

            powershell.Start();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Enables a specific Windows service.
        /// </summary>
        /// <param name="service">The Windows service.</param>
        /// <returns>[<see cref="Task"/>] An asynchronous operation.</returns>
        public static Task EnableService(WindowsService service)
        {
            using Process powershell = new ();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.CreateNoWindow = true;
            powershell.StartInfo.Arguments = $"Set-Service -Name" + $" \"{service.Name}\" " + "-StartupType Manual -Status Running";

            powershell.Start();
            return Task.CompletedTask;
        }
    }
}