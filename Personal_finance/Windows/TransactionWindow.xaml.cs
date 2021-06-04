using BusinessLayer.Models;
using BusinessLayer.Services;
using Personal_finance.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Personal_finance.Windows
{
    /// <summary>
    /// Interaction logic for TransactionWindow.xaml
    /// </summary>
    public partial class TransactionWindow : Window
    {
        TransactionViewModel model;

        public TransactionWindow(TransactionDTO transactionDTO = null)
        {
            InitializeComponent();
            model = new TransactionViewModel(transactionDTO);
            this.DataContext = model;
            SetRadioButtons(transactionDTO);
        }

        private void SetRadioButtons(TransactionDTO transactionDTO)
        {
            if(transactionDTO?.Category?.Type == TransactionType.Income)
            {
                IncomeTypeCategory.IsChecked = true;
                return;
            }

            CostTypeCategory.IsChecked = true;
        }

        private void CategoryType_Checked(object sender, RoutedEventArgs e)
        {
            if((sender as RadioButton) == CostTypeCategory && IncomeComboBox != null)
            {
                IncomeComboBox.Visibility = Visibility.Collapsed;
                CostComboBox.Visibility = Visibility.Visible;
            }

            if((sender as RadioButton) == IncomeTypeCategory && CostComboBox != null)
            {
                CostComboBox.Visibility = Visibility.Collapsed;
                IncomeComboBox.Visibility = Visibility.Visible;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            model.UpsertTransaction();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
