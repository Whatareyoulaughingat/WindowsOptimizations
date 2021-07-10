using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using WindowsOptimizations.Core.Handlers.Configuration.Data;

namespace WindowsOptimizations.Core.Handlers.Configuration
{
    /// <summary>
    /// Handles the way JSON serialization and deserialization works.
    /// </summary>
    public static class ConfigurationHandler
    {
        /// <summary>
        /// Gets or sets a value indicating the instance of <see cref="WindowsServicesData"/>.
        /// </summary>
        public static WindowsServicesData WindowsServicesDataInstance { get; set; }

        /// <summary>
        /// If the specified file path doesn't exist, creates a new one, serializes it and then deserializes it right after.
        /// </summary>
        /// <param name="jsonFilePath">The path of a JSON file.</param>
        /// <returns>[<see cref="Task"/>] An asynchronous operation.</returns>
        public static async Task SerializeOnCreationAndDeserialize(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
            {
                await using FileStream serializationStream = File.OpenWrite(jsonFilePath);
                await JsonSerializer.SerializeAsync(serializationStream, new WindowsServicesData(), new JsonSerializerOptions { WriteIndented = true });
            }

            await DeserializeAsync(jsonFilePath);
        }

        /// <summary>
        /// Deserializes the values from a JSON file.
        /// </summary>
        /// <param name="jsonFilePath">The path of a JSON file.</param>
        /// <returns>[<see cref="Task"/>] An asynchronous operation.</returns>
        public static async Task DeserializeAsync(string jsonFilePath)
        {
            await using FileStream deserializationStream = File.OpenRead(jsonFilePath);
            WindowsServicesDataInstance = await JsonSerializer.DeserializeAsync<WindowsServicesData>(deserializationStream);
        }
    }
}