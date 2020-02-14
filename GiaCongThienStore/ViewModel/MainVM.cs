using GiaCongThienStore.Database;
using GiaCongThienStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GiaCongThienStore.ViewModel
{
    public class MainVM : BaseViewModel
    {
        #region commands
        public ICommand ActiveWC { get; set; }
        public ICommand LoadedWC { get; set; }
        #endregion
        private static TAIKHOAN _Account;
        public static TAIKHOAN Account
        {
            get
            {
                if (_Account == null)
                    _Account = new TAIKHOAN();
                return _Account;
            }
            set
            {
                _Account = value;
            }
        }

        public static bool IsLogin {get; set;} = false ;

        private ObservableCollection<String> _ActiveContent;
        public ObservableCollection<String> ActiveContent { get => _ActiveContent; set { _ActiveContent = value; OnPropertyChanged(); } }
         
        private ObservableCollection<CALAMVIEC> _CaLamViecList;
        public ObservableCollection<CALAMVIEC> CaLamViecList { get => _CaLamViecList; set { _CaLamViecList = value; OnPropertyChanged(); } }

        public MainVM()
        {
            int holdActive = -1;
            InitalMainVM();

            //checked login
            LoadedWC = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                // Show form login when start program
                if (p != null)
                {
                    p.Hide();
                    Login loginForm = new Login();
                    loginForm.ShowDialog();
                    if (loginForm.DataContext == null)
                    {
                        return;
                    }
                     
                    if (MainVM.IsLogin)
                    { 
                        p.Show(); 
                    }
                    else
                    {
                        p.Close();
                    }

                }
            }); 

            //ActiveContent mong muốn bằng cách sữa index
            ActiveWC = new RelayCommand<String>((index) => { return true; }, (index) =>
            { 
                if (index != null && holdActive != Convert.ToInt32(index))
                { 
                    if(holdActive != -1)
                    {
                        this.ActiveContent[holdActive] = "#325976";
                        this.ActiveContent[holdActive + 1] = "0.5";
                        this.ActiveContent[holdActive + 2] = "Hidden";
                    }
                    else
                    {
                        this.ActiveContent[0] = "#325976";
                        this.ActiveContent[1] = "0.5";
                        this.ActiveContent[2] = "Hidden"; 
                    }
                    holdActive = Convert.ToInt32(index);
                    this.ActiveContent[holdActive] = "#23A3FF";
                    this.ActiveContent[holdActive + 1] = "1";
                    this.ActiveContent[holdActive + 2] = "Visible";
                }
            });
        }

        public void InitalMainVM()
        { 
            this.ActiveContent = new ObservableCollection<string>();
            this._ActiveContent.Add("#23A3FF");
            this._ActiveContent.Add("1");
            this._ActiveContent.Add("Visible");
            for (int i = 0; i < 30; i++)
            {
                this._ActiveContent.Add("#325976");
                this._ActiveContent.Add("0.5");
                this._ActiveContent.Add("Hidden");
            }

            CaLamViecList = new ObservableCollection<CALAMVIEC>();
            foreach (var item in DataProvider.Ins.DB.CALAMVIECs)
            {
                CaLamViecList.Add(item);
            }
        }
    }
}
