using System;
using System.Diagnostics;
using System.Windows;
using WindowsOptimizations.Core.Models;

namespace WindowsOptimizations.Core.Optimizations.System
{
    /// <summary>
    /// Handles the way Windows services will run.
    /// </summary>
    public class WindowsServiceOptimizations
    {
        /// <summary>
        /// Disabled a specific Windows service.
        /// </summary>
        /// <param name="service">The Windows service.</param>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool DisableService(WindowsService service)
        {
            try
            {
                using Process powershell = new();
                powershell.StartInfo.FileName = "powershell.exe";
                powershell.StartInfo.CreateNoWindow = true;
                powershell.StartInfo.Arguments = $"Set-Service -Name" + $" \"{service.Name}\" " + "-StartupType Disabled -Status Stopped";
                powershell.Start();
                powershell.WaitForExit();

                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// Enables a specific Windows service.
        /// </summary>
        /// <param name="service">The Windows service.</param>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool EnableService(WindowsService service)
        {
            try
            {
                using Process powershell = new();
                powershell.StartInfo.FileName = "powershell.exe";
                powershell.StartInfo.CreateNoWindow = true;
                powershell.StartInfo.Arguments = $"Set-Service -Name" + $" \"{service.Name}\" " + "-StartupType Manual -Status Running";
                powershell.Start();
                powershell.WaitForExit();

                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }
    }
}