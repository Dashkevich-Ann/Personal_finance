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
                About,
                EditCategories,
                Statistics
            };

            var w = new TransactionWindow();
            w.ShowDialog();
        }

        private void ToggleButtonCliked(object sender, RoutedEventArgs routedEventArgs)
        {
            var button = sender as ToggleButton;

            if(button != null)
            {
                foreach(var btn in buttonList)
                {
                    if (btn.Equals(button))
                        btn.IsChecked = true;
                    else
                        btn.IsChecked = false;
                }
            }
        }

        private void ToggleButtonUnCheck(object sender, RoutedEventArgs routedEventArgs)
        {
            (sender as ToggleButton).IsChecked = true;
        }

        private void SubscribeToggleButtonOnClikc()
        {
            foreach(var button in buttonList)
            {
                button.Click += ToggleButtonCliked;
            }
        }
    }
}
