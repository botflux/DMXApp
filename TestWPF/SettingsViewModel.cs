using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using VPackage;

namespace TestWPF
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private ApplicationConfiguration localConfiguration;
        
        public ApplicationConfiguration LocalConfiguration
        {
            get
            {
                return localConfiguration;
            }
            set
            {
                if (localConfiguration != value)
                {
                    localConfiguration = value;
                    NotifyPropertyChanged();
                }
            }
        } 

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged ([CallerMemberName] string str = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(str));
        }
    }
}
