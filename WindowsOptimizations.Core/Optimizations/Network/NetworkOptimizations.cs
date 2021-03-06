using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using WindowsOptimizations.Core.GlobalData;

namespace WindowsOptimizations.Core.Optimizations.Network
{
    /// <summary>
    /// Various registry and powershell changes that may improve or provide a more stable internet speed.
    /// </summary>
    public class NetworkOptimizations
    {
        /// <summary>
        /// Limits throughput, especially in high-speed, high-latency environments, such as most internet connections.
        /// <para>This method sets the auto-tuning level to 'normal'.</para>
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool ChangeWindowAutoTuningLevel()
        {
            try
            {
                using Process powershell = new();
                powershell.StartInfo.FileName = "powershell.exe";
                powershell.StartInfo.CreateNoWindow = true;
                powershell.StartInfo.Arguments = "Set-NetTCPSetting -SettingName InternetCustom -AutoTuningLevelLocal Normal";
                powershell.Start();
                powershell.WaitForExit();

                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// Restricts the auto-tuning level.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool DisableWindowsScalingHeuristics()
        {
            try
            {
                using Process powershell = new();
                powershell.StartInfo.FileName = "powershell.exe";
                powershell.StartInfo.CreateNoWindow = true;
                powershell.StartInfo.Arguments = "Set-NetTCPSetting -SettingName InternetCustom -ScalingHeuristics Disabled";
                powershell.Start();
                powershell.WaitForExit();

                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// Changes the TCP congestion provider to CTCP, increasing network speed especially on higher speed network connections. This also may decrease latency (ping).
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool ChangeCongestionProvider()
        {
            try
            {
                Registry.SetValue(RegistryKeys.TCPCongestionProviderKey, "00000000", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x05, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xff, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 });
                Registry.SetValue(RegistryKeys.TCPCongestionProviderKey, "04000000", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x05, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xff, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 });

                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// The receive-side scaling setting enables parallelized processing of received packets on multiple processors, while avoiding packet reordering.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool EnableRSS()
        {
            try
            {
                using Process powershell = new();
                powershell.StartInfo.FileName = "powershell.exe";
                powershell.StartInfo.CreateNoWindow = true;
                powershell.StartInfo.Arguments = "Enable-NetAdapterRss -Name *";
                powershell.Start();
                powershell.WaitForExit();

                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// Allows the NIC to coalesce multiple TCP/IP packets that arrive within a single interrupt into a single larger packet (up to 64KB).
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool EnableRSC()
        {
            try
            {
                using Process powershell = new();
                powershell.StartInfo.FileName = "powershell.exe";
                powershell.StartInfo.CreateNoWindow = true;
                powershell.StartInfo.Arguments = "Enable-NetAdapterRsc -Name *";
                powershell.Start();
                powershell.WaitForExit();

                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// Limits the time and number of hops/routers a packet will travel before being discarded. A number that's too small risks packets being discarded before reaching their destination.
        /// A number that's too large (over 128) will cause delay in when lost IP packets are discarded.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool SetDefaultTTL()
        {
            try
            {
                Registry.SetValue(RegistryKeys.TcpipParametersKey, "DefaultTTL", 64);
                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// A mechanism that provides routers with an alternate method of communicating network congestion. It is aimed to decrease retransmissions.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool DisableECN()
        {
            try
            {
                using Process powershell = new();
                powershell.StartInfo.FileName = "powershell.exe";
                powershell.StartInfo.CreateNoWindow = true;
                powershell.StartInfo.Arguments = "Set-NetTCPSetting -SettingName InternetCustom -EcnCapability Disabled";
                powershell.Start();
                powershell.WaitForExit();

                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// Disables checksum offloading.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool DisableChecksumOffloading()
        {
            try
            {
                using Process powershell = new();
                powershell.StartInfo.FileName = "powershell.exe";
                powershell.StartInfo.CreateNoWindow = true;
                powershell.StartInfo.Arguments = "Disable-NetAdapterChecksumOffload -Name *";
                powershell.Start();
                powershell.WaitForExit();

                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// Enables Windows to offload all TCP processing for a connection to a network adapter (with proper driver support).
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool DisableTCPChimneyOffload()
        {
            try
            {
                using Process powershell = new();
                powershell.StartInfo.FileName = "powershell.exe";
                powershell.StartInfo.CreateNoWindow = true;
                powershell.StartInfo.Arguments = "Set-NetOffloadGlobalSetting -Chimney Disabled";
                powershell.Start();
                powershell.WaitForExit();

                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// The network adapter hardware is used to complete data segmentation, theoretically faster than operating system software.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool DisableLargeSendOffload()
        {
            try
            {
                using Process powershell = new();
                powershell.StartInfo.FileName = "powershell.exe";
                powershell.StartInfo.CreateNoWindow = true;
                powershell.StartInfo.Arguments = "Disable-NetAdapterLso -Name *";
                powershell.Start();
                powershell.WaitForExit();

                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// "Timestamps" (TSOpt) is a less commonly used 1323 option that is intended to increase transmission reliability by retransmitting segments that are not acknowledged within some
        /// retransmission timeout (RTO) interval.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool DisableTCP1323Timestamps()
        {
            try
            {
                using Process powershell = new();
                powershell.StartInfo.FileName = "powershell.exe";
                powershell.StartInfo.CreateNoWindow = true;
                powershell.StartInfo.Arguments = "Set-NetTCPSetting -SettingName InternetCustom -Timestamps Disabled";
                powershell.Start();
                powershell.WaitForExit();

                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// This is intended to increase the priority of DNS/hostname resolution, by increasing the priority of four related processes from their defaults.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool IncreaseHostResolutionPriority()
        {
            try
            {
                Registry.SetValue(RegistryKeys.ServiceProviderKey, "LocalPriority", 4);
                Registry.SetValue(RegistryKeys.ServiceProviderKey, "HostPriority", 5);
                Registry.SetValue(RegistryKeys.ServiceProviderKey, "DnsPriority", 6);
                Registry.SetValue(RegistryKeys.ServiceProviderKey, "NetbtPriority", 7);

                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// Sets the number of times to attempt to reestablish a connection with SYN packets.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool DecreaseMaxSYNRetransmissions()
        {
            try
            {
                using Process powershell = new();
                powershell.StartInfo.FileName = "powershell.exe";
                powershell.StartInfo.CreateNoWindow = true;
                powershell.StartInfo.Arguments = "Set-NetTCPSetting -SettingName InternetCustom -MaxSynRetransmissions 2";
                powershell.Start();
                powershell.WaitForExit();

                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// Helps slow clients/connections as it makes TCP/IP less aggressive in retransmitting packets when enabled.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool DisableNonStackRttResiliency()
        {
            try
            {
                using Process powershell = new();
                powershell.StartInfo.FileName = "powershell.exe";
                powershell.StartInfo.CreateNoWindow = true;
                powershell.StartInfo.Arguments = "Set-NetTCPSetting -SettingName InternetCustom -NonSackRttResiliency disabled";
                powershell.Start();
                powershell.WaitForExit();

                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// A network throttling mechanism to restrict the processing of non-multimedia network traffic to 10 packets per millisecond.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool DisableNetworkThrottlingIndex()
        {
            try
            {
                Registry.SetValue(RegistryKeys.SystemProfileKey, "NetworkThrottlingIndex", 0xffffffff);
                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// Nagle's algorithm is designed to allow several small packets to be combined together into a single, larger packet for more efficient transmissions.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool DisableNaglesAlgorithm()
        {
            try
            {
                List<NetworkInterface> networkInterfaces = new();

                // Get all network devices and add them to the list if they're operational.
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        networkInterfaces.Add(nic);
                    }
                }

                NetworkInterface result = null;

                // Enumerate through each network device that we got earlier and get its IPv4 properties. After that, assign some registry keys to that particular network device using its own device ID.
                foreach (NetworkInterface nic in networkInterfaces)
                {
                    if (result == null)
                    {
                        result = nic;
                    }
                    else
                    {
                        if (nic.GetIPProperties().GetIPv4Properties() != null)
                        {
                            if (nic.GetIPProperties().GetIPv4Properties().Index < result.GetIPProperties().GetIPv4Properties().Index)
                            {
                                result = nic;

                                Registry.SetValue($"{RegistryKeys.TcpipInterfacesKey}\\{result.Id}", "TcpAckFrequency", 1);
                                Registry.SetValue($"{RegistryKeys.TcpipInterfacesKey}\\{result.Id}", "TcpNoDelay", 1);
                                Registry.SetValue($"{RegistryKeys.TcpipInterfacesKey}\\{result.Id}", "TcpDelAckTicks", 0);
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// Optimizes the machine as a file server so it would allocate resources accordingly.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool ChangeNetworkMemoryAllocations()
        {
            try
            {
                Registry.SetValue(RegistryKeys.MemoryManagementKey, "LargeSystemCache", 0);
                Registry.SetValue(RegistryKeys.MemoryManagementKey, "Size", 1);

                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// The Windows defaults are usually sufficient under normal network load. However, under heavy network load it may be necessary to adjust these two registry settings to increase port availability
        /// and decrease the time to wait before reclaiming unused ports.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool ConfigureDynamicPortAllocation()
        {
            try
            {
                Registry.SetValue(RegistryKeys.TcpipParametersKey, "MaxUserPort", 65534);
                Registry.SetValue(RegistryKeys.TcpipParametersKey, "TcpTimedWaitDelay", 30);

                return true;
            }
            catch (Exception ax)
            {
                MessageBox.Show($"An exception has occured! Error message: {ax.Message}");
                return false;
            }
        }

        /// <summary>
        /// Prevents QoS applications from getting priority to 20% of available bandwidth.
        /// </summary>
        /// <returns>[<see cref="bool"/>] A completion result.</returns>
        public bool DisableReservableBandwidthLimit()
        {
            try
            {
                Registry.SetValue(RegistryKeys.PschedKey, "NonBestEffortLimit", 0);
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
