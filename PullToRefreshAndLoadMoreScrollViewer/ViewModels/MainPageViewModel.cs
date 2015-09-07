using PullToRefreshAndLoadMoreScrollViewer.Commands;
using PullToRefreshAndLoadMoreScrollViewer.Commons;
using PullToRefreshAndLoadMoreScrollViewer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace PullToRefreshAndLoadMoreScrollViewer.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private int _pageIndex;
        public MainPageViewModel()
        {
            LoadMoreItems();
            this.PullToRefreshCommand = new RelayCommand(() =>
              {
                  Items.Clear();
                  _pageIndex = 0;
                  LoadMoreItems();
              });
            this.MoreDataCommand = new RelayCommand(() =>
              {
                  LoadMoreItems();
              });
        }

        private ObservableCollection<Item> items = new ObservableCollection<Item>();

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (PropertyChanged != null)
            {
                var expression = propertyExpression.Body as MemberExpression;
                PropertyChanged(this, new PropertyChangedEventArgs(expression.Member.Name));
            }
        }

        public ObservableCollection<Item> Items
        {
            get { return items; }
            set
            {
                items = value;
                RaisePropertyChanged(() => Items);
            }
        }

        private void LoadMoreItems()
        {
            int count = 0;
            Item _item = null;
            while (count < 10)
            {
                _item = new Item();
                _item.Text = (count + _pageIndex * 10).ToString();
                _item.Number = new Number2English().NumberToString(count + _pageIndex * 10);
                int[] array = RandomHelper.GetRandomArray(3, 0, 255);
                _item.BackgroundColor = new SolidColorBrush(Color.FromArgb(255, (byte)array[0], (byte)array[1], (byte)array[2]));
                Items.Add(_item);
                count++;
            }
            _pageIndex++;
        }

        public RelayCommand MoreDataCommand { get; set; }
        public RelayCommand PullToRefreshCommand { get; set; }
    }
}
