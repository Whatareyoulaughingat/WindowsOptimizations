using System.Runtime.InteropServices;

namespace WindowsOptimizations.Core.Native
{
    /// <summary>
    /// Includes methods for accessing the native Win32 API through P/Invoke.
    /// </summary>
    public class NativeMethods
    {
        /// <summary>
        /// Sets the resolution of the system timer in the calling process context. The resolution value is in 100ns units, so a value of 10000 is one millisecond.
        /// </summary>
        /// <param name="DesiredResolution">The desired resolution value to be set. Usually 0.5ms or 1ms.</param>
        /// <param name="SetResolution">A value whether or not to set the resolution.</param>
        /// <param name="CurrentResolution">The current resolution value after the desired resolution has been set.</param>
        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern void NtSetTimerResolution(int DesiredResolution, bool SetResolution, ref int CurrentResolution);

        /// <summary>
        /// Returns the resolution of the system timer in the context of the calling process.
        /// </summary>
        /// <param name="MinimumResolution">The minimum resolution the system timer supports.</param>
        /// <param name="MaximumResolution">The maximum resolution the system timer supports.</param>
        /// <param name="CurrentResolution">The current resolution of the system timer.</param>
        /// <returns></returns>
        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern int NtQueryTimerResolution(out int MaximumResolution, out int MinimumResolution, out int CurrentResolution);
    }
}
