using Microsoft.Win32;
using System;
using System.Linq;
using System.Management;
using System.Windows;
using WindowsOptimizations.Core.Extensions;
using WindowsOptimizations.Core.GlobalData;

namespace WindowsOptimizations.Core.Patches
{
    /// <summary>
    /// Various kernel patches that reduce CPU processes thus reducing resource usage.
    /// </summary>
    public class CPUProcessPatch
    {
        /// <summary>
        /// Limits the splitting threshold of SvcHosts.
        /// </summary>
        /// <returns>[<see cref="CPUProcessPatch"/>] The same class for allowing method chaining.</returns>
        public CPUProcessPatch LimitSvcHostSplitting()
        {
            // Get total amount of RAM installed.
            string query = "SELECT Capacity FROM Win32_PhysicalMemory";
            ManagementObjectSearcher searcher = new(query);

            string totalRamAmount = StringExtensions.ToSize(searcher
                .Get()
                .Cast<ManagementObject>()
                .Sum(x => Convert.ToInt64(x.Properties["Capacity"].Value)), SizeUnits.GB);

            // Set the svc host splitting threshold accoring to the total amount of ram.
            switch (totalRamAmount)
            {
                case "4.00":
                    Registry.SetValue(RegistryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 4194304);
                    break;

                case "6.00":
                    Registry.SetValue(RegistryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 6291456);
                    break;

                case "8.00":
                    Registry.SetValue(RegistryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 8388608);
                    break;

                case "12.00":
                    Registry.SetValue(RegistryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 12582912);
                    break;

                case "16.00":
                    Registry.SetValue(RegistryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 16777216);
                    break;

                case "24.00":
                    Registry.SetValue(RegistryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 25165824);
                    break;

                case "32.00":
                    Registry.SetValue(RegistryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 33554432);
                    break;

                case "64.00":
                    Registry.SetValue(RegistryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 67108864);
                    break;

                default:
                    MessageBox.Show("Your total amount of RAM is either lower than 4GB or bigger than 64GB. This optimization cannot be applied because of that." + totalRamAmount, nameof(CPUProcessPatch), MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }

            return this;
        }
    }
}
