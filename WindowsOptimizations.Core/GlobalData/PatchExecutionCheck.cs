using System;

namespace WindowsOptimizations.Core.GlobalData
{
    /// <summary>
    /// A struct containg various checks for every patch that requires a system reboot.
    /// </summary>
    public struct PatchExecutionCheck : IEquatable<PatchExecutionCheck>
    {
        internal PatchExecutionCheck(bool hasDisabledUnnecessaryWindowsServices, bool hasReducedMouseInputLatency, bool hasOptimizedSystemProfile, bool hasDebloatedWindows, bool hasOptimizedNetworkOptions, bool hasReducedCPUProcesses)
        {
            HasDisabledUnnecessaryWindowsServices = hasDisabledUnnecessaryWindowsServices;
            HasReducedMouseInputLatency = hasReducedMouseInputLatency;
            HasOptimizedSystemProfile = hasOptimizedSystemProfile;
            HasDebloatedWindows = hasDebloatedWindows;
            HasOptimizedNetworkOptions = hasOptimizedNetworkOptions;
            HasReducedCPUProcesses = hasReducedCPUProcesses;
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the user has disabled unnecessary windows services by using this application.
        /// </summary>
        public bool HasDisabledUnnecessaryWindowsServices { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the user has reduced mouse input latency by using this application.
        /// </summary>
        public bool HasReducedMouseInputLatency { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the user has optimized the system profile in this kernel by using this application.
        /// </summary>
        public bool HasOptimizedSystemProfile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the user has debloated windows by using Windows10Debloater and Sophia Script through this application.
        /// </summary>
        public bool HasDebloatedWindows { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the user has optimized the way windows manages network options by using this application.
        /// </summary>
        public bool HasOptimizedNetworkOptions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the user has optimized the way CPU processes are handled and reduced by using this application.
        /// </summary>
        public bool HasReducedCPUProcesses { get; set; }

        public bool Equals(PatchExecutionCheck other)
            => (HasDisabledUnnecessaryWindowsServices,
            HasReducedMouseInputLatency,
            HasOptimizedSystemProfile,
            HasDebloatedWindows,
            HasOptimizedNetworkOptions,
            HasReducedCPUProcesses) == (other.HasDisabledUnnecessaryWindowsServices, other.HasReducedMouseInputLatency, other.HasOptimizedSystemProfile, other.HasDebloatedWindows, other.HasOptimizedNetworkOptions, other.HasReducedCPUProcesses);

        public override bool Equals(object obj)
            => (obj is PatchExecutionCheck patchExecutionCheck) && Equals(patchExecutionCheck);

        public override int GetHashCode()
            => (HasDisabledUnnecessaryWindowsServices,
            HasReducedMouseInputLatency,
            HasOptimizedSystemProfile,
            HasDebloatedWindows,
            HasOptimizedNetworkOptions,
            HasReducedCPUProcesses)
            .GetHashCode();

        public static bool operator ==(PatchExecutionCheck left, PatchExecutionCheck right)
            => Equals(left, right);

        public static bool operator !=(PatchExecutionCheck left, PatchExecutionCheck right)
            => !Equals(left, right);
    }
}