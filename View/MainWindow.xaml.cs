using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using Microsoft.Win32;
using EM3.ViewModel;

namespace EM3.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int EnterPressed { get; set; } = 1;
        private string FilePath { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new Em3ViewModel();
        }

        private void TBInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                TbInput.IsEnabled = false;

                SaveFile();

                ShowSave();

                TbInput.IsEnabled = true;
            }
            if (e.Key == Key.Return)
            {
                EnterPressed++;
                TbInfo.Text = EnterPressed.ToString();
            }
        }

        private void ClearTbInfo(object o)
        {
            Dispatcher.Invoke(() => TbInfo.Text = "");
        }

        private void TBInput_MouseEnter(object sender, MouseEventArgs e)
        {
            TbInput.BorderBrush = Brushes.Black;
        }

        private void TBInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EnterPressed++;

                SpIndex.Children.Add(new TextBlock()
                {
                    Text = EnterPressed.ToString(),
                    Padding = new Thickness(8, 0, 0, 0),
                    Foreground = Brushes.White,
                    FontSize = 15
                });
            }
            else if (e.Key == Key.Back)
            {
                int numLines = GetNumLines(TbInput.Text);

                if (numLines < EnterPressed && EnterPressed > 1)
                {
                    SpIndex.Children.RemoveAt(--EnterPressed);
                }
            }
        }

        private int GetNumLines(string str)
        {
            int count = 1;

            foreach (char c in str)
            {
                if (c == '\n') count++;
            }

            return count;
        }

        private void MIExit_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MIOpen_OnClick(object sender, RoutedEventArgs e)
        {
            var fileContent = string.Empty;

            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Txt files (*.txt)|*.txt|EM2 files (*.em2)|*.em2";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;

                var fileStream = openFileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }
            }

            TbInput.Text = fileContent;
            TbFileName.Text = GetFileName();
            
            Reload();
        }

        private string GetFileName()
        {
            var str = FilePath.Split("\\");

            return str[^1];
        }

        private void MISave_OnClick(object sender, RoutedEventArgs e)
        {
            SaveFile();

            ShowSave();
        }

        private void SaveFile()
        {
            Stream myStream;
            SaveFileDialog saveFile = new();

            saveFile.Filter = "Txt files (*.txt)|*.txt|EM2 files (*.em2)|*.em2";
            saveFile.FilterIndex = 2;
            saveFile.RestoreDirectory = true;
            saveFile.OverwritePrompt = false;

            if (FilePath == null)
            {
                if (saveFile.ShowDialog() == true)
                {
                    myStream = saveFile.OpenFile();

                    using (StreamWriter writer = new(myStream))
                    {
                        writer.WriteLine(TbInput.Text);
                    }

                    myStream.Close();
                }
            }
            else
            {
                saveFile.FileName = FilePath;

                myStream = saveFile.OpenFile();

                using (StreamWriter writer = new(myStream))
                {
                    writer.WriteLine(TbInput.Text);
                }

                myStream.Close();

            }
        }

        private void ShowSave()
        {
            TbInfo.Text = "Saved";

            TimerCallback callback = new(ClearTbInfo);
            Timer timer = new(callback, null, 2000, 0);
        }

        private void Reload()
        {
            int numLines = GetNumLines(TbInput.Text);

            if (numLines < EnterPressed)
            {
                while (numLines < EnterPressed && EnterPressed > 1)
                {
                    SpIndex.Children.RemoveAt(--EnterPressed);
                }
            }
            else if (numLines > EnterPressed)
            {
                while (numLines > EnterPressed)
                {
                    SpIndex.Children.Add(new TextBlock()
                    {
                        Text = (++EnterPressed).ToString(),
                        Padding = new Thickness(5, 0, 5, 0),
                        Foreground = Brushes.White,
                        FontSize = 15,
                        HorizontalAlignment = HorizontalAlignment.Center
                    });
                }
            }

            TbInfo.Text = "";
        }

        private void MIReload_OnClick(object sender, RoutedEventArgs e)
        {
            Reload();
        }

    }
}
