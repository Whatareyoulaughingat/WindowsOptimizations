using Microsoft.Win32;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Threading;
using WindowsOptimizations.Core.Handlers.Configuration;
using WindowsOptimizations.Core.Models;
using WindowsOptimizations.Core.Patches;

namespace WindowsOptimizations.WPF.ViewModels
{
    public class WindowsServicesApplierViewModel : ReactiveObject
    {
        private readonly WindowsServicePatch windowsServicePatch = new();

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

        public WindowsServicesApplierViewModel()
        {
            // Create commands.
            SelectAllCommand = ReactiveCommand.CreateFromTask(async () => await SelectAll());
            UseCustomCollectionCommand = ReactiveCommand.CreateFromTask(async () => await UseCustomCollection());
            DisableSelectedServicesCommand = ReactiveCommand.CreateFromTask(async () => await DisableSelectedServices());
            EnableSelectedServicesCommand = ReactiveCommand.CreateFromTask(async () => await EnableSelectedServices());

            // Populate the listview.
            for (int i = 0; i < ConfigurationHandler.WindowsServicesDataInstance.ServiceCollection.Length; i++)
            {
                UnnecessaryServices.Add(new WindowsService()
                {
                    Name = ConfigurationHandler.WindowsServicesDataInstance.ServiceCollection[i],
                });
            } 
        }

        public ReactiveCommand<Unit, Unit> UseCustomCollectionCommand { get; private set; }
        public async Task UseCustomCollection()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(async () =>
            {
                OpenFileDialog fileDialog = new()
                {
                    Title = "Select a custom JSON collection",
                    CheckFileExists = true,
                    CheckPathExists = true,
                    DefaultExt = ".json",
                    Multiselect = false,
                };

                fileDialog.ShowDialog();

                await ConfigurationHandler.DeserializeAsync(fileDialog.FileName);

                // Remove all default items from the collection.
                for (int i = 0; i < UnnecessaryServices.Count; i++)
                {
                    UnnecessaryServices.RemoveAt(i);
                }

                // Add the new custom items to the collection.
                for (int i = 0; i < ConfigurationHandler.WindowsServicesDataInstance.ServiceCollection.Length; i++)
                {
                    UnnecessaryServices.Add(new WindowsService()
                    {
                        Name = ConfigurationHandler.WindowsServicesDataInstance.ServiceCollection[i],
                    });
                }
            });
        }

        public ReactiveCommand<Unit, Unit> SelectAllCommand { get; private set; }
        public async Task SelectAll()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(() =>
            {
                if (HasSelectedAll)
                {
                    for (int i = 0; i < UnnecessaryServices.Count; i++)
                    {
                        UnnecessaryServices[i].IsSelected = true;
                    }
                }
                else
                {
                    for (int i = 0; i < UnnecessaryServices.Count; i++)
                    {
                        UnnecessaryServices[i].IsSelected = false;
                    }
                }
            });
        }

        public ReactiveCommand<Unit, Unit> DisableSelectedServicesCommand { get; private set; }
        public async Task DisableSelectedServices()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(() =>
            {
                for (int i = 0; i < UnnecessaryServices.Count; i++)
                {
                    if (UnnecessaryServices[i].IsSelected)
                    {
                        windowsServicePatch.DisableService(UnnecessaryServices[i]);
                    }
                }
            });
        }

        public ReactiveCommand<Unit, Unit> EnableSelectedServicesCommand { get; private set; }
        public async Task EnableSelectedServices()
        {
            await Dispatcher.CurrentDispatcher.BeginInvoke(() =>
            {
                for (int i = 0; i < UnnecessaryServices.Count; i++)
                {
                    if (UnnecessaryServices[i].IsSelected)
                    {
                        windowsServicePatch.EnableService(UnnecessaryServices[i]);
                    }
                }
            });
        }
    }
}
