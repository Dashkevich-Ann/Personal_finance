using BusinessLayer.Models;
using Personal_finance.ViewModel;
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
using System.Windows.Shapes;

namespace Personal_finance.Windows
{
    /// <summary>
    /// Interaction logic for CategoryWindow.xaml
    /// </summary>
    public partial class CategoryWindow : Window
    {
        public CategoryWindow(TransactionCategoryDTO transactionCategoryDTO)
        {
            InitializeComponent();
            var viewModel = new CategoryViewModel(transactionCategoryDTO);
            DataContext = viewModel;
            SetRadioButtons(transactionCategoryDTO);
            
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void CostTypeCategory_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void IncomeTypeCategory_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void SetRadioButtons(TransactionCategoryDTO transactionCategoryDTO)
        {
            if (transactionCategoryDTO?.Type == TransactionType.Income)
            {
                IncomeTypeCategory.IsChecked = true;
                return;
            }

            CostTypeCategory.IsChecked = true;
        }
    }
}
