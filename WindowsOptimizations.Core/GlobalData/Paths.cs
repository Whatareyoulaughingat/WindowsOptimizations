using System;

#pragma warning disable CA1822

namespace WindowsOptimizations.Core.GlobalData
{
    /// <summary>
    /// A struct containing directory and file paths to be used for this application.
    /// </summary>
    public struct Paths : IEquatable<Paths>
    {
        /// <summary>
        /// Gets the base directory path of this application.
        /// </summary>
        public readonly string BasePath
        {
            get => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WindowsOptimizations";
        }

        /// <summary>
        /// Gets a file used for configuring which unnecessary windows service is going to be disabled.
        /// </summary>
        public readonly string UnnecessaryWindowsServicesJsonFile
        {
            get => BasePath + "\\UnnecessaryWindowsServices.json";
        }

        public bool Equals(Paths other)
            => (BasePath, UnnecessaryWindowsServicesJsonFile) == (other.BasePath, other.UnnecessaryWindowsServicesJsonFile);

        public override bool Equals(object obj)
            => (obj is Paths paths) && Equals(paths);

        public override int GetHashCode()
            => (BasePath, UnnecessaryWindowsServicesJsonFile).GetHashCode();

        public static bool operator ==(Paths left, Paths right)
            => Equals(left, right);

        public static bool operator !=(Paths left, Paths right)
            => !Equals(left, right);
    }
}