using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using WindowsOptimizations.Core.Handlers.Configuration.Data;

namespace WindowsOptimizations.Core.Handlers.Configuration
{
    /// <summary>
    /// Handles the way JSON serialization and deserialization works.
    /// </summary>
    public class ConfigurationHandler
    {
        /// <summary>
        /// Gets or sets a value indicating the instance of <see cref="WindowsServicesData"/>.
        /// </summary>
        public WindowsServicesData CurrentWindowsServicesDataInstance { get; set; }

        /// <summary>
        /// If the specified file path doesn't exist, creates a new one, serializes it and then deserializes it right after.
        /// </summary>
        /// <param name="jsonFilePath">The path of a JSON file.</param>
        /// <returns>[<see cref="Task"/>] An asynchronous operation.</returns>
        public void SerializeOnCreationAndDeserialize(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
            {
                string jsonData = JsonSerializer.Serialize(new WindowsServicesData(), new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonFilePath, jsonData);
            }

            DeserializeAsync(jsonFilePath);
        }

        /// <summary>
        /// Deserializes the values from a JSON file.
        /// </summary>
        /// <param name="jsonFilePath">The path of a JSON file.</param>
        /// <returns>[<see cref="Task"/>] An asynchronous operation.</returns>
        public void DeserializeAsync(string jsonFilePath)
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            CurrentWindowsServicesDataInstance = JsonSerializer.Deserialize<WindowsServicesData>(jsonData);
        }
    }
}