using System;
using System.IO;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : UserControl
    {
        public string dirVid { get; set; }

        public Welcome()
        {
            InitializeComponent();

            //set vid source
            SetVid();
            // Initial volume is set to 0
            myMediaElement.Volume = 0;
        }

        //get location of vid
        public void SetVid()
        {
            string dir = WpfApp1.MainWindow.cwd;
            myMediaElement.Source = new Uri($"{dir}\\Assets\\vid.mp4");
        }



        // Start video Volume controls
        // Change the volume of the media.
        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            myMediaElement.Volume = (double)volumeSlider.Value;
        }

        void InitializePropertyValues()
        {
            // Set the media's starting Volume and SpeedRatio to the current value of the
            // their respective slider controls.
            myMediaElement.Volume = (double)volumeSlider.Value;
        }
    }
}
