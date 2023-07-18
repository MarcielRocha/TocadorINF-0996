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

namespace TocadorV3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializePropertyValues();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Tocador.Stop();
            Tocador2.Stop();
            if (rbMusica.IsChecked==true) 
                Tocador2.Play();
            if(rbVideo.IsChecked==true) 
                Tocador.Play();

            InitializePropertyValues();
            UltimoComando.Text = "Tocando";

        }

        private void Button_Pause(object sender, RoutedEventArgs e)
        {
            Tocador2.Pause();
            Tocador.Pause();
            UltimoComando.Text = "Pausado";

        }

        // When the media opens, initialize the "Seek To" slider maximum value
        // to the total number of miliseconds in the length of the media clip.
        private void Element_MediaOpened(object sender, EventArgs e)
        {
            if (rbMusica.IsChecked == true)
                timelineSlider.Maximum = Tocador2.NaturalDuration.TimeSpan.TotalMilliseconds;
            if (rbVideo.IsChecked == true)
                timelineSlider.Maximum = Tocador.NaturalDuration.TimeSpan.TotalMilliseconds;

        }


        // When the media playback is finished. Stop() the media to seek to media start.
        private void Element_MediaEnded(object sender, EventArgs e)
        {
            Tocador.Stop();
            Tocador2.Stop();
            UltimoComando.Text = "Parando...";
        }

        void InitializePropertyValues()
        {
            // Set the media's starting Volume and SpeedRatio to the current value of the
            // their respective slider controls.
            Tocador.Volume = (double)volumeSlider.Value;
            Tocador.SpeedRatio = (double)speedRatioSlider.Value;
            Tocador2.Volume = (double)volumeSlider.Value;
            Tocador2.SpeedRatio = (double)speedRatioSlider.Value;

        }

        // Change the volume of the media.
        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            Tocador.Volume = (double)volumeSlider.Value;
            Tocador2.Volume = (double)volumeSlider.Value;
        }

        // Change the speed of the media.
        private void ChangeMediaSpeedRatio(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            Tocador.Stop();
            Tocador2.Stop();
            Tocador.SpeedRatio = (double)speedRatioSlider.Value;
            Tocador2.SpeedRatio = (double)speedRatioSlider.Value;
        }

        // Jump to different parts of the media (seek to).
        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            int SliderValue = (int)timelineSlider.Value;

            // Overloaded constructor takes the arguments days, hours, minutes, seconds, milliseconds.
            // Create a TimeSpan with miliseconds equal to the slider value.
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            if (rbMusica.IsChecked == true) Tocador2.Position = ts;
            if (rbVideo.IsChecked == true) Tocador.Position = ts;

        }

        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            Tocador.Stop();
            Tocador2.Stop();
            UltimoComando.Text = "Stop";
        }

    }
}
