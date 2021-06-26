using ReactiveUI;

namespace WindowsOptimizations.Core.Models
{
    /// <summary>
    /// A class model containg variables that store the state of Windows services loaded from the configuration file.
    /// </summary>
    public class WindowsService : ReactiveObject
    {
        private string name;
        public string Name
        {
            get { return name; } 
            set { this.RaiseAndSetIfChanged(ref name, value); }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { this.RaiseAndSetIfChanged(ref isSelected, value); }
        }
    }
}