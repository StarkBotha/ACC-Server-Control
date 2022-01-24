using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace api.Data
{
    public class FileManager : IFileManager
    {
        private readonly ILogger<ProcessHost> logger;
        public IConfiguration Configuration { get; }

        public FileManager(ILogger<ProcessHost> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.Configuration = configuration;
        }

        public string GetSettings()
        {
            string settingsPath = $"{Configuration["AppSettings:ProcessPath"]}\\cfg\\settings.json";

            return System.IO.File.ReadAllText(settingsPath);
        }

        public string GetEvent()
        {
            string settingsPath = $"{Configuration["AppSettings:ProcessPath"]}\\cfg\\event.json";

            return System.IO.File.ReadAllText(settingsPath);
        }

        public string GetEventRules()
        {
            string settingsPath = $"{Configuration["AppSettings:ProcessPath"]}\\cfg\\eventRules.json";

            return System.IO.File.ReadAllText(settingsPath);
        }

        public string GetVersion()
        {
            return "v1.2";
        }

        public void SaveSettings(string text)
        {
            File.WriteAllText($"{Configuration["AppSettings:ProcessPath"]}\\cfg\\settings.json",text);
        }

        public void SaveEvent(string text)
        {
            File.WriteAllText($"{Configuration["AppSettings:ProcessPath"]}\\cfg\\event.json",text);
        }

        public void SaveEventRules(string text)
        {
            File.WriteAllText($"{Configuration["AppSettings:ProcessPath"]}\\cfg\\eventRules.json",text);
        }
    }
}