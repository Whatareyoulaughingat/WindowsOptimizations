using System;
using System.Windows;
using Microsoft.Win32;
using WindowsOptimizations.Core.GlobalData;

namespace WindowsOptimizations.Core.Optimizations.System
{
    /// <summary>
    /// Various registry changes for optimizing the system profile.
    /// </summary>
    public class SystemProfileOptimizations
    {
        /// <summary>
        /// Increases overall system responsiveness.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool IncreaseSystemResponsiveness()
        {
            try
            {
                Registry.SetValue(RegistryKeys.SystemProfileKey, "SystemResponsiveness", 1);
                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// Increase the priority a game takes in the system over other applications.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool IncreaseGamePriority()
        {
            try
            {
                Registry.SetValue(RegistryKeys.GameTaskSystemProfileKey, "Priority", 6);
                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// Sets the scheducling category from normal to high. Will increase system responsiveness in general.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool SetSchedulingCategoryToHigh()
        {
            try
            {
                Registry.SetValue(RegistryKeys.GameTaskSystemProfileKey, "Scheduling Category", "High");
                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// Sets the SFIO priority from normal to high.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool SetSFIOPriorityToHigh()
        {
            try
            {
                Registry.SetValue(RegistryKeys.GameTaskSystemProfileKey, "SFIO Priority", "High");
                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }
    }
}