using System;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using WindowsOptimizations.Core.Extensions;
using WindowsOptimizations.Core.GlobalData;

namespace WindowsOptimizations.Core.Patches
{
    /// <summary>
    /// Various kernel patches that reduce CPU processes thus reducing resource usage.
    /// </summary>
    public static class CPUProcessPatch
    {
        /// <summary>
        /// Limits the splitting threshold of SvcHosts.
        /// </summary>
        /// <returns>[<see cref="Task"/>] An asynchronous operation.</returns>
        public static Task LimitSvcHostSplitting()
        {
            // Get total amount of RAM installed.
            string query = "SELECT Capacity FROM Win32_PhysicalMemory";
            using ManagementObjectSearcher searcher = new (query);

            string totalRamAmount = StringExtensions.ToSize(
                searcher
                .Get()
                .Cast<ManagementObject>()
                .Sum(x => Convert.ToInt64(x.Properties["Capacity"].Value)), SizeUnits.GB);

            // Set the Svc host splitting threshold accoring to the total amount of ram.
            RegistryKeys registryKeys = new();

            switch (totalRamAmount)
            {
                case "4.00":
                    Registry.SetValue(registryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 4194304);
                    break;

                case "6.00":
                    Registry.SetValue(registryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 6291456);
                    break;

                case "8.00":
                    Registry.SetValue(registryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 8388608);
                    break;

                case "12.00":
                    Registry.SetValue(registryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 12582912);
                    break;

                case "16.00":
                    Registry.SetValue(registryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 16777216);
                    break;

                case "24.00":
                    Registry.SetValue(registryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 25165824);
                    break;

                case "32.00":
                    Registry.SetValue(registryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 33554432);
                    break;

                case "64.00":
                    Registry.SetValue(registryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 67108864);
                    break;

                default:
                    MessageBox.Show("Your total amount of RAM is either lower than 4GB or bigger than 64GB. This optimization cannot be applied because of that." + totalRamAmount, nameof(CPUProcessPatch), MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
