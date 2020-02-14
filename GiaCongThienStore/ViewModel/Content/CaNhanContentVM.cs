using GiaCongThienStore.Database;
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
    public class CaNhanContentVM : BaseViewModel
    {
        #region commands 
        public ICommand LoadedAccountWindowCommand { get; set; }
        #endregion
        private NHANVIEN _NhanVien;
        public NHANVIEN NhanVien { get =>_NhanVien; set {_NhanVien = value;  OnPropertyChanged(); } }

        private String _UserName;
        public String UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }

        public CaNhanContentVM()
        {
            // Loaded Account
            LoadedAccountWindowCommand = new RelayCommand<UserControl>((p) => {
                if (!MainVM.IsLogin) { return false; } 
                return true; }, (p) =>
            {
                if (p != null)
                { 
                    NhanVien = DataProvider.Ins.DB.NHANVIENs.Where(item => String.Compare(item.TAIKHOAN, MainVM.Account.TAIKHOAN1, true)  == 0).FirstOrDefault();
                    if(NhanVien != null)
                    {
                        MessageBox.Show(NhanVien.TENNV);
                        UserName = NhanVien.TENNV;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin vui lòng tắt mở lại chương trình");
                    }
                }
            });

        }

    }
}
