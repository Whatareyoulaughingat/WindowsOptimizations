using System.Threading.Tasks;
using WindowsOptimizations.Core.Native;

namespace WindowsOptimizations.Core.Patches
{
    /// <summary>
    /// Gets information about the system timer with the ability to change it to the maximum or minimum possible values.
    /// </summary>
    public class TimerResolutionPatch
    {
        /// <summary>
        /// The minimum possible system timer resolution.
        /// </summary>
        public static int MinimumResolution;

        /// <summary>
        /// The maximum possible system timer resolution.
        /// </summary>
        public static int MaximumResolution;

        /// <summary>
        /// The current system timer resolution.
        /// </summary>
        public static int CurrentResolution;

        /// <summary>
        /// Sets the system's timer to the lowest value possible (0.5ms).
        /// </summary>
        /// <returns>[<see cref="TimerResolutionPatch"/>] An asynchronous operation.</returns>
        public static Task SetMaximumTimerResolutionValue()
        {
            NativeMethods.NtSetTimerResolution(MaximumResolution, true, ref CurrentResolution);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets various information about the system's timer.
        /// </summary>
        /// <returns>[<see cref="Task"/>] An asynchronous operation.</returns>
        public static Task GetTimerResolutionInfo()
        {
            _ = NativeMethods.NtQueryTimerResolution(out int maximumResolution, out int minimumResolution, out int currentResolution);

            MaximumResolution = maximumResolution;
            MinimumResolution = minimumResolution;
            CurrentResolution = currentResolution;

            return Task.CompletedTask;
        }
    }
}