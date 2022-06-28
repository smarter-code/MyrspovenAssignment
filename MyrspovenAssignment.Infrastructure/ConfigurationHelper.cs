using Microsoft.Extensions.Configuration;

namespace MyrspovenAssignment.Infrastructure
{
    public static class ConfigurationHelper
    {
        public static string ReadJsonConfig(string configName, string jsonFilePath)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(jsonFilePath);

            var root = builder.Build();
            var configValue = root.GetConnectionString(configName);
            return configValue;
        }
    }
}
