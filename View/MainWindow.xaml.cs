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
                TbCommands.IsEnabled = false;
                TbOut.IsEnabled = false;
                TbRegA.IsEnabled = false;
                TbRegB.IsEnabled = false;
                TbOther.IsEnabled = false;

                SaveFile();

                ShowSave();

                TbCommands.IsEnabled = true;
                TbOut.IsEnabled = true;
                TbRegA.IsEnabled = true;
                TbRegB.IsEnabled = true;
                TbOther.IsEnabled = true;
            }

            if (e.Key == Key.Return)
            {
                EnterPressed++;
                TbInfo.Text = EnterPressed.ToString();
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.O)
            {
                MIOpen_OnClick(null, null);
            }

            if (e.Key == Key.F5)
            {
                MiStart.Command.Execute(null);
            }
        }

        private void ClearTbInfo(object o)
        {
            Dispatcher.Invoke(() => TbInfo.Text = "");
        }

        private void TBInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EnterPressed++;

                SpIndex.Children.Add(new TextBlock()
                {
                    Text = EnterPressed.ToString(),
                    Foreground = Brushes.White,
                    FontSize = 15,
                    HorizontalAlignment = HorizontalAlignment.Center
                });

                AddLines();
                TbCommands.Focus();
                TbCommands.CaretIndex = TbCommands.Text.Length;
            }
            else if (e.Key == Key.Back)
            {
                int numLines = GetNumLines(TbCommands.Text);

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
            openFileDialog.Filter = "Txt files (*.txt)|*.txt|EM3 files (*.em3)|*.em3";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;

                var fileStream = openFileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    //fileContent = reader.ReadToEnd();
                    (DataContext as Em3ViewModel)!.FileContent = reader.ReadToEnd();
                }
            }

            //TbInput.Text = fileContent;
            TbFileName.Text = GetFileName();
            
            MiOpen.Command.Execute(null);
            Reload();
        }

        private string GetFileName()
        {
            if (FilePath == null)
                return "";

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

            saveFile.Filter = "Txt files (*.txt)|*.txt|EM2 files (*.em3)|*.em3";
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
                        writer.WriteLine(TbCommands.Text);
                        writer.WriteLine("#");
                        writer.WriteLine(TbOut.Text);
                        writer.WriteLine("#");
                        writer.WriteLine(TbRegA.Text);
                        writer.WriteLine("#");
                        writer.WriteLine(TbRegB.Text);
                        writer.WriteLine("#");
                        writer.WriteLine(TbOther.Text);
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
                    writer.WriteLine(TbCommands.Text);
                    writer.WriteLine("#");
                    writer.WriteLine(TbOut.Text);
                    writer.WriteLine("#");
                    writer.WriteLine(TbRegA.Text);
                    writer.WriteLine("#");
                    writer.WriteLine(TbRegB.Text);
                    writer.WriteLine("#");
                    writer.WriteLine(TbOther.Text);
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
            int numLines = GetNumLines(TbCommands.Text);

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


        private void TbInput_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                SwitchFocus();
                e.Handled = true;
            }
        }








        private void SwitchFocus()
        {
            if (TbCommands.IsFocused)
            {
                TbOut.Focus();
                TbOut.CaretIndex = TbOut.Text.Length;
            }
            else if (TbOut.IsFocused)
            {
                TbRegA.Focus();
                TbRegA.CaretIndex = TbRegA.Text.Length;
            }
            else if (TbRegA.IsFocused)
            {
                TbRegB.Focus();
                TbRegB.CaretIndex = TbRegB.Text.Length;
            }
            else if (TbRegB.IsFocused)
            {
                TbOther.Focus();
                TbOther.CaretIndex = TbOther.Text.Length;
            }
            else
            {
                AddLines();
                TbCommands.Focus();
                TbCommands.CaretIndex = TbCommands.Text.Length;
                Reload();
            }
        }

        private void AddLines()
        {
            TbCommands.Text += "\n";
            TbOut.Text += "\n";
            TbRegA.Text += "\n";
            TbRegB.Text += "\n";
            TbOther.Text += "\n";
        }
    }
}
