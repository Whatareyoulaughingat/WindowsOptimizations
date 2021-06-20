using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using WindowsOptimizations.Core.Models;

namespace WindowsOptimizations.Core.Handlers
{
    /// <summary>
    /// A handler that manages some files used by this application.
    /// </summary>
    public class ConfigurationHandler
    {
        /// <summary>
        /// An instance of <see cref="UnnecessaryServices"/>.
        /// </summary>
        public static UnnecessaryServices UnnecessaryServicesInstance { get; set; }

        /// <summary>
        /// Serializes a specific file with an appropriate class.
        /// </summary>
        /// <returns>[<see cref="Task"/>] An asynchronous operation.</returns>
        public async Task SerializeAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                JsonSerializerOptions serializationOptions = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                };

                // A stream with some parameters for serializing.
                await using FileStream serializationStream = new FileStream(
                    path: filePath,
                    mode: FileMode.Create,
                    access: FileAccess.Write,
                    share: FileShare.None,
                    bufferSize: 4096,
                    useAsync: true);

                // Serializes the file.
                await JsonSerializer.SerializeAsync(
                    utf8Json: serializationStream,
                    value: new UnnecessaryServices(),
                    options: serializationOptions);

                // Setting the position of the stream to 0 otherwise no contents will be written to the file.
                serializationStream.Position = 0;
            }
        }

        public async Task DeserializeAsync(string filePath)
        {
            // A stream with some parameters for deserializing.
            await using FileStream derializationStream = new FileStream(
                path: filePath,
                mode: FileMode.Open,
                access: FileAccess.Read,
                share: FileShare.None,
                bufferSize: 4096,
                useAsync: true);

            UnnecessaryServicesInstance = await JsonSerializer.DeserializeAsync<UnnecessaryServices>(derializationStream);
        }
    }
}
