﻿using System.Diagnostics;
using WindowsOptimizations.Core.Handlers;

namespace WindowsOptimizations.Core.Patches
{
    /// <summary>
    /// An overall windows patch which disables some windows services.
    /// </summary>
    public class WindowsServicePatch
    {
        /// <summary>
        /// Disables almost every unnecessary windows service. Will free up system resources considerably and reduce CPU and RAM usage.
        /// </summary>
        public void DisableAllUnnecessaryServices()
        {
            using Process powershell = new Process();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.CreateNoWindow = true;

            foreach (string service in ConfigurationHandler.UnnecessaryServicesInstance.WindowsServices)
            {
                powershell.StartInfo.Arguments = $"Set-Service -Name" + $" \"{service}\" " + "-StartupType Manual -Status Stopped";
                powershell.Start();
            }
        }
    }
}