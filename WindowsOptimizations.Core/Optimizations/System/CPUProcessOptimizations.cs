using System;
using System.Linq;
using System.Management;
using System.Windows;
using Microsoft.Win32;
using WindowsOptimizations.Core.Extensions;
using WindowsOptimizations.Core.GlobalData;

namespace WindowsOptimizations.Core.Optimizations.System
{
    /// <summary>
    /// Various registry changes that reduce CPU processes thus reducing resource usage.
    /// </summary>
    public class CpuProcessOptimizations
    {
        /// <summary>
        /// Limits the splitting threshold of SvcHosts.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool LimitSvcHostSplitting()
        {
            try
            {
                // Get the total amount of RAM installed.
                string query = "SELECT Capacity FROM Win32_PhysicalMemory";
                using ManagementObjectSearcher searcher = new(query);

                string totalRamAmount = searcher
                    .Get()
                    .Cast<ManagementObject>()
                    .Sum(x => Convert.ToInt64(x.Properties["Capacity"].Value))
                    .ToSize(SizeUnits.GB);

                // Set the Svc host splitting threshold accoring to the total amount of ram.
                switch (totalRamAmount)
                {
                    case "3.7":
                    case "3.8":
                    case "3.9":
                    case "4.00":
                        Registry.SetValue(RegistryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 4194304);
                        return true;

                    case "5.7":
                    case "5.8":
                    case "5.9":
                    case "6.00":
                        Registry.SetValue(RegistryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 6291456);
                        return true;

                    case "7.7":
                    case "7.8":
                    case "7.9":
                    case "8.00":
                        Registry.SetValue(RegistryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 8388608);
                        return true;

                    case "11.7":
                    case "11.8":
                    case "11.9":
                    case "12.00":
                        Registry.SetValue(RegistryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 12582912);
                        return true;

                    case "16.7":
                    case "16.8":
                    case "16.9":
                    case "16.00":
                        Registry.SetValue(RegistryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 16777216);
                        return true;

                    case "23.7":
                    case "23.8":
                    case "23.9":
                    case "24.00":
                        Registry.SetValue(RegistryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 25165824);
                        return true;

                    case "31.7":
                    case "31.8":
                    case "31.9":
                    case "32.00":
                        Registry.SetValue(RegistryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 33554432);
                        return true;

                    case "63.7":
                    case "63.8":
                    case "63.9":
                    case "64.00":
                        Registry.SetValue(RegistryKeys.CurrentControlKey, "SvcHostSplitThresholdInKB", 67108864);
                        return true;

                    default:
                        MessageBox.Show("Your total amount of RAM is either lower than 4GB or bigger than 64GB. This optimization cannot be applied.", nameof(CpuProcessOptimizations), MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                }
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }
    }
}
