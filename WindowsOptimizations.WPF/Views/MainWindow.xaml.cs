using ReactiveUI;
using System.Reactive.Disposables;
using WindowsOptimizations.WPF.ViewModels;

namespace WindowsOptimizations.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
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
                    vmProperty => vmProperty.DisableUnnecessaryWindowsServicesCommand,
                    viewProperty => viewProperty.DisableUnnecessaryWindowsServices)
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
                    vmProperty => vmProperty.SetSystemTimerToMaximumResolutionCommand,
                    viewProperty => viewProperty.SetSystemTimerToMaximumResolution)
                .DisposeWith(disposableRegistration);

                this.BindCommand(
                    ViewModel,
                    vmProperty => vmProperty.DebloatWindowsCommand,
                    viewProperty => viewProperty.DebloatWindows)
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
                    vmProperty => vmProperty.AboutCommand,
                    viewProperty => viewProperty.About)
                .DisposeWith(disposableRegistration);

                // One-way bind the timer resolution labels.
                this.OneWayBind(
                    ViewModel,
                    vmProperty => vmProperty.TimerResolutionMinimumValue,
                    viewProperty => viewProperty.TimerResolutionMinimumValueLabel.Content)
                .DisposeWith(disposableRegistration);

                this.OneWayBind(
                    ViewModel,
                    vmProperty => vmProperty.TimerResolutionMaximumValue,
                    viewProperty => viewProperty.TimerResolutionMaximumValueLabel.Content)
                .DisposeWith(disposableRegistration);

                this.OneWayBind(
                    ViewModel,
                    vmProperty => vmProperty.TimerResolutionCurrentValue,
                    viewProperty => viewProperty.TimerResolutionCurrentValueLabel.Content)
                .DisposeWith(disposableRegistration);
            });
        }
    }
}
