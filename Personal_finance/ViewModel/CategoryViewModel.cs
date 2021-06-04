using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Personal_finance.ViewModel
{
    class CategoryViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly TransactionCategoryDTO _transactionCategoryDTO;
        private bool _monthLimitDisabled;
        private ICommand _radioButtonCommand;

        public CategoryViewModel(TransactionCategoryDTO transactionCategoryDTO)
        {
            _transactionCategoryDTO = transactionCategoryDTO;
            _monthLimitDisabled = _transactionCategoryDTO.Type == TransactionType.Income;
        }

        public string Name
        {
            get
            {
                return _transactionCategoryDTO.Name;
            }
            set
            {
                _transactionCategoryDTO.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public decimal? MonthLimit
        {
            get
            {
                return _transactionCategoryDTO.MonthLimit;
            }
            set
            {
                _transactionCategoryDTO.MonthLimit = value;
                OnPropertyChanged("MonthLimit");
            }
        }

        public bool MonthLimitDisabled
        {
            get
            {
                return _transactionCategoryDTO.Type == TransactionType.Income;
            }
        }

        public ICommand RadioButtonCommand =>
            _radioButtonCommand ?? (_radioButtonCommand = new RelayCommand(x =>
            {
                var type = (TransactionType)int.Parse(x.ToString());
                _transactionCategoryDTO.Type = type;
                OnPropertyChanged("MonthLimitDisabled");

            }));

        public string this[string columnName] => throw new NotImplementedException();

        public string Error => throw new NotImplementedException();
    }
}
