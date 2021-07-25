using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ReactiveUI;
using WindowsOptimizations.Core.GlobalData;
using WindowsOptimizations.Core.Managers;
using WindowsOptimizations.Core.Tweaks;
using WindowsOptimizations.WPF.Views;

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
        public PatchExecutionCheck PatchExecutionCheck
        {
            get { return patchExecutionCheck; }
            set { this.RaiseAndSetIfChanged(ref patchExecutionCheck, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            // Setting up the commands.
            DisableUnnecessaryWindowsServicesCommand = ReactiveCommand.CreateFromTask(async () => await DisableUnnecessaryWindowsServices().ConfigureAwait(false));
            ReduceMouseInputLatencyCommand = ReactiveCommand.CreateFromTask(async () => await ReduceMouseInputLatency().ConfigureAwait(false));
            OptimizeSystemProfileCommand = ReactiveCommand.CreateFromTask(async () => await OptimizeSystemProfile().ConfigureAwait(false));
            OptimizeNetworkOptionsCommand = ReactiveCommand.CreateFromTask(async () => await OptimizeNetworkOptions().ConfigureAwait(false));
            ReduceCPUProcessesCommand = ReactiveCommand.CreateFromTask(async () => await ReduceCPUProcesses().ConfigureAwait(false));
            AboutCommand = ReactiveCommand.CreateFromTask(async () => await About().ConfigureAwait(false));
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
                    RawMouseInputTweaks.DisablePointerAcceleration(),
                    RawMouseInputTweaks.SetPointerSensitivityToDefault(),
                    RawMouseInputTweaks.SetPointerSensitivityToDefault());

                PatchExecutionCheck.HasReducedMouseInputLatency = true;
                MessageBox.Show("Operation completed sucessfully.", nameof(SystemProfileTweaks), MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }

        public ReactiveCommand<Unit, Unit> OptimizeSystemProfileCommand { get; private set; }
        public async Task OptimizeSystemProfile()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(async () =>
            {
                await Task.WhenAll(
                    SystemProfileTweaks.IncreaseSystemResponsiveness(),
                    SystemProfileTweaks.IncreaseGamePriority(),
                    SystemProfileTweaks.SetSchedulingCategoryToHigh(),
                    SystemProfileTweaks.SetSFIOPriorityToHigh());

                PatchExecutionCheck.HasOptimizedSystemProfile = true;
                MessageBox.Show("Operation completed sucessfully.", nameof(SystemProfileTweaks), MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }

        public ReactiveCommand<Unit, Unit> OptimizeNetworkOptionsCommand { get; private set; }
        public async Task OptimizeNetworkOptions()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(async () =>
            {
                await Task.WhenAll(
                    NetworkTweaks.ChangeWindowAutoTuningLevel(),
                    NetworkTweaks.DisableWindowsScalingHeuristics(),
                    NetworkTweaks.ChangeCongestionProvider(),
                    NetworkTweaks.EnableRSS(),
                    NetworkTweaks.EnableRSC(),
                    NetworkTweaks.SetDefaultTTL(),
                    NetworkTweaks.DisableECN(),
                    NetworkTweaks.DisableChecksumOffloading(),
                    NetworkTweaks.DisableTCPChimneyOffload(),
                    NetworkTweaks.DisableLargeSendOffload(),
                    NetworkTweaks.DisableTCP1323Timestamps(),
                    NetworkTweaks.IncreaseHostResolutionPriority(),
                    NetworkTweaks.DecreaseMaxSYNRetransmissions(),
                    NetworkTweaks.DisableNonStackRttResiliency(),
                    NetworkTweaks.DisableNetworkThrottlingIndex(),
                    NetworkTweaks.DisableNaglesAlgorithm(),
                    NetworkTweaks.ChangeNetworkMemoryAllocations(),
                    NetworkTweaks.ConfigureDynamicPortAllocation(),
                    NetworkTweaks.DisableReservableBandwidthLimit());

                PatchExecutionCheck.HasOptimizedNetworkOptions = true;
                MessageBox.Show("Operation completed sucessfully.", "Network Patch", MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }

        public ReactiveCommand<Unit, Unit> ReduceCPUProcessesCommand { get; private set; }
        public async Task ReduceCPUProcesses()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(async () => await CPUProcessTweaks.LimitSvcHostSplitting());

            PatchExecutionCheck.HasReducedCPUProcesses = true;
            MessageBox.Show("Operation completed sucessfully.", "CPU Process Patch", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public ReactiveCommand<Unit, Unit> AboutCommand { get; private set; }
        public async Task About()
            => await Dispatcher.CurrentDispatcher.BeginInvoke(() => WindowManager.ShowBlockingView<About, AboutViewModel>());
    }
}
