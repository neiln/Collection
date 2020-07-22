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

namespace WpfChromelessApp
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

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button)?.Name)
            {
                case "MinButton":
                    this.WindowState = WindowState.Minimized;
                    break;

                case "MaxButton":
                    this.WindowState = (this.WindowState==WindowState.Maximized)?WindowState.Normal:WindowState.Maximized;
                    break;

                case "CloseButton":
                    this.Close();
                    break;
            }
        }
    }
}
