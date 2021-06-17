﻿using Microsoft.Win32;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace WindowsOptimizations.Core.Patches
{
    public class NetworkPatch
    {
        /// <summary>
        /// Limit throughput, especially in high-speed, high-latency environments, such as most internet connections.
        /// <para>This method sets the auto-tuning level to 'normal'.</para>
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch ChangeWindowAutoTuningLevel()
        {
            using Process powershell = new Process();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.CreateNoWindow = true;
            powershell.StartInfo.Arguments = "Set-NetTCPSetting -SettingName InternetCustom -AutoTuningLevelLocal Normal";
            powershell.Start();

            return this;
        }

        /// <summary>
        /// Restricts the auto-tuning level.
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch DisableWindowsScalingHeuristics()
        {
            using Process powershell = new Process();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.CreateNoWindow = true;
            powershell.StartInfo.Arguments = "Set-NetTCPSetting -SettingName InternetCustom -ScalingHeuristics Disabled";
            powershell.Start();

            return this;
        }

        /// <summary>
        /// Changes the TCP congestion provider to CTCP, increasing network speed especially on higher speed network connections. This also may decrease latency (ping).
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch ChangeCongestionProvider()
        {
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Nsi\\{eb004a03-9b1a-11d4-9123-0050047759bc}\\26", "00000000", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x05, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xff, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 });
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Nsi\\{eb004a03-9b1a-11d4-9123-0050047759bc}\\26", "04000000", new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x05, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0xff, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 });

            return this;
        }

        /// <summary>
        /// The receive-side scaling setting enables parallelized processing of received packets on multiple processors, while avoiding packet reordering.
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch EnableRSS()
        {
            using Process powershell = new Process();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.CreateNoWindow = true;
            powershell.StartInfo.Arguments = "Enable-NetAdapterRss -Name *";
            powershell.Start();

            return this;
        }

        /// <summary>
        /// Allows the NIC to coalesce multiple TCP/IP packets that arrive within a single interrupt into a single larger packet (up to 64KB).
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch EnableRSC()
        {
            using Process powershell = new Process();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.CreateNoWindow = true;
            powershell.StartInfo.Arguments = "Enable-NetAdapterRsc -Name *";
            powershell.Start();

            return this;
        }

        /// <summary>
        /// Limits the time and number of hops/routers a packet will travel before being discarded. A number that's too small risks packets being discarded before reaching their destination.
        /// A number that's too large (over 128) will cause delay in when lost IP packets are discarded.
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch SetDefaultTTL()
        {
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters", "DefaultTTL", 64);
            return this;
        }

        /// <summary>
        /// A mechanism that provides routers with an alternate method of communicating network congestion. It is aimed to decrease retransmissions.
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch DisableECN()
        {
            using Process powershell = new Process();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.CreateNoWindow = true;
            powershell.StartInfo.Arguments = "Set-NetTCPSetting -SettingName InternetCustom -EcnCapability Disabled";
            powershell.Start();

            return this;
        }

        /// <summary>
        /// Disables checksum offloading.
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch DisableChecksumOffloading()
        {
            using Process powershell = new Process();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.CreateNoWindow = true;
            powershell.StartInfo.Arguments = "Disable-NetAdapterChecksumOffload -Name *";
            powershell.Start();

            return this;
        }

        /// <summary>
        /// Enables Windows to offload all TCP processing for a connection to a network adapter (with proper driver support).
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch DisableTCPChimneyOffload()
        {
            using Process powershell = new Process();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.CreateNoWindow = true;
            powershell.StartInfo.Arguments = "Set-NetOffloadGlobalSetting -Chimney Disabled";
            powershell.Start();

            return this;
        }

        /// <summary>
        /// The network adapter hardware is used to complete data segmentation, theoretically faster than operating system software.
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch DisableLargeSendOffload()
        {
            using Process powershell = new Process();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.CreateNoWindow = true;
            powershell.StartInfo.Arguments = "Disable-NetAdapterLso -Name *";
            powershell.Start();

            return this;
        }

        /// <summary>
        /// "Timestamps" (TSOpt) is a less commonly used 1323 option that is intended to increase transmission reliability by retransmitting segments that are not acknowledged within some
        /// retransmission timeout (RTO) interval.
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch DisableTCP1323Timestamps()
        {
            using Process powershell = new Process();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.CreateNoWindow = true;
            powershell.StartInfo.Arguments = "Set-NetTCPSetting -SettingName InternetCustom -Timestamps Disabled";
            powershell.Start();

            return this;
        }

        /// <summary>
        /// This is intended to increase the priority of DNS/hostname resolution, by increasing the priority of four related processes from their defaults. 
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch IncreaseHostResolutionPriority()
        {
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\ServiceProvider", "LocalPriority", 4);
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\ServiceProvider", "HostPriority", 5);
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\ServiceProvider", "DnsPriority", 6);
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\ServiceProvider", "NetbtPriority", 7);

            return this;
        }

        /// <summary>
        /// Sets the number of times to attempt to reestablish a connection with SYN packets.
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch DecreaseMaxSYNRetransmissions()
        {
            using Process powershell = new Process();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.CreateNoWindow = true;
            powershell.StartInfo.Arguments = "Set-NetTCPSetting -SettingName InternetCustom -MaxSynRetransmissions 2";
            powershell.Start();

            return this;
        }

        /// <summary>
        /// Helps slow clients/connections as it makes TCP/IP less aggressive in retransmitting packets when enabled.
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch DisableNonStackRttResiliency()
        {
            using Process powershell = new Process();
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.CreateNoWindow = true;
            powershell.StartInfo.Arguments = "Set-NetTCPSetting -SettingName InternetCustom -NonSackRttResiliency disabled";
            powershell.Start();

            return this;
        }

        /// <summary>
        /// A network throttling mechanism to restrict the processing of non-multimedia network traffic to 10 packets per millisecond.
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch DisableNetworkThrottlingIndex()
        {
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile", "NetworkThrottlingIndex", 0xffffffff);
            return this;
        }

        /// <summary>
        /// Nagle's algorithm is designed to allow several small packets to be combined together into a single, larger packet for more efficient transmissions.
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch DisableNaglesAlgorithm()
        {
            List<NetworkInterface> networkInterfaces = new List<NetworkInterface>();

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

                            Registry.SetValue($"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces\\{result.Id}", "TcpAckFrequency", 1);
                            Registry.SetValue($"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces\\{result.Id}", "TcpNoDelay", 1);
                            Registry.SetValue($"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces\\{result.Id}", "TcpDelAckTicks", 0);
                        }
                    }
                }
            }

            return this;
        }

        /// <summary>
        /// Optimizes the machine as a file server so it would allocate resources accordingly.
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch ChangeNetworkMemoryAllocations()
        {
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management", "LargeSystemCache", 0);
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management", "Size", 1);

            return this;
        }

        /// <summary>
        /// The Windows defaults are usually sufficient under normal network load. However, under heavy network load it may be necessary to adjust these two registry settings to increase port availability
        /// and decrease the time to wait before reclaiming unused ports.
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch ConfigureDynamicPortAllocation()
        {
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters", "MaxUserPort", 65534);
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters", "TcpTimedWaitDelay", 30);

            return this;
        }

        /// <summary>
        /// Prevents QoS applications from getting priority to 20% of available bandwidth
        /// </summary>
        /// <returns>[<see cref="NetworkPatch"/>] The same class for allowing method chaining.</returns>
        public NetworkPatch DisableReservableBandwidthLimit()
        {
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\Psched", "NonBestEffortLimit", 0);
            return this;
        }
    }
}
