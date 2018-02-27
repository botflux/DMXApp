using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPackage.Network;
using VPackage.Graphics;
using System.Windows.Media;
using System.Drawing;
using VPackage.Parser;
using System.Windows.Input;
using System.Windows;
using System.Net;
using VPackage.Json;
using VPackage.Files;
using System.IO;
using TestWPF;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VPackage
{
    public class ApplicationViewModel
    {
        public const string FILE_NAME = @"\config\settings.json";

        #region Fields
        private ConfigurationHandler configurationHandler;
        /// <summary>
        /// Client utilisé pour l'application
        /// </summary>
        private Client appClient;
        /// <summary>
        /// Serveur utilisé pour l'application
        /// </summary>
        private Server appServer;
        /// <summary>
        /// Représente la couleur principale
        /// </summary>
        private ColorWrapper mainColor;
        /// <summary>
        /// Cible à controler
        /// </summary>
        private int target;
        #endregion

        #region Properties
        /// <summary>
        /// Renvoie l'instance du Client utilisé pour cette application
        /// </summary>
        public Client AppClient
        {
            get
            {
                return appClient;
            }
        }

        /// <summary>
        /// Renvoie ou renseigne la couleur principale
        /// </summary>
        public ColorWrapper MainColor
        {
            get
            {
                return mainColor;
            }

            set
            {
                mainColor = value;
            }
        }

        /// <summary>
        /// Renvoie la commande servant à envoyer la trame de la couleur au serveur
        /// </summary>
        public ICommand SendData
        {
            get
            {
                return sendData;
            }
        }

        /// <summary>
        /// Renvoie la commande servant à changer la configuration de l'application
        /// </summary>
        public ICommand ChangeConfiguration
        {
            get
            {
                return changeConfiguration;
            }
        }

        /// <summary>
        /// Renvoie ou renseigne la cible à controler
        /// </summary>
        public int Target
        {
            get
            {
                return target;
            }

            set
            {
                target = value;
            }
        }

        /// <summary>
        /// Renvoie l'instance du Server utilisé pour cette application
        /// </summary>
        public Server AppServer
        {
            get
            {
                return appServer;
            }
        }
        
        #endregion

        #region Constructors
        public ApplicationViewModel ()
        {
            configurationHandler = new ConfigurationHandler(AppDomain.CurrentDomain.BaseDirectory + FILE_NAME);

            MessageBox.Show(configurationHandler.LastApplicationConfiguration.ToString());

            appClient = new Client(configurationHandler.LastApplicationConfiguration.Hostname, configurationHandler.LastApplicationConfiguration.Port);
            appServer = new Server(configurationHandler.LastApplicationConfiguration.ReceivePort);
            mainColor = new ColorWrapper();
            Target = configurationHandler.LastApplicationConfiguration.Target;
            appServer.StartListen();
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return string.Format("Hostname: {0}; Port: {1}", AppClient.EndPoint.Address.ToString(), AppClient.EndPoint.Port);
        }
        
        /// <summary>
        /// Met à jour la configuration de l'application
        /// </summary>
        /// <param name="config"></param>
        public void ApplyNewConfiguration(ApplicationConfiguration config)
        {
            AppClient.EndPoint.Address = IPAddress.Parse(config.Hostname);
            AppClient.EndPoint.Port = config.Port;
            Target = config.Target;
            AppServer.EndPoint.Port = config.Port;

            string json = JSONSerializer.Serialize<ApplicationConfiguration>(config);
            FileManager.Write(AppDomain.CurrentDomain.BaseDirectory + FILE_NAME, json, FileManager.WriteOptions.CreateDirectory);
        }
        #endregion

        #region Command

        /// <summary>
        /// Commande qui envoie la trame de la couleur au serveur
        /// </summary>
        private ICommand sendData = new RelayCommand<ApplicationViewModel>((app) => 
        {
            string colorEncoded = app.MainColor.Encode();
            string target = FrameParser.Encode("cible", app.Target);

            string merged = FrameParser.Merge(target, colorEncoded);

            app.appClient.Send(merged);
        });

        /// <summary>
        /// Commande qui change la configuration de l'application
        /// </summary>
        private ICommand changeConfiguration = new RelayCommand<ApplicationViewModel>((app) =>
        {
            
        });

        #endregion
    }
}
