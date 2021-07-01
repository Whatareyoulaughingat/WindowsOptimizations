using Microsoft.Win32;

namespace WindowsOptimizations.Core.GlobalData
{
    /// <summary>
    /// A struct containing various variables that store individual registry key paths used by this application.
    /// </summary>
    public struct RegistryKeys
    {
        /// <summary>
        /// The registry key location of the current TCP congestion provider.
        /// </summary>
        public static readonly string TCPCongestionProviderKey = $"{Registry.LocalMachine}" + "\\SYSTEM\\CurrentControlSet\\Control\\Nsi\\{eb004a03-9b1a-11d4-9123-0050047759bc}\\26";

        /// <summary>
        /// The registry key location of the Tcpip Parameters folder..
        /// </summary>
        public static readonly string TcpipParametersKey = $"{Registry.LocalMachine}\\SYSTEM\\CurrentControlSet\\Service\\Tcpip\\Parameters";

        /// <summary>
        /// The registry key location of the Service Provider folder.
        /// </summary>
        public static readonly string ServiceProviderKey = $"{Registry.LocalMachine}\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\ServiceProvider";

        /// <summary>
        /// The registry key location of the System Profile folder.
        /// </summary>
        public static readonly string SystemProfileKey = $"{Registry.LocalMachine}\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile";

        /// <summary>
        /// The registry key location of the Tcpip Interfaces folder.
        /// </summary>
        public static readonly string TcpipInterfacesKey = $"{Registry.LocalMachine}\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces";

        /// <summary>
        /// The registry key location of the Memory Management folder.
        /// </summary>
        public static readonly string MemoryManagementKey = $"{Registry.LocalMachine}\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Memory Management";

        /// <summary>
        /// The registry key location of the Psched folder.
        /// </summary>
        public static readonly string PschedKey = $"{Registry.LocalMachine}\\SOFTWARE\\Policies\\Microsoft\\Windows\\Psched";

        /// <summary>
        /// The registry key location of the Mouse folder.
        /// </summary>
        public static readonly string MouseKey = $"{Registry.CurrentUser}\\Control Panel\\Mouse";

        /// <summary>
        /// The registry key location of ThemeManager.
        /// </summary>
        public static readonly string ThemeManagerKey = $"{Registry.CurrentUser}\\Software\\Microsoft\\Windows\\CurrentVersion\\ThemeManager";

        /// <summary>
        /// The registry key location of the Game folder.
        /// </summary>
        public static readonly string GameTaskSystemProfileKey = $"{Registry.LocalMachine}\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Multimedia\\SystemProfile\\Games";

        /// <summary>
        /// The registry key location of the Control folder.
        /// </summary>
        public static readonly string CurrentControlKey = $"{Registry.LocalMachine}\\SYSTEM\\CurrentControlSet\\Control";
    }
}
