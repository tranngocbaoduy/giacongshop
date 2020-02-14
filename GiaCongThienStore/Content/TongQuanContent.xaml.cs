using GiaCongThienStore.ViewModel.Content;
using System;
using System.Collections.Generic;
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

namespace GiaCongThienStore.Content
{
    /// <summary>
    /// Interaction logic for TongQuanContent.xaml
    /// </summary>
    public partial class TongQuanContent : UserControl
    {
        public TongQuanContentVM ViewModel { get; set; }
        public TongQuanContent()
        {
            InitializeComponent();
            this.DataContext = ViewModel = new TongQuanContentVM();
        }
    }
}
