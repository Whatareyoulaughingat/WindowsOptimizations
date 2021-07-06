using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace WindowsOptimizations.WPF.ViewModels
{
    /// <summary>
    /// The viewmodel of <see cref="Views.About"/>.
    /// </summary>
    public class AboutViewModel : ReactiveObject
    {
        [Reactive]
        public string ApplicationLicenseInfo { get; set; }

        [Reactive]
        public string DisclaimerInfo { get; set; }

        [Reactive]
        public string AcknowledgementsInfo { get; set; }

        public AboutViewModel()
        {
            ApplicationLicenseInfo = "This application is licensed under the MIT license.";

            DisclaimerInfo = "This application is not affliated with Windows10Debloater or SophiaScript or any other application.";

            AcknowledgementsInfo = "Windows10Debloater: https://github.com/Sycnex/Windows10Debloater \n" +
                                   "SophiaScript: https://github.com/farag2/Windows-10-Sophia-Script";
        }
    }
}
