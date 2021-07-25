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
        protected override async void OnStartup(StartupEventArgs e)
        {
            Directory.CreateDirectory(Paths.Base);

            await ConfigurationHandler.SerializeOnCreationAndDeserialize(Paths.UnnecessaryWindowsServicesJsonFile);

            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (PatchExecutionCheck.HasDisabledUnnecessaryWindowsServices ||
                PatchExecutionCheck.HasReducedMouseInputLatency ||
                PatchExecutionCheck.HasOptimizedSystemProfile ||
                PatchExecutionCheck.HasDebloatedWindows ||
                PatchExecutionCheck.HasOptimizedNetworkOptions ||
                PatchExecutionCheck.HasReducedCPUProcesses)
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
