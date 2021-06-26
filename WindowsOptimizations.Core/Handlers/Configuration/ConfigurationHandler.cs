using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using WindowsOptimizations.Core.Handlers.Configuration.Data;

namespace WindowsOptimizations.Core.Handlers.Configuration
{
    public class ConfigurationHandler
    {
        public static WindowsServicesData WindowsServicesDataInstance { get; set; }

        /// <summary>
        /// If the specified file path doesn't exist, creates a new one, serializes it and then deserializes it right after.
        /// </summary>
        /// <param name="jsonFilePath"></param>
        /// <returns></returns>
        public static async Task SerializeOnCreationAndDeserialize(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
            {
                await SerializeAsync(jsonFilePath);
            }

            await DeserializeAsync(jsonFilePath);
        }

        public static async Task SerializeAsync(string jsonFilePath)
        {
            await using FileStream serializationStream = File.OpenWrite(jsonFilePath);
            await JsonSerializer.SerializeAsync(serializationStream, new WindowsServicesData(), new JsonSerializerOptions() { WriteIndented = true });
        }

        public static async Task DeserializeAsync(string jsonFilePath)
        {
            await using FileStream deserializationStream = File.OpenRead(jsonFilePath);
            WindowsServicesDataInstance = await JsonSerializer.DeserializeAsync<WindowsServicesData>(deserializationStream);
        }
    }
}