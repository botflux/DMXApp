using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VPackage
{
    [DataContract]
    public class ApplicationConfiguration : INotifyPropertyChanged
    {
        /// <summary>
        /// Nom d'hôte distant
        /// </summary>
        [DataMember]
        private string hostname;
        /// <summary>
        /// Port distant
        /// </summary>
        [DataMember]
        private int port;
        /// <summary>
        /// Cible à piloter
        /// </summary>
        [DataMember]
        private int target;

        /// <summary>
        /// Port de reception
        /// </summary>
        [DataMember]
        private int receivePort;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName]string str = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(str));
            }
        }

        /// <summary>
        /// Renvoie ou renseigne le nom d'hôte distant
        /// </summary>
        public string Hostname
        {
            get
            {
                return hostname;
            }

            set
            {
                if (Hostname != value)
                {
                    hostname = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Renvoie ou renseigne le port distant
        /// </summary>
        public int Port
        {
            get
            {
                return port;
            }

            set
            {
                if (Port != value)
                {
                    port = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Renvoie ou renseigne la cible à piloter
        /// </summary>
        public int Target
        {
            get
            {
                return target;
            }

            set
            {
                if (Target != value)
                {
                    target = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int ReceivePort
        {
            get
            {
                return receivePort;
            }

            set
            {
                if (ReceivePort != value)
                { 
                    receivePort = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ApplicationConfiguration ()
        {
            Hostname = "";
            Port = 0;
            Target = 0;
        }
    }
}
