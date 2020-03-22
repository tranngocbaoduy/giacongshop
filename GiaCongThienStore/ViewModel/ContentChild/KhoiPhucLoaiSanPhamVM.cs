using GiaCongThienStore.Database;
using GiaCongThienStore.Model;
using GiaCongThienStore.ModelCustom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GiaCongThienStore.ViewModel.ContentChild
{
    class KhoiPhucLoaiSanPhamVM : BaseViewModel
    {
        #region commands
        public ICommand LoadDanhMucSanPhamCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand KhoiPhucLSPCommand { get; set; }
        public ICommand OnKeyUpFilterText { get; set; }
        #endregion

        private String _FilterText = "";
        public String FilterText { get => _FilterText; set { _FilterText = value; OnPropertyChanged(); } }

        private String _SelectedValueFilter;
        public String SelectedValueFilter
        {
            get => _SelectedValueFilter; set
            {
                _SelectedValueFilter = value; OnPropertyChanged();
                Filter(FilterText);
            }
        }

        private ObservableCollection<LOAISANPHAM> _LoaiSanPhamList;
        public ObservableCollection<LOAISANPHAM> LoaiSanPhamList { get => _LoaiSanPhamList; set { _LoaiSanPhamList = value; OnPropertyChanged(); } }

        public string fullPath = "";

        public string _VisibleKhoiPhucButton = "Hidden";
        public string VisibleKhoiPhucButton { get => _VisibleKhoiPhucButton; set { _VisibleKhoiPhucButton = value; OnPropertyChanged(); } }

        public bool _EnableKhoiPhucButton = false;
        public bool EnableKhoiPhucButton { get => _EnableKhoiPhucButton; set { _EnableKhoiPhucButton = value; OnPropertyChanged(); } }

        private LOAISANPHAM _SelectedLoaiSanPham = new LOAISANPHAM();
        public LOAISANPHAM SelectedLoaiSanPham
        {
            get => _SelectedLoaiSanPham; set
            {
                _SelectedLoaiSanPham = value; OnPropertyChanged();
                if (SelectedLoaiSanPham != null)
                {
                    EnableKhoiPhucButton = true;
                }
                else
                {
                    EnableKhoiPhucButton = false;
                }
            }
        }

        private ObservableCollection<LOAISANPHAM> _LoaiSPList;
        public ObservableCollection<LOAISANPHAM> LoaiSPList { get => _LoaiSPList; set { _LoaiSPList = value; OnPropertyChanged(); } }

        private ObservableCollection<String> _LoaiFilter;
        public ObservableCollection<String> LoaiFilter { get => _LoaiFilter; set { _LoaiFilter = value; OnPropertyChanged(); } }

        public KhoiPhucLoaiSanPhamVM()
        {
            Init();
            CheckChucVu();
            LoadDanhMucSanPhamCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Filter("");
            });

            OnKeyUpFilterText = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                Filter(p.Text);
            });

            KhoiPhucLSPCommand = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                KhoiPhuc();
            });

            BackCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p = p as Window;
                if (p != null)
                {
                    Init();
                    p.Close();
                }
            });

        }

        public void Init()
        {
            FilterText = "";
            LoaiSanPhamList = new ObservableCollection<LOAISANPHAM>();
            LoaiFilter = new ObservableCollection<String>();
            LoaiFilter.Clear();
            _LoaiSanPhamList.Clear();
            var loaisp = DataProvider.Ins.DB.LOAISANPHAMs.Where(item => !item.ACTIVATE);
            foreach (var item in loaisp)
            {
                _LoaiSanPhamList.Add(item);
            }
            LoaiFilter.Add("Tên");
            LoaiFilter.Add("Mã");
        }

        public void Filter(String query)
        {
            _LoaiSanPhamList.Clear();

            if (query != null || SelectedValueFilter != null)
            {
                FilterText = query;
                if (query.Length == 0)
                {
                    foreach (var item in DataProvider.Ins.DB.LOAISANPHAMs)
                    {
                        LoaiSanPhamList.Add(item);
                    }
                }
                else
                {
                    switch (SelectedValueFilter)
                    {
                        case "Tên":
                            foreach (var item in DataProvider.Ins.DB.LOAISANPHAMs.Where(item => item.TENLOAI.ToUpper().Contains(query.ToUpper()) && item.ACTIVATE))
                            {

                                LoaiSanPhamList.Add(item);
                            }
                            break;

                        case "Mã":
                            foreach (var item in DataProvider.Ins.DB.LOAISANPHAMs.Where(item => item.MLSP.ToUpper().Contains(query.ToUpper()) && item.ACTIVATE))
                            {
                                LoaiSanPhamList.Add(item);
                            }
                            break;

                        default:
                            SelectedValueFilter = "Tên";
                            break;
                    }
                }
            }
            else
            {
                foreach (var item in DataProvider.Ins.DB.LOAISANPHAMs)
                {
                    LoaiSanPhamList.Add(item);
                }
            }

        }
        public void KhoiPhuc()
        {
            try
            {
                var loaisp = DataProvider.Ins.DB.LOAISANPHAMs.Where(item => item.MLSP == SelectedLoaiSanPham.MLSP).ToList()[0];
                loaisp.ACTIVATE = true;
                DataProvider.Ins.DB.SaveChanges();
                Helper.Helper.WriteLog("Khôi phục loại sản phẩm " + loaisp.MLSP + " thành công vào lúc " + DateTime.UtcNow.ToString(), "LSP");
                MessageBox.Show("Khôi phục loại sản phẩm thành công");
                Init();
            }
            catch (Exception e)
            {
                MessageBox.Show("Khôi phục không thành công", e.Message);
            }

        }

        public void CheckChucVu()
        {
            var nhanvien = DataProvider.Ins.DB.NHANVIENs.Where(item => item.TAIKHOAN == MainVM.Account.TAIKHOAN1).ToList();
            if (nhanvien.Count == 1 && nhanvien[0].CHUCVU1.TENCHUCVU == "admin")
            {
                VisibleKhoiPhucButton = "Visible";
            }
        }
    }
}
