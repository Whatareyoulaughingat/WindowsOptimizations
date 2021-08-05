namespace WindowsOptimizations.Core.GlobalData
{
    /// <summary>
    /// A struct containg various checks for every patch that requires a system reboot.
    /// </summary>
    public static class PatchExecutionCheck
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not the user has disabled unnecessary windows services by using this application.
        /// </summary>
        public static bool HasDisabledUnnecessaryWindowsServices { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the user has reduced mouse input latency by using this application.
        /// </summary>
        public static bool HasReducedMouseInputLatency { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the user has optimized the system profile in this kernel by using this application.
        /// </summary>
        public static bool HasOptimizedSystemProfile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the user has debloated windows by using Windows10Debloater and Sophia Script through this application.
        /// </summary>
        public static bool HasDebloatedWindows { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the user has optimized the way windows manages network options by using this application.
        /// </summary>
        public static bool HasOptimizedNetworkOptions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the user has optimized the way CPU processes are handled and reduced by using this application.
        /// </summary>
        public static bool HasReducedCPUProcesses { get; set; }
    }
}