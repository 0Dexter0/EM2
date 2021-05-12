﻿using System;
using System.Collections.Generic;
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

namespace EM2.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int EnterPressed { get; set; } = 1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MI_MouseLeave(object sender, MouseEventArgs e)
        {
            SolidColorBrush solidColor = new();
            solidColor.Color = new Color() { R = 20, G = 20, B = 20 };
            MIWindows.Background = solidColor;
            MIRegisters.Background = solidColor;
        }

        private void MI_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush solidColor = new();
            solidColor.Color = new Color() { R = 20, G = 20, B = 20 };
            MIWindows.Background = solidColor;
            MIRegisters.Background = solidColor;
            MIRegisters.Foreground = Brushes.Black;
        }

        private void TBInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                TBInput.IsEnabled = false;

                TimerCallback callback = new(ClearTBInfo);
                Timer timer = new(callback, null, 2000, 0);

                TBInfo.Text = "Saved";
               
                TBInput.IsEnabled = true;
            }
            if (e.Key == Key.Return)
            {
                

                EnterPressed++;
                TBInfo.Text = EnterPressed.ToString();
            }
        }

        private void ClearTBInfo(object o)
        {
            Dispatcher.Invoke(() => TBInfo.Text = "");
        }

        private void TBInput_MouseEnter(object sender, MouseEventArgs e)
        {
            TBInput.BorderBrush = Brushes.Black;
        }

        private void TBInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EnterPressed++;

                SPIndex.Children.Add(new TextBlock()
                {
                    Text = EnterPressed.ToString(),
                    Padding = new Thickness(8, 0, 0, 0),
                    Foreground = Brushes.White,
                    FontSize = 15
                });
            }
        }
    }
}
