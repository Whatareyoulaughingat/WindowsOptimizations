using System;

namespace WindowsOptimizations.Core.GlobalData
{
    /// <summary>
    /// A struct containing directory and file paths to be used for this application.
    /// </summary>
    public static class Paths
    {
        /// <summary>
        /// Gets the base directory path of this application.
        /// </summary>
        public static string Base => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WindowsOptimizations";

        /// <summary>
        /// Gets a file used for configuring which unnecessary windows service is going to be disabled.
        /// </summary>
        public static string UnnecessaryWindowsServicesJsonFile => Base + "\\UnnecessaryWindowsServices.json";
    }
}