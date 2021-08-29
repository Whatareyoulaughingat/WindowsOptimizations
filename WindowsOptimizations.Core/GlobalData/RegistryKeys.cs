using Microsoft.Win32;

namespace WindowsOptimizations.Core.GlobalData
{
    /// <summary>
    /// A class containing various variables that store individual registry key paths used by this application.
    /// </summary>
    public static class RegistryKeys
    {
        /// <summary>
        /// Gets the registry key location of the current TCP congestion provider.
        /// </summary>
        public static string TCPCongestionProviderKey => $"{Registry.LocalMachine}" + "\\SYSTEM\\CurrentControlSet\\Control\\Nsi\\{eb004a03-9b1a-11d4-9123-0050047759bc}\\26";

        /// <summary>
        /// Gets the registry key location of the Tcpip Parameters folder..
        /// </summary>
        public static string TcpipParametersKey => $"{Registry.LocalMachine}\\SYSTEM\\CurrentControlSet\\Service\\Tcpip\\Parameters";

        /// <summary>
        /// Gets the registry key location of the Service Provider folder.
        /// </summary>
        public static string ServiceProviderKey => $"{Registry.LocalMachine}\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\ServiceProvider";

        /// <summary>
        /// Gets the registry key location of the System Profile folder.
        /// </summary>
        public static string SystemProfileKey => $"{Registry.LocalMachine}\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile";

        /// <summary>
        /// Gets the registry key location of the Tcpip Interfaces folder.
        /// </summary>
        public static string TcpipInterfacesKey => $"{Registry.LocalMachine}\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces";

        /// <summary>
        /// Gets the registry key location of the Memory Management folder.
        /// </summary>
        public static string MemoryManagementKey => $"{Registry.LocalMachine}\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management";

        /// <summary>
        /// Gets the registry key location of the Psched folder.
        /// </summary>
        public static string PschedKey => $"{Registry.LocalMachine}\\SOFTWARE\\Policies\\Microsoft\\Windows\\Psched";

        /// <summary>
        /// Gets the registry key location of the Mouse folder.
        /// </summary>
        public static string MouseKey => $"{Registry.CurrentUser}\\Control Panel\\Mouse";

        /// <summary>
        /// Gets the registry key location of ThemeManager.
        /// </summary>
        public static string ThemeManagerKey => $"{Registry.CurrentUser}\\Software\\Microsoft\\Windows\\CurrentVersion\\ThemeManager";

        /// <summary>
        /// Gets the registry key location of the Game folder.
        /// </summary>
        public static string GameTaskSystemProfileKey => $"{Registry.LocalMachine}\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\\Games";

        /// <summary>
        /// Gets the registry key location of the Control folder.
        /// </summary>
        public static string CurrentControlKey => $"{Registry.LocalMachine}\\SYSTEM\\CurrentControlSet\\Control";

        /// <summary>
        /// Gets the registry key location of the PriorityControl folder.
        /// </summary>
        public static string PriorityControlKey => $"{Registry.LocalMachine}\\SYSTEM\\CurrentControlSet\\Control\\PriorityControl";

        /// <summary>
        /// Gets the registry key location of the GameConfigStore folder.
        /// </summary>
        public static string GameConfigStoreKey => $"{Registry.CurrentUser}\\System\\GameConfigStore";
    }
}
