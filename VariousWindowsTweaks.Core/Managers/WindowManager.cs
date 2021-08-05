using System.Windows;

namespace WindowsOptimizations.Core.Managers
{
    /// <summary>
    /// Manages the way views are handled.
    /// </summary>
    public static class WindowManager
    {
        /// <summary>
        /// Opens a new window with the specified view and viewmodel as data context while waiting for it to exit.
        /// </summary>
        /// <typeparam name="TView">The specified view.</typeparam>
        /// <typeparam name="TViewModel">The specified viewmodel.</typeparam>
        public static void ShowBlockingView<TView, TViewModel>()
            where TView : Window, new()
            where TViewModel : class, new()
        {
            Window view = new TView
            {
                DataContext = new TViewModel(),
            };

            view.ShowDialog();
        }
    }
}
