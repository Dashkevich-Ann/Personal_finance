using BusinessLayer.Models;
using BusinessLayer.Services;
using Personal_finance.Infrastructure;
using Personal_finance.ViewModel;
using Personal_finance.Windows;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            SetStatusBarMessage("User's Balance window was opened");
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
            try
            {
                var newTransaction = new TransactionDTO() { Date = DateTime.Now };

                var dialog = new TransactionWindow(newTransaction);
                dialog.ShowDialog();
                if (dialog.DialogResult == true)
                {
                    _transactionService.CreateTransaction(newTransaction);
                    SetStatusBarMessage($"New {newTransaction.Category.Type}.{newTransaction.Category.Name} transaction was created");
                    FillOutTransactionList();
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex.Message);
                MessageBox.Show("Transaction creation error. Please, contact with help desk", "Error", MessageBoxButton.OK);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                var selected = UserBalanceDateBinding.SelectedItem as TransactionDTO;

                if(selected != null)
                {
                    _transactionService.DeleteTransaction(selected);
                    SetStatusBarMessage($"{selected.Category.Type}.{selected.Category.Name} transaction was deleted");
                    FillOutTransactionList();
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex.Message);
                MessageBox.Show("Transaction remove error. Please, contact with help desk", "Error", MessageBoxButton.OK);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = UserBalanceDateBinding.SelectedItem as TransactionDTO;

                if (selected != null)
                {
                    var dialog = new TransactionWindow(selected);
                    dialog.ShowDialog();

                    if (dialog.DialogResult == true)
                    {
                        _transactionService.EditTransaction(selected);

                        SetStatusBarMessage($"{selected.Category.Type}.{selected.Category.Name} transaction was changed");
                        FillOutTransactionList();
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex.Message);
                MessageBox.Show("Transaction edition error. Please, contact with help desk", "Error", MessageBoxButton.OK);
            }

        }

        private void SetStatusBarMessage(string message)
        {
            var time = DateTime.Now.ToLongTimeString();
            st.Text = $"{time}: {message}";
        }
    }
}
