using BusinessLayer.Models;
using BusinessLayer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : UserControl
    {
        private readonly TransactionService _transactionService;

        ObservableCollection<CostStatisticItem> items = new ObservableCollection<CostStatisticItem>();
        public Statistics()
        {
            _transactionService = new TransactionService();
            InitializeComponent();
            SetInitialDates();
            FillOutStatisticsList();
        }

        private void SetInitialDates()
        {
            var now = DateTime.Now;
            var toDate = now.AddDays(1).Date;
            ToDate.SelectedDate = toDate.AddSeconds(-1);

            var fromDate = toDate.AddMonths(-1);
            FromDate.SelectedDate = fromDate;
        }

        private void FillOutStatisticsList()
        {


            var statistic = _transactionService.GetStatistics(FromDate.SelectedDate.GetValueOrDefault(), ToDate.SelectedDate.GetValueOrDefault());
            items.Clear();
            foreach (var item in statistic.CostStatistics)
            {
                items.Add(item);
            }
            StatisticsList.ItemsSource = items;
            SetTotalLable(statistic);
        }

        private void SetTotalLable(TransactionStatisticsDTO statistic)
        {
            CostSum.Text = statistic.CostSum.ToString("C", CultureInfo.CurrentCulture);
            IncomeSum.Text = statistic.IncomeSum.ToString("C", CultureInfo.CurrentCulture);
        }

        private void SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FillOutStatisticsList();
        }
    }
}
