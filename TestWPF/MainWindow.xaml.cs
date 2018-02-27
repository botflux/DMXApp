using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using VPackage.Graphics;
using VPackage;
using VPackage.Json;
using VPackage.Files;
using System.IO;

namespace TestWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ApplicationConfiguration Configuration
        {
            get;
            set;
        }

        /// <summary>
        /// Application
        /// </summary>
        private MyApp myApp;

        public MainWindow()
        {
            InitializeComponent();
            ApplicationConfiguration config;

            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + MyApp.FILE_NAME))
                config = ApplicationConfiguration.Default;
            else
            {
                string json = FileManager.Read(AppDomain.CurrentDomain.BaseDirectory + MyApp.FILE_NAME);
                config = JSONSerializer.Deserialize<ApplicationConfiguration>(json);
            }
            Configuration = config;
            
            myApp = new MyApp(config);
            DataContext = this.myApp;

            myApp.MainColor.PropertyChanged += (sender, e) => {
                rct_blended.Fill = new SolidColorBrush((sender as ColorWrapper).ToColor());
                rct_blue.Fill = new SolidColorBrush((sender as ColorWrapper).BlueBalance);
                rct_green.Fill = new SolidColorBrush((sender as ColorWrapper).GreenBalance);
                rct_red.Fill = new SolidColorBrush((sender as ColorWrapper).RedBalance);
            };

            myApp.AppServer.OnMessageReceived += (message) => {
                MessageBox.Show(message);
            };
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
        }
    }
}