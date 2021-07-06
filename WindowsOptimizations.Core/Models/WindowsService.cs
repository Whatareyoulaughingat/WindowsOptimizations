using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace WindowsOptimizations.Core.Models
{
    /// <summary>
    /// A class model containg variables that store the state of Windows services loaded from the configuration file.
    /// </summary>
    public class WindowsService : ReactiveObject
    {
        /// <summary>
        /// Gets or sets the name of the service.
        /// </summary>
        [Reactive]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the specific service is selected.
        /// </summary>
        [Reactive]
        public bool IsSelected { get; set; }
    }
}