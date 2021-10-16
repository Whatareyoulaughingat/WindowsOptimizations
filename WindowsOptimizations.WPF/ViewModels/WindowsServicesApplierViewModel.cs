using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using Microsoft.Win32;
using ReactiveUI;
using Splat;
using WindowsOptimizations.Core.Handlers.Configuration;
using WindowsOptimizations.Core.Models;
using WindowsOptimizations.Core.Optimizations.System;
using System.Reactive.Linq;
using WindowsOptimizations.Core.Extensions;

namespace WindowsOptimizations.WPF.ViewModels
{
    public class WindowsServicesApplierViewModel : ReactiveObject
    {
        private ObservableCollection<WindowsService> unnecessaryServices = new();
        public ObservableCollection<WindowsService> UnnecessaryServices
        {
            get { return unnecessaryServices; }
            set { this.RaiseAndSetIfChanged(ref unnecessaryServices, value); }
        }

        private bool hasSelectedAll;
        public bool HasSelectedAll
        {
            get { return hasSelectedAll; }
            set { this.RaiseAndSetIfChanged(ref hasSelectedAll, value); }
        }

        private WindowsServiceOptimizations WindowsServiceOptimizations { get; set; } = Locator.GetLocator().GetAnyService<WindowsServiceOptimizations>();
        private ConfigurationHandler Configuration { get; set; } = Locator.GetLocator().GetAnyService<ConfigurationHandler>();

        public WindowsServicesApplierViewModel()
        {
            // Setup commands.
            SelectAllCommand = ReactiveCommand.Create(SelectAll);
            UseCustomCollectionCommand = ReactiveCommand.Create(UseCustomCollection);
            DisableSelectedServicesCommand = ReactiveCommand.Create(DisableSelectedServices);
            EnableSelectedServicesCommand = ReactiveCommand.Create(EnableSelectedServices);

            // Populate the listview.
            foreach (string service in Configuration.CurrentWindowsServicesDataInstance.ServiceCollection)
            {
                UnnecessaryServices.Add(new WindowsService
                {
                    Name = service,
                });
            }
        }

        public ReactiveCommand<Unit, Unit> UseCustomCollectionCommand { get; private set; }
        public void UseCustomCollection()
        {
            RxApp.MainThreadScheduler.Schedule(() =>
            {
                OpenFileDialog fileDialog = new()
                {
                    Title = "Select a custom JSON collection",
                    CheckFileExists = true,
                    CheckPathExists = true,
                    Filter = "JSON File (*.json) | *.json",
                    Multiselect = false,
                };

                bool? result = fileDialog.ShowDialog();

                if (result == true)
                {
                    UnnecessaryServices.Clear();

                    // Add the new custom items to the collection.
                    Configuration.DeserializeAsync(fileDialog.FileName);

                    foreach (string service in Configuration.CurrentWindowsServicesDataInstance.ServiceCollection)
                    {
                        UnnecessaryServices.Add(new WindowsService
                        {
                            Name = service
                        });
                    }
                }
            });
        }

        public ReactiveCommand<Unit, Unit> SelectAllCommand { get; private set; }
        public void SelectAll()
        {
            RxApp.MainThreadScheduler.Schedule(() =>
            {
                if (HasSelectedAll)
                {
                    foreach (WindowsService service in UnnecessaryServices)
                    {
                        service.IsSelected = true;
                    }
                }
                else
                {
                    foreach (WindowsService service in UnnecessaryServices)
                    {
                        service.IsSelected = false;
                    }
                }
            });
        }

        public ReactiveCommand<Unit, Unit> DisableSelectedServicesCommand { get; private set; }
        public void DisableSelectedServices()
        {
            RxApp.TaskpoolScheduler.Schedule(() =>
            {
                foreach (WindowsService service in UnnecessaryServices.Where(x => x.IsSelected))
                {
                    WindowsServiceOptimizations.DisableService(service);
                }
            });
        }

        public ReactiveCommand<Unit, Unit> EnableSelectedServicesCommand { get; private set; }
        public void EnableSelectedServices()
        {
            RxApp.TaskpoolScheduler.Schedule(() =>
            {
                foreach (WindowsService service in UnnecessaryServices.Where(x => x.IsSelected))
                {
                    WindowsServiceOptimizations.EnableService(service);
                }
            });
        }
    }
}
