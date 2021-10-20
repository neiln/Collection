using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CtrlManager.Views
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void WindowButton_OnClick(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button)?.Name)
            {
                case "MinButton":
                    this.WindowState = WindowState.Minimized;
                    break;

                case "MaxButton":
                    this.WindowState = (this.WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
                    break;

                case "CloseButton":
                    this.Close();
                    Application.Current.Shutdown();
                    break;
            }
        }

        private void Toggle_Click(object sender, RoutedEventArgs e)
        {
            Dashboard.IsChecked = Config.IsChecked = false;
            ((ToggleButton)sender).IsChecked = true;
        }
    }
}
