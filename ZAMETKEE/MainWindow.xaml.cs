using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Speech.Synthesis;
using System.Windows.Shapes;
using System.Reflection;

namespace ZAMETKEE
{
    public partial class MainWindow : Window
    {
        public static string putch = @"";

        SpeechSynthesizer synth;
        public string Speech;

        public MainWindow()
        {
            InitializeComponent();
            pause.Visibility = Visibility.Hidden;

            //FileInfo fileInfo = new FileInfo(putch);
            //if (fileInfo.Exists)
            //{
            //    putch = File.ReadAllText(putch);
            //}
            //else
            //{
            //    File.Create(putch);
            //}
        }

        public MediaPlayer player = new MediaPlayer();


        private void ShriftChange(object sender, EventArgs e)
        {
            string str = shrift.Text;
            tex.FontFamily = new FontFamily(str);
        }

        private void ColourShriftChange(object sender, EventArgs e)
        {
            string str = word.Text;
            switch (str)
            {
                case "green":
                    tex.Foreground = Brushes.Green;
                    break;
                case "black":
                    tex.Foreground = Brushes.Black;
                    break;
                case "red":
                    tex.Foreground = Brushes.Red;
                    break;
                case "yellow":
                    tex.Foreground = Brushes.Yellow;
                    break;
            }
        }

        private void ColourBackgroundChange(object sender, EventArgs e)
        {
            string str = back.Text;
            switch (str)
            {
                case "green":
                    tex.Background = Brushes.Green;
                    break;
                case "black":
                    tex.Background = Brushes.Black;
                    break;
                case "red":
                    tex.Background = Brushes.Red;
                    break;
                case "yellow":
                    tex.Background = Brushes.Yellow;
                    break;
                case "white":
                    tex.Background = Brushes.White;
                    break;
            }
        }

        private void CB(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.IsChecked == true & checkBox.Content.ToString() == "Bolt")
            {
                tex.FontWeight = FontWeights.Bold;
            }
            else if (checkBox.IsChecked == false & checkBox.Content.ToString() == "Bolt")
            {
                tex.FontWeight = FontWeights.Regular;
            }

            else if (checkBox.IsChecked == true & checkBox.Content.ToString() == "Cursive")
            {
                tex.FontStyle = FontStyles.Italic;

            }
            else
            {
                tex.FontStyle = FontStyles.Normal;
            }
        }

        private void SIZE(object sender, RoutedEventArgs e)
        {
            try
            {
                tex.FontSize = Int32.Parse(size.Text);
            }
            catch (FormatException)
            {
                tex.FontSize = 12;
            }

        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StreamWriter stream = new StreamWriter(putch, false))
                {
                    stream.WriteLine(tex.Text);
                }
                MessageBox.Show("Всё сохранено успешно");
            }
            catch (Exception) { MessageBox.Show("Ты, ВАЛЕНОК!"); }
        }

        public string GetPath()
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.Filter = "txt|*.txt|mp3|*.mp3"; // Фильтр файлов в проводнике
                Nullable<bool> result = dlg.ShowDialog();

                if (result == true)
                {
                    return dlg.FileName;
                }
                return null;
            }
            catch (Exception) { return null; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                putch = GetPath();
                string[] names = putch.Split(new char[] { (char)92 });
                string[] expansion = names[names.Length - 1].Split(new char[] { '.' });
                if (expansion[1] == "txt")
                {
                    tex.Text = "";
                    tex.Text = File.ReadAllText(putch);
                }
                else
                {
                    LS.Items.Add(putch);
                    //FileInfo fileInfo = new FileInfo(putch);
                    //if (fileInfo.Exists)
                    //{
                    //    using (StreamWriter stream = new StreamWriter(putch, false))
                    //    {
                    //        stream.WriteLine(putch);
                    //    }
                    //}
                    //else
                    //{
                    //    File.Create(putch);
                    //    using (StreamWriter stream = new StreamWriter(putch, false))
                    //    {
                    //        stream.WriteLine(putch);
                    //    }
                    //}
                    player.Open(new Uri(putch, UriKind.Relative));
                    player.Play();
                }
            }
            catch (Exception) { }
        }

        private void Speake(object sender, RoutedEventArgs e)
        {
            try
            {
                Speech = File.ReadAllText(putch);
                synth = new SpeechSynthesizer();
                pause.Visibility = Visibility.Visible;
                speak.Visibility = Visibility.Hidden;
                synth.SetOutputToDefaultAudioDevice();
                synth.SpeakAsync(Speech);

            }
            catch (Exception) { }

        }

        private void Pause(object sender, RoutedEventArgs e)
        {
            try
            {
                speak.Visibility = Visibility.Visible;
                pause.Visibility = Visibility.Hidden;
                synth.SpeakAsyncCancelAll();
            }
            catch (Exception) { }
        }

        private void MP3_P(object sender, RoutedEventArgs e)
        {
            player.Play();
        }
        private void MP3_S(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }
        private void DL(object sender, RoutedEventArgs e)
        {
            LS.Items.RemoveAt(0);
        }
        private void DA(object sender, RoutedEventArgs e)
        {
            LS.Items.Clear();
        }

        private void LS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            putch = LS.SelectedItems[0].ToString();
            player.Open(new Uri(putch, UriKind.Relative));
            player.Play();
        }
    }
}