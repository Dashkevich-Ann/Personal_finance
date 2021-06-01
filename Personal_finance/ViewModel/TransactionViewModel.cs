using BusinessLayer.Models;
using BusinessLayer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Personal_finance.ViewModel
{
    public class TransactionViewModel : ViewModelBase
    {
        private readonly TransactionDTO _transaction;
        private readonly TransactionCategoryService _transactionCategoryService;
        private TransactionCategoryDTO selectedCategory;

        private ICommand _radioButtonCommand;

        public ObservableCollection<TransactionCategoryDTO> Costs { get; set; } = new ObservableCollection<TransactionCategoryDTO>();
        public ObservableCollection<TransactionCategoryDTO> Incomes { get; set; } = new ObservableCollection<TransactionCategoryDTO>();


        public TransactionViewModel(TransactionDTO transactionDTO = null)
        {
            _transaction = transactionDTO ?? new TransactionDTO() { Date = DateTime.Now };

            _transactionCategoryService = new TransactionCategoryService();
            selectedCategory = transactionDTO?.Category;
            FillCostCategoryCollection();
        }

        private void FillCostCategoryCollection()
        {
            var categories = _transactionCategoryService.GetAllCategories();

            foreach(var category in categories)
            {
                if (category.Type == TransactionType.Cost)
                    Costs.Add(category);
                else
                    Incomes.Add(category);

            }
        }

        public decimal Amount { 
            get {
                return _transaction.Amount;
            }
            set {
                _transaction.Amount = value;
                OnPropertyChanged("Amount");
            } 
        }

        public DateTime Date
        {
            get
            {
                return _transaction.Date;
            }
            set
            {
                _transaction.Date = value;
                OnPropertyChanged("Date");
            }
        }

        public string Comment
        {
            get => _transaction.Comment;
            set
            {
                _transaction.Comment = value;
                OnPropertyChanged("Comment");
            }
        }

        public TransactionCategoryDTO Category
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                selectedCategory = value;
                OnPropertyChanged("Category");
            }
        }

        public ICommand RadioButtonCommand =>
            _radioButtonCommand ?? (_radioButtonCommand = new RelayCommand(x =>
            {
                var type = (TransactionType)int.Parse(x.ToString());
                Category = null;

            }));
    }
}
