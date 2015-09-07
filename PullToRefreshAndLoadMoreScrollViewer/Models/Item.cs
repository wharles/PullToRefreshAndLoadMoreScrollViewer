using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace PullToRefreshAndLoadMoreScrollViewer.Models
{
    public class Item
    {
        public Brush BackgroundColor { get; set; }
        public string Text { get; set; }
        public string Number { get; set; }
    }
}
