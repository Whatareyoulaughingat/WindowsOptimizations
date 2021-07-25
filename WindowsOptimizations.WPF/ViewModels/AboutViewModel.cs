using ReactiveUI;

namespace WindowsOptimizations.WPF.ViewModels
{
    /// <summary>
    /// The viewmodel of <see cref="Views.About"/>.
    /// </summary>
    public class AboutViewModel : ReactiveObject
    {
        private string applicationLicenseInfo;
        public string ApplicationLicenseInfo
        {
            get { return applicationLicenseInfo; }
            set { this.RaiseAndSetIfChanged(ref applicationLicenseInfo, value); }
        }

        private string disclaimerInfo;
        public string DisclaimerInfo
        {
            get { return disclaimerInfo; }
            set { this.RaiseAndSetIfChanged(ref disclaimerInfo, value); }
        }

        public AboutViewModel()
        {
            ApplicationLicenseInfo = "This application is licensed under the MIT license.";
            DisclaimerInfo = "This application is not affliated with Windows or Microsoft itself in any way.";
        }
    }
}
