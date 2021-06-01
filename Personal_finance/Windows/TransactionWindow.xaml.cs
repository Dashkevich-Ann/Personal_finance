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
        private readonly TransactionCategoryService transactionCategoryService;
        private readonly IEnumerable<TransactionCategoryDTO> transactionCategories;

        TransactionViewModel model;

        public TransactionWindow()
        {
            InitializeComponent();
            model = new TransactionViewModel();
            this.DataContext = model;
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
    }
}
