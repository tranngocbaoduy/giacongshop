using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GiaCongThienStore.ViewModel.Content
{
    public class TongQuanContentVM :BaseViewModel
    {
        #region commands
        public ICommand TongQuanCommand { get; set; }
        #endregion
        public TongQuanContentVM()
        {
            TongQuanCommand = new RelayCommand<UserControl>((p) => {
                return true; }, (p) =>
            {
                var w = Window.GetWindow(p) as Window;
                if (w != null)
                {
                    w.Hide();
                    Login loginForm = new Login();
                    loginForm.ShowDialog();
                    w.Show();
                }
            });
        }
    }
}
