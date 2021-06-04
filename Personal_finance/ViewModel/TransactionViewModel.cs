using BusinessLayer.Models;
using BusinessLayer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Personal_finance.ViewModel
{
    public class TransactionViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly DateTime minDate = new DateTime(1900, 1, 1);
        private readonly TransactionDTO _transaction;
        private readonly TransactionCategoryService _transactionCategoryService;
        private int? selectedCategory;

        private ICommand _radioButtonCommand;
        private ICommand _okBtnCommand;

        private TransactionType transactionType;

        public ObservableCollection<TransactionCategoryDTO> Costs { get; set; } = new ObservableCollection<TransactionCategoryDTO>();
        public ObservableCollection<TransactionCategoryDTO> Incomes { get; set; } = new ObservableCollection<TransactionCategoryDTO>();


        public TransactionViewModel(TransactionDTO transactionDTO = null)
        {
            _transaction = transactionDTO ?? new TransactionDTO() { Date = DateTime.Now };

            _transactionCategoryService = new TransactionCategoryService();
            Category = _transaction?.Category?.Id;
            transactionType = _transaction.Category?.Type ?? TransactionType.Cost;
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

        public int? Category
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

        public void UpsertTransaction()
        {
            _transaction.Category = transactionType == TransactionType.Cost
                ? Costs.First(x => x.Id == Category)
                : Incomes.First(x => x.Id == Category);
        }

        public ICommand RadioButtonCommand =>
            _radioButtonCommand ?? (_radioButtonCommand = new RelayCommand(x =>
            {
                var type = (TransactionType)int.Parse(x.ToString());
                transactionType = type;
                Category = null;
            }));

        public ICommand OkBtnCommand =>
            _okBtnCommand ?? (_okBtnCommand = new RelayCommand(x => {}, TransactionIsValid));

        private bool TransactionIsValid(object obj) => 
            Date > minDate 
            && string.IsNullOrEmpty(this[nameof(Category)])
            && string.IsNullOrEmpty(this[nameof(Amount)]);
        

        public string Error => null;

        public string this[string columnName] 
        {
            get
            {
                string result = string.Empty;
                switch (columnName)
                {
                    case nameof(Amount):
                        {
                            if (this.Amount <= 0)
                            {
                                result = "Amount must be more then 0.";
                            }
                        }
                        break;
                    case nameof(Category):
                        {
                            if (Category == null)
                            {
                                result = "Please, choose category.";
                            }
                        }
                        break;
                    
                    default:
                        break;
                }
                return result;
            }
        }
    }
}
