using BusinessLayer.Models;
using BusinessLayer.Services;
using Personal_finance.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Personal_finance.Views
{
    /// <summary>
    /// Interaction logic for UserBalance.xaml
    /// </summary>
    public partial class UserBalance : UserControl
    {
        private readonly TransactionService _transactionService = new TransactionService();
        ObservableCollection<TransactionDTO> items = new ObservableCollection<TransactionDTO>();
        public UserBalance()
        {
            InitializeComponent();
            FillOutTransactionList();
        }

        private void FillOutTransactionList()
        {
            var transactions = _transactionService.GetAllTransactions();
            items.Clear();
            foreach (var item in transactions)
            {
                items.Add(item);
            }
            UserBalanceDateBinding.ItemsSource = items;
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = UserBalanceDateBinding.SelectedItem as TransactionDTO;

            if(selected != null)
            {
                _transactionService.DeleteTransaction(selected);
                FillOutTransactionList();
            }
        }
    }
}
