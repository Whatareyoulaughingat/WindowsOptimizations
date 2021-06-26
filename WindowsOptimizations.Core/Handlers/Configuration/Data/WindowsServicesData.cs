using System;

namespace WindowsOptimizations.Core.Handlers.Configuration.Data
{
    [Serializable]
    public class WindowsServicesData
    {
        public string[] ServiceCollection { get; set; }

        public WindowsServicesData()
        {
            ServiceCollection = new string[]
            {
                "AJRouter", // AllJoyn Router Service
                "ALG", // Application Layer Gateway Service
                "bthserv", // Bluetooth Support Device
                "PeerDistSvc", // BranchCache - Pro Only
                "CaptureService_?????", // OneCore Capture Service
                "CertPropSvc", // Certificate Propagation - Pro Only
                "dmwappushsvc",
                "MapsBroker", // Downloaded Maps Manager
                "lfsvc", // Geolocation Service
                "HvHost", // HV Host Service
                "vmickvpexchange", // Hyper-V Data Exchange Service
                "vmicguestinterface", // Hyper-V Guest Service Interface
                "vmicshutdown", // Hyper-V Guest Shutdown Service
                "vmicheartbeat", // Hyper-V Guest Heartbeat Service
                "vmicvmsession", // Hyper-V Powershell Direct Service
                "vmicrdv", // Hyper-V Remote Desktop Virtualization Service
                "vmictimesync", // Hyper-V Time Synchronization Service
                "vmicvss", // Hyper-V Volume Shadow Copy Requestor
                "irmon", // Infrared Monitor Service
                "SharedAccess", // Internet Connection Sharing (ICS)
                "MSiSCSI", // Microsoft iSCSI Initiator Service
                "SmsRouter", // Microsoft Windows SMS Router Service
                "NaturalAuthentication", // Natural Authentication
                "Netlogon",
                "NcdAutoSetup", // Network Connected Devices Auto-Setup
                "CscService", // Offline Files
                "WpcMonSvc", // Parental Controls
                "SEMgrSvc", // Payments and NFC/SE Manager
                "PhoneSvc", // Phone Service
                "SessionEnv", // Remote Desktop Configuration
                "TermService", // Remote Desktop Services
                "UmRdpService", // Remote Desktop Services UserMode Port Redirector
                "RpcLocator", // Remote Procedure Call (RPC) Locator
                "RetailDemo", // Retail Demo Service
                "ScDeviceEnum", // Smart Card Device Enumeration Service
                "SCPolicySvc", // Smart Card Removal Policy
                "SNMPTRAP",
                "WebClient",
                "WFDSConSvc", // Wi-Fi Direct Services Connection Manager Service
                "wcncsvc", // Windows Connect Now - Config Registar
                "wisvc", // Windows Insider Service
                "icssvc", // Windows Mobile Hotspot Service
                "WinRM", // Windows Remote Management (WS-Management)
                "WwanSvc", // WWAN AutoConfig
                "XblAuthManager", // Xbox Live Auth Manager
                "XblGameSave", // Xbox Live Game Save
                "XboxNetApiSvc", // Xbox Live Networking Service
            };
        }
    }
}