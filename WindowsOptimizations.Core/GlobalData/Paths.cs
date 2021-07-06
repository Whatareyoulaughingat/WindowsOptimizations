﻿using System;

namespace WindowsOptimizations.Core.GlobalData
{
    /// <summary>
    /// A struct containing directory and file paths to be used for this application.
    /// </summary>
    public struct Paths
    {
        /// <summary>
        /// The base directory path of this application.
        /// </summary>
        public static readonly string BasePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WindowsOptimizations";

        /// <summary>
        /// A file used for configuring which unnecessary windows service is going to be disabled.
        /// </summary>
        public static readonly string UnnecessaryWindowsServicesJsonFile = BasePath + "\\UnnecessaryWindowsServices.json";
    }
}