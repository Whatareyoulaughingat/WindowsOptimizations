using System;
using System.IO;

namespace WindowsOptimizations.Core.GlobalData
{
    /// <summary>
    /// A static class containing directory and file paths to be used for this application.
    /// </summary>
    public static class Paths
    {
        /// <summary>
        /// Gets the base directory path of this application.
        /// </summary>
        public static string Base => Path.GetFullPath("WindowsOptimizations", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

        /// <summary>
        /// Gets a file used for configuring which unnecessary windows service is going to be disabled.
        /// </summary>
        public static string DefaultUnnecessaryWindowsServicesJsonFile => Path.GetFullPath("DefaultUnnecessaryWindowsServices.json", Base);
    }
}