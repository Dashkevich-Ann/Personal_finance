using BusinessLayer.Models;
using BusinessLayer.Services;
using Personal_finance.ViewModel;
using Personal_finance.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
            SetTotalLable();
        }

        private void SetTotalLable()
        {
            var total = items.Where(x => x.Category.Type == TransactionType.Income).Sum(x => x.Amount)
                - items.Where(x => x.Category.Type == TransactionType.Cost).Sum(x => x.Amount);

            TotalTextBlock.Text = total.ToString("C", CultureInfo.CurrentCulture);
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            var newTransaction = new TransactionDTO() { Date = DateTime.Now};

            var dialog = new TransactionWindow(newTransaction);
            dialog.ShowDialog();
            if(dialog.DialogResult == true)
            {
                _transactionService.CreateTransaction(newTransaction);
                FillOutTransactionList();
            }
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

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = UserBalanceDateBinding.SelectedItem as TransactionDTO;

            if(selected != null)
            {
                var dialog = new TransactionWindow(selected);
                dialog.ShowDialog();

                if (dialog.DialogResult == true)
                {
                    _transactionService.EditTransaction(selected);
                    FillOutTransactionList();
                }
            }
        }
    }
}
