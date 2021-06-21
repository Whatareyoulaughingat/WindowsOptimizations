using System.Diagnostics;
using System.IO;
using System.Windows;
using WindowsOptimizations.Core.GlobalData;

namespace WindowsOptimizations
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Directory.CreateDirectory(Paths.BasePath);
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Delete the downloaded debloaters.
            try
            {
                Directory.Delete($"{Paths.BasePath}\\Windows10Debloater-master");
                Directory.Delete($"{Paths.BasePath}\\Windows-10-Sophia-Script-master");
            }
            catch
            {
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
