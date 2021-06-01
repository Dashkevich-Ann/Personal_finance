using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_finance.ViewModel
{
    public class UserBalanceViewModel : ViewModelBase
    {
        private string text = "I am User Balance";

        public string Text { 
            get => text; 
            set {
                text = value;
                OnPropertyChanged("Text");
            } 
        }
    }
}
