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
using System.Windows.Shapes;

namespace Bomberman
{
    /// <summary>
    /// Interaction logic for BomberMenu.xaml
    /// </summary>
    public partial class BomberMenu : Window
    {
        public int sliderValue;

        public BomberMenu()
        {
            InitializeComponent();
        }

        private void PlayButton(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow(sliderValue);
            ;
            main.Difficulty = sliderValue;
            main.Show();
            
            this.Close();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
            this.sliderValue = (int)slider.Value;
        }
    }
}
