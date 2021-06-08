using Personal_finance.Infrastructure;
using Personal_finance.ViewModel;
using Personal_finance.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Personal_finance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IEnumerable<ToggleButton> buttonList;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            UserBalance.IsChecked = true;
            buttonList = new List<ToggleButton>()
            {
                UserBalance,
                EditCategories,
                Statistics
            };
            Logger.Info("Application Personal Office is opened");
            SubscribeToggleButtonOnClikc();
        }

        private void ToggleButtonCliked(object sender, RoutedEventArgs routedEventArgs)
        {
            var button = sender as ToggleButton;

            if(button != null)
            {
                var btnParams = button.CommandParameter.ToString();

                foreach(var btn in buttonList)
                {
                    if (btnParams == btn.CommandParameter.ToString())
                        btn.IsChecked = true;
                    else
                        btn.IsChecked = false;
                }
            }
        }

        private void SubscribeToggleButtonOnClikc()
        {
            foreach(var button in buttonList)
            {
                button.Click += ToggleButtonCliked;
            }
        }

        private void SaveLog_Click(object sender, RoutedEventArgs e)
        {
            Logger.Show();
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Logger.Info("Application Personal Office is closed");
            this.Close();
        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            var about = new AboutWindow();
            about.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Logger.Info("Application Personal Office is closed");
        }
    }
}
