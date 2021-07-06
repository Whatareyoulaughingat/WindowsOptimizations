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
        /// <param name="desiredResolution">The desired resolution value to be set. Usually 0.5ms or 1ms.</param>
        /// <param name="setResolution">A value whether or not to set the resolution.</param>
        /// <param name="currentResolution">The current resolution value after the desired resolution has been set.</param>
        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern void NtSetTimerResolution(int desiredResolution, bool setResolution, ref int currentResolution);

        /// <summary>
        /// Returns the resolution of the system timer in the context of the calling process.
        /// </summary>
        /// <param name="maximumResolution">The maximum resolution the system timer supports.</param>
        /// <param name="minimumResolution">The minimum resolution the system timer supports.</param>
        /// <param name="currentResolution">The current resolution of the system timer.</param>
        /// <returns>[<see cref="int"/>] An HRESULT value.</returns>
        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern int NtQueryTimerResolution(out int maximumResolution, out int minimumResolution, out int currentResolution);
    }
}
