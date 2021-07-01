using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace WindowsOptimizations.Core.Models
{
    /// <summary>
    /// A class model containg variables that store the state of Windows services loaded from the configuration file.
    /// </summary>
    public class WindowsService : ReactiveObject
    {
        [Reactive]
        public string Name { get; set; }

        [Reactive]
        public bool IsSelected { get; set; }
    }
}