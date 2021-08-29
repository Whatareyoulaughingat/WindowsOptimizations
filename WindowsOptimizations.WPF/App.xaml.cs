using System.IO;
using System.Reflection;
using System.Windows;
using ReactiveUI;
using Splat;
using WindowsOptimizations.Core.GlobalData;

namespace WindowsOptimizations.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Directory.CreateDirectory(Paths.Base);

            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
            base.OnStartup(e);
        }
    }
}
