using System;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MI_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            MIWindows.Background = Brushes.LightGray;
        }

        private void MI_MouseLeave(object sender, MouseEventArgs e)
        {
            MIWindows.Background = Brushes.White;
        }

        private void MI_MouseEnter(object sender, MouseEventArgs e)
        {
            MIWindows.Background = Brushes.LightGray;
        }

        private void TBInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                TBInput.IsEnabled = false;
                //TODO: Create Save window
                Save savePage = new();

                savePage.Owner = this;
                //savePage.Show();
                TBInput.IsEnabled = true;
            }
        }
    }
}
