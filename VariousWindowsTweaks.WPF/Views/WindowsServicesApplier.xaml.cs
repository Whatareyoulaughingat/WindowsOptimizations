using System.Reactive.Disposables;
using ReactiveUI;
using WindowsOptimizations.WPF.ViewModels;

namespace WindowsOptimizations.WPF.Views
{
    /// <summary>
    /// Interaction logic for WindowsServicesApplier.xaml.
    /// </summary>
    public partial class WindowsServicesApplier : ReactiveWindow<WindowsServicesApplierViewModel>
    {
        public WindowsServicesApplier()
        {
            InitializeComponent();
            ViewModel = new WindowsServicesApplierViewModel();

            this.WhenActivated(disposableRegistration =>
            {
                // Bind the listview control.
                this.OneWayBind(
                    ViewModel,
                    vmProperty => vmProperty.UnnecessaryServices,
                    viewProperty => viewProperty.WindowsServiceCollection.ItemsSource)
                .DisposeWith(disposableRegistration);

                // Bind the state of the checkbox.
                this.Bind(
                    ViewModel,
                    vmProperty => vmProperty.HasSelectedAll,
                    viewProperty => viewProperty.SelectAllWindowsServices.IsChecked)
                .DisposeWith(disposableRegistration);

                this.BindCommand(
                    ViewModel,
                    vmProperty => vmProperty.SelectAllCommand,
                    viewProperty => viewProperty.SelectAllWindowsServices)
                .DisposeWith(disposableRegistration);

                // Bind the button where the user can use a custom collection.
                this.BindCommand(
                    ViewModel,
                    vmProperty => vmProperty.UseCustomCollectionCommand,
                    viewProperty => viewProperty.UseCustomJSONCollection)
                .DisposeWith(disposableRegistration);

                // Bind the 2 buttons where the services get enabled or disabled.
                this.BindCommand(
                    ViewModel,
                    vmProperty => vmProperty.DisableSelectedServicesCommand,
                    viewProperty => viewProperty.DisableSelectedServices)
                .DisposeWith(disposableRegistration);

                this.BindCommand(
                    ViewModel,
                    vmProperty => vmProperty.EnableSelectedServicesCommand,
                    viewProperty => viewProperty.EnableSelectedServices)
                .DisposeWith(disposableRegistration);
            });
        }
    }
}
