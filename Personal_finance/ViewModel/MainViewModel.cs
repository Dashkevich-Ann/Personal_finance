using Personal_finance.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace Personal_finance.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ICommand _gotoView;
        private UserControl _currentView;
        private IEnumerable<UserControl> _views;

        public MainViewModel()
        {
            _views = new List<UserControl> { new UserBalance(), new About() };

            CurrentView = _views.First();
        }

        public object GotoViewCommand
        {
            get
            {
                return _gotoView ?? (_gotoView = new RelayCommand(
                   x =>
                   {
                       GoToView(x.ToString());
                   }));
            }
        }

        public UserControl CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged("CurrentView");
            }
        }

        private void GoToView(string viewName)
        {
            var view = _views.FirstOrDefault(x => x.GetType().Name == viewName);

            if(view != null)
                CurrentView = view;
        }

    }
    public class ButtonModal 
    { 
        public bool IsChecked { get; set; }

        public Type ControlType { get; set; }
    }
}
