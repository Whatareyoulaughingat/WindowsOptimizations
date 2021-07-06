using System.Threading.Tasks;
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
        /// <returns>[<see cref="Task"/>] An asynchronous operation.</returns>
        public static Task IncreaseSystemResponsiveness()
        {
            Registry.SetValue(RegistryKeys.SystemProfileKey, "SystemResponsiveness", 1);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Increase the priority a game takes in the system over other applications.
        /// </summary>
        /// <returns>[<see cref="SystemProfilePatch"/>] An asynchronous operation.</returns>
        public static Task IncreaseGamePriority()
        {
            Registry.SetValue(RegistryKeys.GameTaskSystemProfileKey, "Priority", 6);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Sets the scheducling category from normal to high. Will increase system responsiveness in general.
        /// </summary>
        /// <returns>[<see cref="Task"/>] An asynchronous operation.</returns>
        public static Task SetSchedulingCategoryToHigh()
        {
            Registry.SetValue(RegistryKeys.GameTaskSystemProfileKey, "Scheduling Category", "High");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Sets the SFIO priority from normal to high.
        /// </summary>
        /// <returns>[<see cref="Task"/>] An asynchronous operation.</returns>
        public static Task SetSFIOPriorityToHigh()
        {
            Registry.SetValue(RegistryKeys.GameTaskSystemProfileKey, "SFIO Priority", "High");
            return Task.CompletedTask;
        }
    }
}