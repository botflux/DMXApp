using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPackage.Files;
using System.IO;
using VPackage.Json;

namespace VPackage
{
    public class ConfigurationHandler
    {
        /// <summary>
        /// Chemin de la configuration
        /// </summary>
        private string configurationPath;

        /// <summary>
        /// La dernière configuration appliquée
        /// </summary>
        private ApplicationConfiguration lastApplicationConfiguration;

        /// <summary>
        /// Recoit ou renvoie la dernière configuration appliquée
        /// </summary>
        public ApplicationConfiguration LastApplicationConfiguration
        {
            get
            {
                return lastApplicationConfiguration;
            }

            private set
            {
                lastApplicationConfiguration = value;
            }
        }

        /// <summary>
        /// Chemin vers la configuration
        /// </summary>
        public string ConfigurationPath
        {
            get
            {
                return configurationPath;
            }
            set
            {
                configurationPath = value;
            }
        }

        /// <summary>
        /// Initialise une nouvelle instance de ConfigurationHandler
        /// </summary>
        /// <param name="configurationPath">Chemin vers la configuration</param>
        public ConfigurationHandler (string configurationPath)
        {
            ConfigurationPath = configurationPath;
            SearchConfiguration();
        }

        /// <summary>
        /// Cherche la configuration présente dans les fichiers de l'application
        /// </summary>
        public void SearchConfiguration ()
        {
            if (!File.Exists(ConfigurationPath))
            {
                LastApplicationConfiguration = ApplicationConfiguration.Default;
                FileManager.Write(ConfigurationPath, JSONSerializer.Serialize<ApplicationConfiguration>(LastApplicationConfiguration), FileManager.WriteOptions.CreateDirectory);
            }
            else
            {
                string json = FileManager.Read(ConfigurationPath);
                LastApplicationConfiguration = JSONSerializer.Deserialize<ApplicationConfiguration>(json);
            }
        }

        /// <summary>
        /// Change la configuration actuelle
        /// </summary>
        /// <param name="newConfiguration">Nouvelle configuration à appliquer</param>
        public void ModifyConfiguration (ApplicationConfiguration newConfiguration)
        {
            if (newConfiguration != LastApplicationConfiguration)
            {
                LastApplicationConfiguration = newConfiguration;
                FileManager.Write(ConfigurationPath, JSONSerializer.Serialize<ApplicationConfiguration>(LastApplicationConfiguration), FileManager.WriteOptions.CreateDirectory);
            }
        }
    }
}
