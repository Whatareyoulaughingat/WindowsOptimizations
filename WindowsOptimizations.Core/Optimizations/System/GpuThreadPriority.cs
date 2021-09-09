using Microsoft.Win32;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using WindowsOptimizations.Core.GlobalData;

namespace WindowsOptimizations.Core.Optimizations.System
{
    public class GpuThreadPriorityOptimizations
    {
        public Task IncreaseThreadPriority()
        {
            string query = "select * from Win32_VideoController";
            using ManagementObjectSearcher searcher = new(query);

            string gpuBrand = searcher
                .Get()
                .Cast<ManagementObject>()
                .Select(x => x["Name"] as string)
                .First();

            if (gpuBrand.Contains("Nvidia"))
            {
                Registry.SetValue(RegistryKeys.NvidiaParameters, "ThreadPriority", 0000001F);
            }
            else
            {
                Registry.SetValue(RegistryKeys.AmdParameters, "ThreadPriority", 0000001F);
            }

            return Task.CompletedTask;
        }
    }
}
