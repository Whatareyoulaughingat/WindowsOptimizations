using Microsoft.Win32;
using WindowsOptimizations.Core.GlobalData;

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
            Registry.SetValue(RegistryKeys.SystemProfileKey, "SystemResponsiveness", 1);
            return this;
        }

        /// <summary>
        /// Increase the priority a game takes in the system over other applications.
        /// </summary>
        /// <returns>[<see cref="SystemProfilePatch"/>] The same class for allowing method chaining.</returns>
        public SystemProfilePatch IncreaseGamePriority()
        {
            Registry.SetValue(RegistryKeys.GameTaskSystemProfileKey, "Priority", 6);
            return this;
        }

        /// <summary>
        /// Sets the scheducling category from normal to high. Will increase system responsiveness in general.
        /// </summary>
        /// <returns>[<see cref="SystemProfilePatch"/>] The same class for allowing method chaining.</returns>
        public SystemProfilePatch SetSchedulingCategoryToHigh()
        {
            Registry.SetValue(RegistryKeys.GameTaskSystemProfileKey, "Scheduling Category", "High");
            return this;
        }

        /// <summary>
        /// Sets the SFIO priority from normal to high.
        /// </summary>
        /// <returns>[<see cref="SystemProfilePatch"/>] The same class for allowing method chaining.</returns>
        public SystemProfilePatch SetSFIOPriorityToHigh()
        {
            Registry.SetValue(RegistryKeys.GameTaskSystemProfileKey, "SFIO Priority", "High");
            return this;
        }
    }
}