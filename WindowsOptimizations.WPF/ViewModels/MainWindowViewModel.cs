using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using WindowsOptimizations.Core.GlobalData;
using WindowsOptimizations.Core.Patches;
using WindowsOptimizations.Core.Tools;

namespace WindowsOptimizations.UI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly TimerResolutionPatch timerResolutionPatch = new();

        private string timerResolutionMinimumValue;
        public string TimerResolutionMinimumValue
        {
            get { return timerResolutionMinimumValue; }
            set { SetProperty(ref timerResolutionMinimumValue, $"Minimum timer resolution value: {value}ms"); }
        }

        private string timerResolutionMaximumValue;
        public string TimerResolutionMaximumValue
        {
            get { return timerResolutionMaximumValue; }
            set { SetProperty(ref timerResolutionMaximumValue, $"Maximum timer resolution value: {value}ms"); }
        }

        private string timerResolutionCurrentValue;
        public string TimerResolutionCurrentValue
        {
            get { return timerResolutionCurrentValue; }
            set { SetProperty(ref timerResolutionCurrentValue, $"Current timer resolution value: {value}ms"); }
        }

        public MainWindowViewModel()
        {
            // Setting up the commands.
            DisableUnnecessaryWindowsServicesCommand = new DelegateCommand(async () => await DisableUnnecessaryWindowsServices());
            ReduceMouseInputLatencyCommand = new DelegateCommand(async () => await ReduceMouseInputLatency());
            OptimizeSystemProfileCommand = new DelegateCommand(async () => await OptimizeSystemProfile());
            SetSystemTimerToMaximumResolutionCommand = new DelegateCommand(async () => await SetSystemTimerToMaximumResolution());
            DebloatWindowsCommand = new DelegateCommand(async () => await DebloatWindows());
            OptimizeNetworkOptionsCommand = new DelegateCommand(async () => await OptimizeNetworkOptions());
            ReduceCPUProcessesCommand = new DelegateCommand(async () => await ReduceCPUProcesses());
            AboutCommand = new DelegateCommand(async () => await About());

            // Getting timer resolution info and assigning it to the appropriate variables.
            timerResolutionPatch.GetTimerResolutionInfo();
            TimerResolutionMinimumValue = TimeSpan.FromMilliseconds(timerResolutionPatch.MinimumResolution).TotalSeconds.ToString();
            TimerResolutionMaximumValue = TimeSpan.FromMilliseconds(timerResolutionPatch.MaximumResolution).TotalSeconds.ToString();
        }

        public DelegateCommand DisableUnnecessaryWindowsServicesCommand { get; internal set; }
        public static async Task DisableUnnecessaryWindowsServices()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(() => new WindowsServicePatch().DisableAllUnnecessaryServices());

            PatchExecutionCheck.HasDisabledUnnecessaryWindowsServices = true;
            MessageBox.Show("Operation completed sucessfully.", nameof(WindowsServicePatch), MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public DelegateCommand ReduceMouseInputLatencyCommand { get; internal set; }
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

        public DelegateCommand OptimizeSystemProfileCommand { get; internal set; }
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

        public DelegateCommand SetSystemTimerToMaximumResolutionCommand { get; internal set; }
        public async Task SetSystemTimerToMaximumResolution()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(() =>
            {
                timerResolutionPatch
                .GetTimerResolutionInfo() // Geting timer resolution info just in case something changed even though we did the same thing in the constructor of this class.
                .SetMaximumTimerResolutionValue();

                TimerResolutionMinimumValue = TimeSpan.FromMilliseconds(timerResolutionPatch.MinimumResolution).TotalSeconds.ToString();
                TimerResolutionMaximumValue = TimeSpan.FromMilliseconds(timerResolutionPatch.MaximumResolution).TotalSeconds.ToString();
                TimerResolutionCurrentValue = TimeSpan.FromMilliseconds(timerResolutionPatch.CurrentResolution).TotalSeconds.ToString();

                MessageBox.Show("This only works when this application is active. By the time you exit, the timer will return to its default resolution value.", nameof(SystemProfilePatch), MessageBoxButton.OK, MessageBoxImage.Warning);
            });
        }

        public DelegateCommand DebloatWindowsCommand { get; internal set; }
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

        public DelegateCommand OptimizeNetworkOptionsCommand { get; internal set; }
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

        public DelegateCommand ReduceCPUProcessesCommand { get; internal set; }
        public async Task ReduceCPUProcesses()
            => await Dispatcher.CurrentDispatcher.BeginInvoke(() => new CPUProcessPatch().LimitSvcHostSplitting());

        public DelegateCommand AboutCommand { get; internal set; }
        public static async Task About()
            => await Dispatcher.CurrentDispatcher.BeginInvoke(() => MessageBox.Show("Licensing:\n" + "This application is licensed under the MIT license.\n" + "\nAcknowledgements:\n" + "Windows10Debloater --> https://github.com/Sycnex/Windows10Debloater \n" + "Sophia Script --> https://github.com/farag2/Windows-10-Sophia-Script", "About"));
    }
}
