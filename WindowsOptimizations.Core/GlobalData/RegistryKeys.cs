using System;
using Microsoft.Win32;

#pragma warning disable CA1822

namespace WindowsOptimizations.Core.GlobalData
{
    /// <summary>
    /// A struct containing various variables that store individual registry key paths used by this application.
    /// </summary>
    public struct RegistryKeys : IEquatable<RegistryKeys>
    {
        /// <summary>
        /// Gets the registry key location of the current TCP congestion provider.
        /// </summary>
        public readonly string TCPCongestionProviderKey
        {
            get => $"{Registry.LocalMachine}" + "\\SYSTEM\\CurrentControlSet\\Control\\Nsi\\{eb004a03-9b1a-11d4-9123-0050047759bc}\\26";
        }

        /// <summary>
        /// Gets the registry key location of the Tcpip Parameters folder..
        /// </summary>
        public readonly string TcpipParametersKey
        {
            get => $"{Registry.LocalMachine}\\SYSTEM\\CurrentControlSet\\Service\\Tcpip\\Parameters";
        }

        /// <summary>
        /// Gets the registry key location of the Service Provider folder.
        /// </summary>
        public readonly string ServiceProviderKey
        {
            get => $"{Registry.LocalMachine}\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\ServiceProvider";
        }

        /// <summary>
        /// Gets the registry key location of the System Profile folder.
        /// </summary>
        public readonly string SystemProfileKey
        {
            get => $"{Registry.LocalMachine}\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile";
        }

        /// <summary>
        /// Gets the registry key location of the Tcpip Interfaces folder.
        /// </summary>
        public readonly string TcpipInterfacesKey
        {
            get => $"{Registry.LocalMachine}\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces";
        }

        /// <summary>
        /// Gets the registry key location of the Memory Management folder.
        /// </summary>
        public readonly string MemoryManagementKey
        {
            get => $"{Registry.LocalMachine}\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management";
        }

        /// <summary>
        /// Gets the registry key location of the Psched folder.
        /// </summary>
        public readonly string PschedKey
        {
            get => $"{Registry.LocalMachine}\\SOFTWARE\\Policies\\Microsoft\\Windows\\Psched";
        }

        /// <summary>
        /// Gets the registry key location of the Mouse folder.
        /// </summary>
        public readonly string MouseKey
        {
            get => $"{Registry.CurrentUser}\\Control Panel\\Mouse";
        }

        /// <summary>
        /// Gets the registry key location of ThemeManager.
        /// </summary>
        public readonly string ThemeManagerKey
        {
            get => $"{Registry.CurrentUser}\\Software\\Microsoft\\Windows\\CurrentVersion\\ThemeManager";
        }

        /// <summary>
        /// Gets the registry key location of the Game folder.
        /// </summary>
        public readonly string GameTaskSystemProfileKey
        {
            get => $"{Registry.LocalMachine}\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\\Games";
        }

        /// <summary>
        /// Gets the registry key location of the Control folder.
        /// </summary>
        public readonly string CurrentControlKey
        {
            get => $"{Registry.LocalMachine}\\SYSTEM\\CurrentControlSet\\Control";
        }

        public bool Equals(RegistryKeys other)
            => (TCPCongestionProviderKey,
            TcpipParametersKey,
            ServiceProviderKey,
            SystemProfileKey,
            TcpipInterfacesKey,
            MemoryManagementKey,
            PschedKey,
            MouseKey,
            ThemeManagerKey,
            GameTaskSystemProfileKey,
            CurrentControlKey) == (other.TCPCongestionProviderKey, other.TcpipParametersKey, other.ServiceProviderKey, other.SystemProfileKey, other.TcpipInterfacesKey, other.MemoryManagementKey, other.PschedKey, other.MouseKey, other.ThemeManagerKey, other.GameTaskSystemProfileKey, other.CurrentControlKey);

        public override bool Equals(object obj)
            => (obj is RegistryKeys registryKeys) && Equals(registryKeys);

        public override int GetHashCode()
            => (TCPCongestionProviderKey,
            TcpipParametersKey,
            ServiceProviderKey,
            SystemProfileKey,
            TcpipInterfacesKey,
            MemoryManagementKey,
            PschedKey,
            MouseKey,
            ThemeManagerKey,
            GameTaskSystemProfileKey,
            CurrentControlKey)
            .GetHashCode();

        public static bool operator ==(RegistryKeys left, RegistryKeys right)
            => Equals(left, right);

        public static bool operator !=(RegistryKeys left, RegistryKeys right)
            => !Equals(left, right);
    }
}
