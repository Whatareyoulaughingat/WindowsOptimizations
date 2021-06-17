using Microsoft.Win32;

namespace WindowsOptimizations.Core.Patches
{
    /// <summary>
    /// Various kernel patches for optimizing the system profile.
    /// </summary>
    public class SystemProfilePatch
    {
        /// <summary>
        /// Increases overall system responsiveness.
        /// </summary>
        /// <returns>[<see cref="SystemProfilePatch"/>] The same class for allowing method chaining.</returns>
        public SystemProfilePatch IncreaseSystemResponsiveness()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile", "SystemResponsiveness", 1);
            return this;
        }

        /// <summary>
        /// Increase the priority a game takes in the system over other applications.
        /// </summary>
        /// <returns>[<see cref="SystemProfilePatch"/>] The same class for allowing method chaining.</returns>
        public SystemProfilePatch IncreaseGamePriority()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", "Priority", 6);
            return this;
        }

        /// <summary>
        /// Sets the scheducling category from normal to high. Will increase system responsiveness in general.
        /// </summary>
        /// <returns>[<see cref="SystemProfilePatch"/>] The same class for allowing method chaining.</returns>
        public SystemProfilePatch SetSchedulingCategoryToHigh()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", "Scheduling Category", "High");
            return this;
        }

        /// <summary>
        /// Sets the SFIO priority from normal to high.
        /// </summary>
        /// <returns>[<see cref="SystemProfilePatch"/>] The same class for allowing method chaining.</returns>
        public SystemProfilePatch SetSFIOPriorityToHigh()
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Multimedia\SystemProfile\Tasks\Games", "SFIO Priority", "High");
            return this;
        }
    }
}