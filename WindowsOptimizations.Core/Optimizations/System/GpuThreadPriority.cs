using Microsoft.Win32;
using System;
using System.Linq;
using System.Management;
using System.Windows;
using WindowsOptimizations.Core.GlobalData;

namespace WindowsOptimizations.Core.Optimizations.System
{
    public class GpuThreadPriorityOptimizations
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool IncreaseThreadPriority()
        {
            try
            {
                string query = "select * from Win32_VideoController";
                using ManagementObjectSearcher searcher = new(query);

                string gpuBrand = searcher
                    .Get()
                    .Cast<ManagementObject>()
                    .Select(x => x["Name"] as string)
                    .First();

                if (gpuBrand == "Nvidia" || gpuBrand == "NVIDIA" || gpuBrand == "nvidia")
                {
                    Registry.SetValue(RegistryKeys.NvidiaParameters, "ThreadPriority", 0000001F);
                    return true;
                }
                else if (gpuBrand == "Amd" || gpuBrand == "AMD" || gpuBrand == "amd")
                {
                    Registry.SetValue(RegistryKeys.AmdParameters, "ThreadPriority", 0000001F);
                    return true;
                }
                else
                {
                    MessageBox.Show("Your GPU is most likely an Intel one. This optimization cannot be applied.", nameof(GpuThreadPriorityOptimizations), MessageBoxButton.OK, MessageBoxImage.Error);
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
