using GiaCongThienStore.Database;
using GiaCongThienStore.Helper;
using GiaCongThienStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GiaCongThienStore.ViewModel
{
    class LoginVM : BaseViewModel
    {
        #region
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand RePasswordChangedCommand { get; set; }
        public ICommand ClickCloseLoginForm { get; set; }
        public ICommand ClickLoginButton { get; set; }
        #endregion

        public TAIKHOAN Account { get; set; }

        private String _UserName;
        public String UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }

        private String _Password;
        public String Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        private String _RePassword;
        public String RePassword { get => _RePassword; set { _RePassword = value; OnPropertyChanged(); } }
         
        private PasswordBox _PasswordBox1 { get; set; }
        public PasswordBox PasswordBox1 { get => _PasswordBox1; set { _PasswordBox1 = value; OnPropertyChanged(); } }

        private PasswordBox _PasswordBox2 { get; set; }
        public PasswordBox PasswordBox2 { get => _PasswordBox2; set { _PasswordBox2 = value; OnPropertyChanged(); } }


        public LoginVM()
        {

            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return p == null ? false : true; }, (p) => { PasswordBox1 = p as PasswordBox; Password = p.Password; });

            RePasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return p == null ? false : true; }, (p) => { PasswordBox2 = p as PasswordBox; RePassword = p.Password; });

            ClickLoginButton = new RelayCommand<Window>((p) => { return true; }, (p) => { 
                checkLogin(p as Window);
            });

            // Check IsLogin Yet
            void checkLogin(Window p)
            { 
                if (Password == null || UserName == null)
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không hợp lệ");
                    return;
                } 
                Password = EncodeDecode.Base64Encode(Password); 
                var pass = Password;
                var acc = DataProvider.Ins.DB.TAIKHOANs.Where(item => item.TAIKHOAN1 == UserName && item.MATKHAU == pass && !item.ACTIVATE ).SingleOrDefault();
                if (acc != null)
                {
                    if (Password == EncodeDecode.Base64Encode("1"))
                    { 
                        RePassword = "";
                        Password = "";  
                        PasswordBox1.Password = string.Empty;
                        return;
                    }
                    else
                    {
                        MessageBox.Show(acc.TAIKHOAN1);
                        MainVM.Account = acc;
                        MainVM.IsLogin = true;
                        p.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không hợp lệ");
                    return;
                } 
            }
        }
    }
}
