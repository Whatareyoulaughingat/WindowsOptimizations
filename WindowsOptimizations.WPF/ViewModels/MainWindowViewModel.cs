using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WindowsOptimizations.Core.GlobalData;
using WindowsOptimizations.Core.Patches;
using WindowsOptimizations.Core.Tools;
using WindowsOptimizations.WPF.Views;

namespace WindowsOptimizations.UI.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private readonly TimerResolutionPatch timerResolutionPatch = new();

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
            timerResolutionPatch.GetTimerResolutionInfo();
            TimerResolutionMinimumValue = (timerResolutionPatch.MinimumResolution * 0.0001).ToString();
            TimerResolutionMaximumValue = (timerResolutionPatch.MaximumResolution * 0.0001).ToString();
        }

        public ReactiveCommand<Unit, Unit> DisableUnnecessaryWindowsServicesCommand { get; private set; }
        public static async Task DisableUnnecessaryWindowsServices()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(() => new WindowsServicesApplier().ShowDialog());
        }

        public ReactiveCommand<Unit, Unit> ReduceMouseInputLatencyCommand { get; private set; }
        public static async Task ReduceMouseInputLatency()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(() =>
            {
                new RawMouseInputPatch()
                .DisablePointerAcceleration()
                .SetPointerSensitivityToDefault()
                .SetOneToOnePointerPrecision();

                PatchExecutionCheck.HasReducedMouseInputLatency = true;
                MessageBox.Show("Operation completed sucessfully.", nameof(SystemProfilePatch), MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }

        public ReactiveCommand<Unit, Unit> OptimizeSystemProfileCommand { get; private set; }
        public static async Task OptimizeSystemProfile()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(() =>
            {
                new SystemProfilePatch()
                .IncreaseGamePriority()
                .IncreaseSystemResponsiveness()
                .SetSchedulingCategoryToHigh()
                .SetSFIOPriorityToHigh();

                PatchExecutionCheck.HasOptimizedSystemProfile = true;
                MessageBox.Show("Operation completed sucessfully.", nameof(SystemProfilePatch), MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }

        public ReactiveCommand<Unit, Unit> SetSystemTimerToMaximumResolutionCommand { get; private set; }
        public async Task SetSystemTimerToMaximumResolution()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(() =>
            {
                timerResolutionPatch
                .GetTimerResolutionInfo() // Getting the timer resolution info again (even though we did in the constructor), just in case something has changed.
                .SetMaximumTimerResolutionValue();

                TimerResolutionMinimumValue = (timerResolutionPatch.MinimumResolution * 0.0001).ToString();
                TimerResolutionMaximumValue = (timerResolutionPatch.MaximumResolution * 0.0001).ToString();
                TimerResolutionCurrentValue = (timerResolutionPatch.CurrentResolution * 0.0001).ToString();

                MessageBox.Show("This only works when this application is active. By the time you exit, the timer will return to its default resolution value.", nameof(SystemProfilePatch), MessageBoxButton.OK, MessageBoxImage.Warning);
            });
        }

        public ReactiveCommand<Unit, Unit> DebloatWindowsCommand { get; private set; }
        public static async Task DebloatWindows()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(async () =>
            {
                Debloater debloater = new();

                await debloater
                .SetUnrestrictedExecutionPolicy()
                .ContinueWith(async x => await debloater.DebloatWindowsFirstPhaseAsync())
                .ContinueWith(async x => await debloater.DebloatWindowsSecondPhase());

                PatchExecutionCheck.HasDebloatedWindows = true;
            });
        }

        public ReactiveCommand<Unit, Unit> OptimizeNetworkOptionsCommand { get; private set; }
        public static async Task OptimizeNetworkOptions()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(() =>
            {
                new NetworkPatch()
                .ChangeWindowAutoTuningLevel()
                .DisableWindowsScalingHeuristics()
                .ChangeCongestionProvider()
                .EnableRSS()
                .EnableRSC()
                .SetDefaultTTL()
                .DisableECN()
                .DisableChecksumOffloading()
                .DisableTCPChimneyOffload()
                .DisableLargeSendOffload()
                .DisableTCP1323Timestamps()
                .IncreaseHostResolutionPriority()
                .DecreaseMaxSYNRetransmissions()
                .DisableNonStackRttResiliency()
                .DisableNetworkThrottlingIndex()
                .DisableNaglesAlgorithm()
                .ChangeNetworkMemoryAllocations()
                .ConfigureDynamicPortAllocation()
                .DisableReservableBandwidthLimit();

                PatchExecutionCheck.HasOptimizedNetworkOptions = true;
                MessageBox.Show("Operation completed sucessfully.", nameof(NetworkPatch), MessageBoxButton.OK, MessageBoxImage.Information);
            });
        }

        public ReactiveCommand<Unit, Unit> ReduceCPUProcessesCommand { get; private set; }
        public async Task ReduceCPUProcesses()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(() => new CPUProcessPatch().LimitSvcHostSplitting());

            PatchExecutionCheck.HasReducedCPUProcesses = true;
            MessageBox.Show("Operation completed sucessfully.", nameof(CPUProcessPatch), MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public ReactiveCommand<Unit, Unit> AboutCommand { get; private set; }
        public static async Task About()
            => await Dispatcher.CurrentDispatcher.BeginInvoke(() => MessageBox.Show("Licensing:\n" + "This application is licensed under the MIT license.\n" + "\nAcknowledgements:\n" + "Windows10Debloater --> https://github.com/Sycnex/Windows10Debloater \n" + "Sophia Script --> https://github.com/farag2/Windows-10-Sophia-Script", "About"));
    }
}
