using System.Reactive.Disposables;
using ReactiveUI;
using WindowsOptimizations.WPF.ViewModels;

namespace WindowsOptimizations.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
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
            });
        }
    }
}
