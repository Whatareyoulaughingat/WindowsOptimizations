namespace WindowsOptimizations.Core.GlobalData
{
    /// <summary>
    /// A struct containg various checks for every patch that requires a system reboot.
    /// </summary>
    public struct PatchExecutionCheck
    {
        /// <summary>
        /// Contains a value wether the user has disabled unnecessary windows services by using this application.
        /// </summary>
        public static bool HasDisabledUnnecessaryWindowsServices { get; set; }

        /// <summary>
        /// Contains a value wether the user has reduced mouse input latency by using this application.
        /// </summary>
        public static bool HasReducedMouseInputLatency { get; set; }

        /// <summary>
        /// Contains a value wether the user has optimized the system profile in this kernel by using this application.
        /// </summary>
        public static bool HasOptimizedSystemProfile { get; set; }

        /// <summary>
        /// Contains a value wether the user has debloated windows by using Windows10Debloater and Sophia Script through this application.
        /// </summary>
        public static bool HasDebloatedWindows { get; set; }

        /// <summary>
        /// Contains a value wether or the user has optimized the way windows manages TCP/UDP.
        /// </summary>
        public static bool HasOptimizedNetworkOptions { get; set; }
    }
}