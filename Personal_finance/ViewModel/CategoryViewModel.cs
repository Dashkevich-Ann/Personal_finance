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
        private bool _isEditFlow;
        private ICommand _radioButtonCommand;
        private ICommand _okBtnCommand;

        public CategoryViewModel(TransactionCategoryDTO transactionCategoryDTO, bool isEditFlow = false)
        {
            _transactionCategoryDTO = transactionCategoryDTO;
            _monthLimitDisabled = _transactionCategoryDTO.Type == TransactionType.Income;
            _isEditFlow = isEditFlow;
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

            }, param => !_isEditFlow));

        public ICommand OkBtnCommand =>
            _okBtnCommand ?? (_okBtnCommand = new RelayCommand(x => { }, TransactionIsValid));

        private bool TransactionIsValid(object obj) =>
            string.IsNullOrEmpty(this[nameof(Name)])
            && string.IsNullOrEmpty(this[nameof(MonthLimit)]);


        public string this[string columnName] 
        {
            get
            {
                string result = string.Empty;
                switch(columnName)
                {
                    case nameof(Name):
                        {
                            if (this.Name == null) result = "Please, fill in field.";
                        } break;
                    default: break;
                }
                return result;        
            }
        }

        public string Error => null;
    }
}
