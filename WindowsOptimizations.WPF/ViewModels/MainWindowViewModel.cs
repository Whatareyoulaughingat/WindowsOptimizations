using System.Reactive;
using System.Reactive.Concurrency;
using System.Windows;
using ReactiveUI;
using Splat;
using WindowsOptimizations.Core.GlobalData;
using WindowsOptimizations.Core.Handlers.Configuration;
using WindowsOptimizations.Core.Managers;
using WindowsOptimizations.Core.Optimizations.Input;
using WindowsOptimizations.Core.Tweaks.Input;
using WindowsOptimizations.Core.Tweaks.Network;
using WindowsOptimizations.Core.Tweaks.System;
using WindowsOptimizations.WPF.Views;

namespace WindowsOptimizations.WPF.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private CPUProcessOptimizations CPUProcessTweaks { get; set; }
        private MouseInputOptimizations MouseInputTweaks { get; set; }
        private InputLagOptimizations InputLagTweaks { get; set; }
        private NetworkOptimizations NetworkTweaks { get; set; }
        private SystemProfileOptimizations SystemProfileTweaks { get; set; }
        private ConfigurationHandler Configuration { get; set; }

        public MainWindowViewModel()
        {
            // Setup DI.
            Locator.CurrentMutable.RegisterConstant(new CPUProcessOptimizations());
            Locator.CurrentMutable.RegisterConstant(new MouseInputOptimizations());
            Locator.CurrentMutable.RegisterConstant(new InputLagOptimizations());
            Locator.CurrentMutable.RegisterConstant(new NetworkOptimizations());
            Locator.CurrentMutable.RegisterConstant(new SystemProfileOptimizations());
            Locator.CurrentMutable.RegisterConstant(new WindowsServiceOptimizations());
            Locator.CurrentMutable.RegisterConstant(new ConfigurationHandler());

            CPUProcessTweaks = Locator.Current.GetService<CPUProcessOptimizations>();
            MouseInputTweaks = Locator.Current.GetService<MouseInputOptimizations>();
            InputLagTweaks = Locator.Current.GetService<InputLagOptimizations>();
            NetworkTweaks = Locator.Current.GetService<NetworkOptimizations>();
            SystemProfileTweaks = Locator.Current.GetService<SystemProfileOptimizations>();
            Configuration = Locator.Current.GetService<ConfigurationHandler>();

            // Serialize and deserialize the unnecessary windows service file.
            Configuration.SerializeOnCreationAndDeserialize(Paths.DefaultUnnecessaryWindowsServicesJsonFile);

            // Setting up the commands.
            ManageUnnecessaryWindowsServicesCommand = ReactiveCommand.Create(DisableUnnecessaryWindowsServices);
            ReduceMouseInputLatencyCommand = ReactiveCommand.Create(ReduceMouseInputLatency);
            OptimizeSystemProfileCommand = ReactiveCommand.Create(OptimizeSystemProfile);
            OptimizeNetworkOptionsCommand = ReactiveCommand.Create(OptimizeNetworkOptions);
            ReduceCPUProcessesCommand = ReactiveCommand.Create(ReduceCPUProcesses);
            AboutCommand = ReactiveCommand.Create(About);
        }

        public ReactiveCommand<Unit, Unit> ManageUnnecessaryWindowsServicesCommand { get; private set; }
        public void DisableUnnecessaryWindowsServices()
            => RxApp.MainThreadScheduler.Schedule(() => WindowManager.ShowBlockingView<WindowsServicesApplier, WindowsServicesApplierViewModel>());

        public ReactiveCommand<Unit, Unit> ReduceMouseInputLatencyCommand { get; private set; }
        public void ReduceMouseInputLatency()
        {
            RxApp.TaskpoolScheduler.Schedule(() =>
            {
                MouseInputTweaks.DisablePointerAcceleration();
                MouseInputTweaks.SetPointerSensitivityToDefault();
                MouseInputTweaks.SetPointerSensitivityToDefault();
            });

            PatchExecutionCheck.HasReducedMouseInputLatency = true;
            MessageBox.Show("Operation completed sucessfully.", "Mouse Input Optimizations", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public ReactiveCommand<Unit, Unit> OptimizeSystemProfileCommand { get; private set; }
        public void OptimizeSystemProfile()
        {
            RxApp.TaskpoolScheduler.Schedule(() =>
            {
                SystemProfileTweaks.IncreaseSystemResponsiveness();
                SystemProfileTweaks.IncreaseGamePriority();
                SystemProfileTweaks.SetSchedulingCategoryToHigh();
                SystemProfileTweaks.SetSFIOPriorityToHigh();
            });

            PatchExecutionCheck.HasOptimizedSystemProfile = true;
            MessageBox.Show("Operation completed sucessfully.", "System Profile Optimizations", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public ReactiveCommand<Unit, Unit> OptimizeNetworkOptionsCommand { get; private set; }
        public void OptimizeNetworkOptions()
        {
            RxApp.TaskpoolScheduler.Schedule(() =>
            {
                NetworkTweaks.ChangeWindowAutoTuningLevel();
                NetworkTweaks.DisableWindowsScalingHeuristics();
                NetworkTweaks.ChangeCongestionProvider();
                NetworkTweaks.EnableRSS();
                NetworkTweaks.EnableRSC();
                NetworkTweaks.SetDefaultTTL();
                NetworkTweaks.DisableECN();
                NetworkTweaks.DisableChecksumOffloading();
                NetworkTweaks.DisableTCPChimneyOffload();
                NetworkTweaks.DisableLargeSendOffload();
                NetworkTweaks.DisableTCP1323Timestamps();
                NetworkTweaks.IncreaseHostResolutionPriority();
                NetworkTweaks.DecreaseMaxSYNRetransmissions();
                NetworkTweaks.DisableNonStackRttResiliency();
                NetworkTweaks.DisableNetworkThrottlingIndex();
                NetworkTweaks.DisableNaglesAlgorithm();
                NetworkTweaks.ChangeNetworkMemoryAllocations();
                NetworkTweaks.ConfigureDynamicPortAllocation();
                NetworkTweaks.DisableReservableBandwidthLimit();
            });

            PatchExecutionCheck.HasOptimizedNetworkOptions = true;
            MessageBox.Show("Operation completed sucessfully.", "Network Optimizations", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public ReactiveCommand<Unit, Unit> ReduceCPUProcessesCommand { get; private set; }
        public void ReduceCPUProcesses()
        {
            RxApp.TaskpoolScheduler.Schedule(() => CPUProcessTweaks.LimitSvcHostSplitting());

            PatchExecutionCheck.HasReducedCPUProcesses = true;
            MessageBox.Show("Operation completed sucessfully.", "CPU Process Optimizations", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public ReactiveCommand<Unit, Unit> ReduceInputLagCommand { get; private set; }
        public void ReduceInputLag()
        {
            RxApp.TaskpoolScheduler.Schedule(() =>
            {
                InputLagTweaks.OptimizePrioritySeperation();
                InputLagTweaks.DisableGameDVR();
            });

            PatchExecutionCheck.HasReducedInputLag = true;
            MessageBox.Show("Operation completed sucessfully.", "Input Lag Optimizations", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public ReactiveCommand<Unit, Unit> AboutCommand { get; private set; }
        public void About()
            => RxApp.MainThreadScheduler.Schedule(() => WindowManager.ShowBlockingView<About, AboutViewModel>());
    }
}
