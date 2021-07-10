using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using ReactiveUI;
using Splat;
using WindowsOptimizations.Core.GlobalData;
using WindowsOptimizations.Core.Handlers.Configuration;

namespace WindowsOptimizations.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        private readonly Paths paths = new();

        protected override async void OnStartup(StartupEventArgs e)
        {
            Directory.CreateDirectory(paths.BasePath);

            await ConfigurationHandler.SerializeOnCreationAndDeserialize(paths.UnnecessaryWindowsServicesJsonFile);

            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // Delete the downloaded debloaters.
            if (Directory.Exists($"{paths.BasePath}\\Windows10Debloater-master") && Directory.Exists($"{paths.BasePath}\\Windows-10-Sophia-Script-master"))
            {
                Directory.Delete($"{paths.BasePath}\\Windows10Debloater-master", true);
                Directory.Delete($"{paths.BasePath}\\Windows-10-Sophia-Script-master", true);
            }

            // Show a message prompting the user that a reboot is required (if any of these condition below is true).
            PatchExecutionCheck patchExecutionCheck = new();

            if (patchExecutionCheck.HasDisabledUnnecessaryWindowsServices || patchExecutionCheck.HasReducedMouseInputLatency || patchExecutionCheck.HasOptimizedSystemProfile || patchExecutionCheck.HasDebloatedWindows || patchExecutionCheck.HasOptimizedNetworkOptions || patchExecutionCheck.HasReducedCPUProcesses)
            {
                MessageBoxResult result = MessageBox.Show("Some changes require a reboot to take effect. Would you like reboot now?", "Windows Optimizations", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                {
                    Process.Start("shutdown", "/r /t 0").Dispose();
                }
            }

            base.OnExit(e);
        }
    }
}
