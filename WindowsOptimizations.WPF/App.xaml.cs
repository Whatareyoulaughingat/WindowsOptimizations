﻿using ReactiveUI;
using Splat;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using WindowsOptimizations.Core.GlobalData;
using WindowsOptimizations.Core.Handlers.Configuration;

namespace WindowsOptimizations
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            Directory.CreateDirectory(Paths.BasePath);

            await ConfigurationHandler.SerializeOnCreationAndDeserialize(Paths.UnnecessaryWindowsServicesJsonFile);

            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Delete the downloaded debloaters.
            if (Directory.Exists($"{Paths.BasePath}\\Windows10Debloater-master") && Directory.Exists($"{Paths.BasePath}\\Windows-10-Sophia-Script-master"))
            {
                Directory.Delete($"{Paths.BasePath}\\Windows10Debloater-master", true);
                Directory.Delete($"{Paths.BasePath}\\Windows-10-Sophia-Script-master", true);
            }

            // Show a message prompting the user that a reboot is required (if any of these condition below is true).
            if (PatchExecutionCheck.HasDisabledUnnecessaryWindowsServices || PatchExecutionCheck.HasReducedMouseInputLatency || PatchExecutionCheck.HasOptimizedSystemProfile || PatchExecutionCheck.HasDebloatedWindows || PatchExecutionCheck.HasOptimizedNetworkOptions || PatchExecutionCheck.HasReducedCPUProcesses)
            {
                MessageBoxResult result = MessageBox.Show("Some changes require a reboot to take effect. Would you like reboot now?", "WindowsOptimizations", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                {
                    Process.Start("shutdown", "/r /t 0").Dispose();
                }
            }

            base.OnExit(e);
        }
    }
}
