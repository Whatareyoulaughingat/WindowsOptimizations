using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ReactiveUI;
using WindowsOptimizations.Core.GlobalData;
using WindowsOptimizations.Core.Managers;
using WindowsOptimizations.Core.Patches;
using WindowsOptimizations.Core.Tools;
using WindowsOptimizations.WPF.Views;

#pragma warning disable SA1600
#pragma warning disable SA1201
#pragma warning disable CA1822

// Source: https://stackoverflow.com/questions/4525854/remove-trailing-zeros --> ToString("G29")
namespace WindowsOptimizations.WPF.ViewModels
{
    /// <summary>
    /// The viewmodel of <see cref="MainWindow"/>.
    /// </summary>
    public class MainWindowViewModel : ReactiveObject
    {
        private string timerResolutionMinimumValue;
        public string TimerResolutionMinimumValue
        {
            get { return timerResolutionMinimumValue; }
            set { this.RaiseAndSetIfChanged(ref timerResolutionMinimumValue, $"Minimum timer resolution value: {value}ms"); }
        }

        private string timerResolutionMaximumValue;
        public string TimerResolutionMaximumValue
        {
            get { return timerResolutionMaximumValue; }
            set { this.RaiseAndSetIfChanged(ref timerResolutionMaximumValue, $"Maximum timer resolution value: {value}ms"); }
        }

        private string timerResolutionCurrentValue;
        public string TimerResolutionCurrentValue
        {
            get { return timerResolutionCurrentValue; }
            set { this.RaiseAndSetIfChanged(ref timerResolutionCurrentValue, $"Current timer resolution value: {value}ms"); }
        }

        private PatchExecutionCheck patchExecutionCheck = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            // Setting up the commands.
            DisableUnnecessaryWindowsServicesCommand = ReactiveCommand.CreateFromTask(async () => await DisableUnnecessaryWindowsServices());
            ReduceMouseInputLatencyCommand = ReactiveCommand.CreateFromTask(async () => await ReduceMouseInputLatency());
            OptimizeSystemProfileCommand = ReactiveCommand.CreateFromTask(async () => await OptimizeSystemProfile());
            SetSystemTimerToMaximumResolutionCommand = ReactiveCommand.CreateFromTask(async () => await SetSystemTimerToMaximumResolution());
            DebloatWindowsCommand = ReactiveCommand.CreateFromTask(async () => await DebloatWindows());
            OptimizeNetworkOptionsCommand = ReactiveCommand.CreateFromTask(async () => await OptimizeNetworkOptions());
            ReduceCPUProcessesCommand = ReactiveCommand.CreateFromTask(async () => await ReduceCPUProcesses());
            AboutCommand = ReactiveCommand.CreateFromTask(async () => await About());

            // Getting timer resolution info and assigning it to the appropriate variables.
            TimerResolutionPatch.GetTimerResolutionInfo();
            TimerResolutionMinimumValue = (TimerResolutionPatch.MinimumResolution * 0.0001m).ToString("G29");
            TimerResolutionMaximumValue = (TimerResolutionPatch.MaximumResolution * 0.0001m).ToString("G29");
            TimerResolutionCurrentValue = (TimerResolutionPatch.CurrentResolution * 0.0001m).ToString("G29");
        }

        public ReactiveCommand<Unit, Unit> DisableUnnecessaryWindowsServicesCommand { get; private set; }
        public static async Task DisableUnnecessaryWindowsServices()
            => await Dispatcher.CurrentDispatcher.BeginInvoke(() => WindowManager.ShowBlockingView<WindowsServicesApplier, WindowsServicesApplierViewModel>());

        public ReactiveCommand<Unit, Unit> ReduceMouseInputLatencyCommand { get; private set; }
        public async Task ReduceMouseInputLatency()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(async () =>
            {
                await Task.WhenAll(
                    RawMouseInputPatch.DisablePointerAcceleration(),
                    RawMouseInputPatch.SetPointerSensitivityToDefault(),
                    RawMouseInputPatch.SetPointerSensitivityToDefault());

                patchExecutionCheck.HasReducedMouseInputLatency = true;
                MessageBox.Show("Operation completed sucessfully.", nameof(SystemProfilePatch), MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }

        public ReactiveCommand<Unit, Unit> OptimizeSystemProfileCommand { get; private set; }
        public async Task OptimizeSystemProfile()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(async () =>
            {
                await Task.WhenAll(
                    SystemProfilePatch.IncreaseSystemResponsiveness(),
                    SystemProfilePatch.IncreaseGamePriority(),
                    SystemProfilePatch.SetSchedulingCategoryToHigh(),
                    SystemProfilePatch.SetSFIOPriorityToHigh());

                patchExecutionCheck.HasOptimizedSystemProfile = true;
                MessageBox.Show("Operation completed sucessfully.", nameof(SystemProfilePatch), MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }

        public ReactiveCommand<Unit, Unit> SetSystemTimerToMaximumResolutionCommand { get; private set; }
        public async Task SetSystemTimerToMaximumResolution()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(async () =>
            {
                await Task.WhenAll(
                    TimerResolutionPatch.GetTimerResolutionInfo(),
                    TimerResolutionPatch.SetMaximumTimerResolutionValue());

                TimerResolutionMinimumValue = (TimerResolutionPatch.MinimumResolution * 0.0001m).ToString("G29");
                TimerResolutionMaximumValue = (TimerResolutionPatch.MaximumResolution * 0.0001m).ToString("G29");
                TimerResolutionCurrentValue = (TimerResolutionPatch.CurrentResolution * 0.0001m).ToString("G29");

                MessageBox.Show("This only works when this application is active. By the time you exit, the timer will return to its default resolution value.", "System Profile Patch", MessageBoxButton.OK, MessageBoxImage.Warning);
            });
        }

        public ReactiveCommand<Unit, Unit> DebloatWindowsCommand { get; private set; }
        public async Task DebloatWindows()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(async () =>
            {
                await Debloater
                .SetUnrestrictedExecutionPolicy()
                .ContinueWith(async x => await Debloater.DebloatWindowsFirstPhaseAsync())
                .ContinueWith(async x => await Debloater.DebloatWindowsSecondPhase());

                patchExecutionCheck.HasDebloatedWindows = true;
            });
        }

        public ReactiveCommand<Unit, Unit> OptimizeNetworkOptionsCommand { get; private set; }
        public async Task OptimizeNetworkOptions()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(async () =>
            {
                await Task.WhenAll(
                    NetworkPatch.ChangeWindowAutoTuningLevel(),
                    NetworkPatch.DisableWindowsScalingHeuristics(),
                    NetworkPatch.ChangeCongestionProvider(),
                    NetworkPatch.EnableRSS(),
                    NetworkPatch.EnableRSC(),
                    NetworkPatch.SetDefaultTTL(),
                    NetworkPatch.DisableECN(),
                    NetworkPatch.DisableChecksumOffloading(),
                    NetworkPatch.DisableTCPChimneyOffload(),
                    NetworkPatch.DisableLargeSendOffload(),
                    NetworkPatch.DisableTCP1323Timestamps(),
                    NetworkPatch.IncreaseHostResolutionPriority(),
                    NetworkPatch.DecreaseMaxSYNRetransmissions(),
                    NetworkPatch.DisableNonStackRttResiliency(),
                    NetworkPatch.DisableNetworkThrottlingIndex(),
                    NetworkPatch.DisableNaglesAlgorithm(),
                    NetworkPatch.ChangeNetworkMemoryAllocations(),
                    NetworkPatch.ConfigureDynamicPortAllocation(),
                    NetworkPatch.DisableReservableBandwidthLimit());

                patchExecutionCheck.HasOptimizedNetworkOptions = true;
                MessageBox.Show("Operation completed sucessfully.", "Network Patch", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }

        public ReactiveCommand<Unit, Unit> ReduceCPUProcessesCommand { get; private set; }
        public async Task ReduceCPUProcesses()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(async () => await CPUProcessPatch.LimitSvcHostSplitting());

            patchExecutionCheck.HasReducedCPUProcesses = true;
            MessageBox.Show("Operation completed sucessfully.", "CPU Process Patch", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public ReactiveCommand<Unit, Unit> AboutCommand { get; private set; }
        public async Task About()
            => await Dispatcher.CurrentDispatcher.BeginInvoke(() => WindowManager.ShowBlockingView<About, AboutViewModel>());
    }
}
