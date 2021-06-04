using BusinessLayer.Models;
using BusinessLayer.Services;
using Personal_finance.Windows;
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
    /// Interaction logic for EditCategory.xaml
    /// </summary>
    public partial class EditCategory : UserControl
    {
        private readonly TransactionCategoryService _categoryService = new TransactionCategoryService();
        ObservableCollection<TransactionCategoryDTO> items = new ObservableCollection<TransactionCategoryDTO>();

        public EditCategory()
        {
            InitializeComponent();
            FillOutCategoryList();
        }

        private void FillOutCategoryList()
        {
            var categories = _categoryService.GetAllCategories();
            items.Clear();
            foreach (var item in categories)
            {
                items.Add(item);
            }
            CategoryList.ItemsSource = items;
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CategoryWindow(new TransactionCategoryDTO());
            dialog.ShowDialog();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = CategoryList.SelectedItem as TransactionCategoryDTO;

            if(selected != null)
            {
                if (_categoryService.CategoryInUse(selected))
                {
                    MessageBox.Show($"Category {selected.Name} is used. You can't delete it.", "Error", MessageBoxButton.OK);
                    return;
                }

                _categoryService.DeleteCategory(selected);
                FillOutCategoryList();
            }
        }
    }
}
