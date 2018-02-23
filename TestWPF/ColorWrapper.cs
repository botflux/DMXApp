using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media;
using System.Runtime.CompilerServices;
using VPackage.Parser;

namespace VPackage.Graphics
{

    /// <summary>
    /// Represente une couleur
    /// </summary>
    public class ColorWrapper : INotifyPropertyChanged, IParsable
    {
        private double r;
        private double g;
        private double b;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged ([CallerMemberName]string str = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(str));
            }
        }

        /// <summary>
        /// Composante rouge
        /// </summary>
        public double R
        {
            get
            {
                return r;
            }

            set
            {
                if (r != value)
                {
                    r = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Composante verte
        /// </summary>
        public double G
        {
            get
            {
                return g;
            }

            set
            {
                if (g != value)
                {
                    g = value;
                    NotifyPropertyChanged();
                }

            }
        }

        /// <summary>
        /// Composante bleu
        /// </summary>
        public double B
        {
            get
            {
                return b;
            }

            set
            {
                if (b != value)
                {
                    b = value;
                    NotifyPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Renvoie la composante rouge sous forme de couleur
        /// </summary>
        public Color RedBalance
        {
            get
            {
                return Color.FromRgb(Convert.ToByte(Convert.ToInt32(Math.Round(R))), 0, 0);
            }
        }

        /// <summary>
        /// Renvoie la composante verte sous forme de couleur
        /// </summary>
        public Color GreenBalance
        {
            get
            {
                return Color.FromRgb(0, Convert.ToByte(Convert.ToInt32(Math.Round(G))), 0);
            }
        }

        /// <summary>
        /// Renvoie la composante bleu sous forme de couleur
        /// </summary>
        public Color BlueBalance
        {
            get
            {
                return Color.FromRgb(0, 0, Convert.ToByte(Convert.ToInt32(Math.Round(B))));
            }
        }

        /// <summary>
        /// Renvoie la couleur sous forme de couleur
        /// </summary>
        /// <returns></returns>
        public Color ToColor()
        {
            byte r = Convert.ToByte(Convert.ToInt32(Math.Round(this.R)));
            byte g = Convert.ToByte(Convert.ToInt32(Math.Round(this.G)));
            byte b = Convert.ToByte(Convert.ToInt32(Math.Round(this.B)));

            return Color.FromRgb(r, g, b);
        }

        public string Encode()
        {
            Color toColor = ToColor();

            return FrameParser.Encode(new List<DataWrapper>() {
                new DataWrapper("red", toColor.R),
                new DataWrapper("green", toColor.G),
                new DataWrapper("blue", toColor.B)
            });
        }
    }
}
