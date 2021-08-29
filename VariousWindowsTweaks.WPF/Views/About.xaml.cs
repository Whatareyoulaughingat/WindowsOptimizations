using System.Reactive.Disposables;
using ReactiveUI;
using WindowsOptimizations.WPF.ViewModels;

namespace WindowsOptimizations.WPF.Views
{
    /// <summary>
    /// Interaction logic for AboutView.xaml.
    /// </summary>
    public partial class About : ReactiveWindow<AboutViewModel>
    {
        public About()
        {
            InitializeComponent();
            ViewModel = new AboutViewModel();

            this.WhenActivated(disposableRegistration =>
            {
                this.OneWayBind(
                    ViewModel,
                    vmProperty => vmProperty.ApplicationLicenseInfo,
                    viewProperty => viewProperty.License.Text)
                .DisposeWith(disposableRegistration);

                this.OneWayBind(
                    ViewModel,
                    vmProperty => vmProperty.DisclaimerInfo,
                    viewProperty => viewProperty.Disclaimer.Text)
                .DisposeWith(disposableRegistration);
            });
        }
    }
}
