using WindowsOptimizations.Core.Native;

namespace WindowsOptimizations.Core.Patches
{
    /// <summary>
    /// Gets information about the system timer with the ability to change it to the maximum or minimum possible values.
    /// </summary>
    public class TimerResolutionPatch
    {
        /// <summary>
        /// Contains a value about the minimum possible system timer resolution
        /// </summary>
        public int MinimumResolution;

        /// <summary>
        /// Contains a value about the maximum possible system timer resolution.
        /// </summary>
        public int MaximumResolution;

        /// <summary>
        /// Contains a value about the current system timer resolution.
        /// </summary>
        public int CurrentResolution;

        /// <summary>
        /// Sets the system's timer to the lowest value possible (0.5ms).
        /// </summary>
        /// <returns>[<see cref="TimerResolutionPatch"/>] The same class for allowing method chaining.</returns>
        public TimerResolutionPatch SetMaximumTimerResolutionValue()
        {
            NativeMethods.NtSetTimerResolution(MaximumResolution, true, ref CurrentResolution);
            return this;
        }

        /// <summary>
        /// Gets various information about the system's timer.
        /// </summary>
        /// <returns>[<see cref="TimerResolutionPatch"/>] The same class for allowing method chaining.</returns>
        public TimerResolutionPatch GetTimerResolutionInfo()
        {
            NativeMethods.NtQueryTimerResolution(out int maximumResolution, out int minimumResolution, out int currentResolution);

            MaximumResolution = maximumResolution;
            MinimumResolution = minimumResolution;
            CurrentResolution = currentResolution;

            return this;
        }
    }
}