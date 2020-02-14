using GiaCongThienStore.ViewModel;
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
    /// Interaction logic for CaNhanContent.xaml
    /// </summary>
    public partial class CaNhanContent : UserControl
    {
        public CaNhanContentVM ViewModel { get; set; }
        public CaNhanContent()
        {
            InitializeComponent();
            this.DataContext = ViewModel = new CaNhanContentVM();
        }
    }
}
