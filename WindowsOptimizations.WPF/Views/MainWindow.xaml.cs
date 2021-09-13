using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using ReactiveUI;
using WindowsOptimizations.Core.GlobalData;
using WindowsOptimizations.WPF.ViewModels;

namespace WindowsOptimizations.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();

            this.WhenActivated(disposableRegistration =>
            {
                // Bind all buttons of the view to the appropriate commands.
                this.BindCommand(
                    ViewModel,
                    vmProperty => vmProperty.ManageUnnecessaryWindowsServicesCommand,
                    viewProperty => viewProperty.ManageUnnecessaryWindowsServices)
                .DisposeWith(disposableRegistration);

                this.BindCommand(
                    ViewModel,
                    vmProperty => vmProperty.ReduceMouseInputLatencyCommand,
                    viewProperty => viewProperty.ReduceMouseInputLatency)
                .DisposeWith(disposableRegistration);

                this.BindCommand(
                    ViewModel,
                    vmProperty => vmProperty.OptimizeSystemProfileCommand,
                    viewProperty => viewProperty.OptimizeSystemProfile)
                .DisposeWith(disposableRegistration);

                this.BindCommand(
                    ViewModel,
                    vmProperty => vmProperty.OptimizeNetworkOptionsCommand,
                    viewProperty => viewProperty.OptimizeNetworkOptions)
                .DisposeWith(disposableRegistration);

                this.BindCommand(
                    ViewModel,
                    vmProperty => vmProperty.ReduceCPUProcessesCommand,
                    viewProperty => viewProperty.ReduceCPUProcesses)
                .DisposeWith(disposableRegistration);

                this.BindCommand(
                    ViewModel,
                    vmProperty => vmProperty.ReduceInputLagCommand,
                    viewProperty => viewProperty.ReduceInputLag)
                .DisposeWith(disposableRegistration);
                
                this.BindCommand(
                    ViewModel,
                    vmProperty => vmProperty.IncreaseGpuThreadPriorityCommand,
                    viewProperty => viewProperty.IncreaseGpuThreadPriority)
                .DisposeWith(disposableRegistration);

                this.BindCommand(
                    ViewModel,
                    vmProperty => vmProperty.AboutCommand,
                    viewProperty => viewProperty.About)
                .DisposeWith(disposableRegistration);

                this.Events().Closing
                    .Do(action =>
                    {
                        if (PatchExecutionCheck.HasDisabledUnnecessaryWindowsServices ||
                            PatchExecutionCheck.HasReducedMouseInputLatency ||
                            PatchExecutionCheck.HasOptimizedSystemProfile ||
                            PatchExecutionCheck.HasOptimizedNetworkOptions ||
                            PatchExecutionCheck.HasReducedCPUProcesses ||
                            PatchExecutionCheck.HasReducedInputLag)
                        {
                            MessageBoxResult result = MessageBox.Show("Some changes require a reboot to take effect. Would you like reboot now?", "Windows Optimizations", MessageBoxButton.YesNo, MessageBoxImage.Information);

                            if (result == MessageBoxResult.Yes)
                            {
                                Process.Start("shutdown", "/r /t 0").Dispose();
                            }
                        }
                    });
            });
        }
    }
}
