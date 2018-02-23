using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VPackage;

namespace TestWPF
{
    /// <summary>
    /// Logique d'interaction pour Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private List<StackPanel> sps = new List<StackPanel>();
        private ApplicationConfiguration configuration = new ApplicationConfiguration();

        public ApplicationConfiguration Configuration
        {
            get
            {
                return configuration;
            }

            set
            {
                configuration.Hostname = value.Hostname;
                configuration.Port = value.Port;
                configuration.Target = value.Target;
                configuration.ReceivePort = value.ReceivePort;
            }
        }

        public Settings()
        {
            InitializeComponent();
            DataContext = Configuration;

            sps.Add(sp_network);
            sps.Add(sp_address);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selected = (sender as ListBox).SelectedIndex;
            Show(selected);
        }

        private void Show (int index)
        {
            if (index < 0 || index >= sps.Count)
                throw new IndexOutOfRangeException("Accès à un menu inexistant");

            foreach(StackPanel sp in sps)
            {
                sp.Visibility = Visibility.Hidden;
            }
            sps[index].Visibility = Visibility.Visible;
        }
        
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btn_validate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DialogResult = true;
            }
            catch (Exception ex)
            {
                lbl_error.Content = ex.Message;
            }
        }
    }
}
