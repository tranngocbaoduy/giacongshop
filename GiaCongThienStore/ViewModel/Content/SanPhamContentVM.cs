using GiaCongThienStore.ContentChild;
using GiaCongThienStore.ViewModel.ContentChild;
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
    public class SanPhamContentVM : BaseViewModel
    {
        #region commands
        public ICommand DanhMucSanPhamCommand { get; set; }
        public ICommand LoaiSanPhamCommand { get; set; }
        public ICommand NhaCungCapCommand { get; set; }
        public ICommand ThemSanPhamCommand { get; set; }
        #endregion

        public SanPhamContentVM()
        {
            DanhMucSanPhamCommand = new RelayCommand<UserControl>((p) => {
                return true;
            }, (p) =>
            {
                var w = Window.GetWindow(p) as Window;
                if (w != null)
                {
                    w.Hide();
                    DanhMucSanPham DanhMucForm = new DanhMucSanPham();
                    DanhMucForm.ShowDialog();
                    w.Show();
                }
            });

            LoaiSanPhamCommand = new RelayCommand<UserControl>((p) => {
                return true;
            }, (p) =>
            {
                var w = Window.GetWindow(p) as Window;
                if (w != null)
                {
                    w.Hide();
                    LoaiSanPham LoaiSPForm = new LoaiSanPham();
                    LoaiSPForm.ShowDialog();
                    w.Show();
                }
            });
            
            NhaCungCapCommand = new RelayCommand<UserControl>((p) => {
                return true;
            }, (p) =>
            {
                var w = Window.GetWindow(p) as Window;
                if (w != null)
                {
                    w.Hide();
                    ThemSanPham DanhMucForm = new ThemSanPham();
                    DanhMucForm.ShowDialog();
                    w.Show();
                }
            });
            
            ThemSanPhamCommand = new RelayCommand<UserControl>((p) => {
                return true;
            }, (p) =>
            {
                var w = Window.GetWindow(p) as Window;
                if (w != null)
                {
                    w.Hide();
                    ThemSanPham DanhMucForm = new ThemSanPham();
                    ThemSanPhamVM.idUpdate = "";
                    ThemSanPhamVM.isUpdate = false;

                    var sanPhamVM = DanhMucForm.DataContext as ThemSanPhamVM;
                    sanPhamVM.Init();
                    DanhMucForm.ShowDialog();
                    w.Show();
                }
            });
        }

    }
}
