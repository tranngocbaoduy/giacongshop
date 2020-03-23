using GiaCongThienStore.Database;
using GiaCongThienStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GiaCongThienStore.ViewModel.ContentChild
{
    public class KhoiPhucNhaCungCapVM : BaseViewModel
    {
        #region commands
        public ICommand LoadDanhMucSanPhamCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand KhoiPhucNCCCommand { get; set; }
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

        private ObservableCollection<NHACUNGCAP> _NhaCungCapList;
        public ObservableCollection<NHACUNGCAP> NhaCungCapList { get => _NhaCungCapList; set { _NhaCungCapList = value; OnPropertyChanged(); } }

        public string fullPath = "";

        public string _VisibleKhoiPhucButton = "Hidden";
        public string VisibleKhoiPhucButton { get => _VisibleKhoiPhucButton; set { _VisibleKhoiPhucButton = value; OnPropertyChanged(); } }

        public bool _EnableKhoiPhucButton = false;
        public bool EnableKhoiPhucButton { get => _EnableKhoiPhucButton; set { _EnableKhoiPhucButton = value; OnPropertyChanged(); } }

        private NHACUNGCAP _SelectedNhaCungCap = new NHACUNGCAP();
        public NHACUNGCAP SelectedNhaCungCap
        {
            get => _SelectedNhaCungCap; set
            {
                _SelectedNhaCungCap = value; OnPropertyChanged();
                if (SelectedNhaCungCap != null)
                {
                    EnableKhoiPhucButton = true;
                }
                else
                {
                    EnableKhoiPhucButton = false;
                }
            }
        }
         
        private ObservableCollection<String> _LoaiFilter;
        public ObservableCollection<String> LoaiFilter { get => _LoaiFilter; set { _LoaiFilter = value; OnPropertyChanged(); } }

        public KhoiPhucNhaCungCapVM()
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

            KhoiPhucNCCCommand = new RelayCommand<TextBox>((p) =>
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
                    p.Close();
                    Init();
                    CheckChucVu();
                }
            });

        }

        public void Init()
        {
            FilterText = "";
            NhaCungCapList = new ObservableCollection<NHACUNGCAP>();
            LoaiFilter = new ObservableCollection<String>();
            LoaiFilter.Clear();
            NhaCungCapList.Clear();
            var loaisp = DataProvider.Ins.DB.NHACUNGCAPs.Where(item => !item.ACTIVATE);
            foreach (var item in loaisp)
            {
                NhaCungCapList.Add(item);
            }
            LoaiFilter.Add("Tên");
            LoaiFilter.Add("Mã");
        }

        public void Filter(String query)
        {
            NhaCungCapList.Clear();

            if (query != null || SelectedValueFilter != null)
            {
                FilterText = query;
                if (query.Length == 0)
                {
                    var items = DataProvider.Ins.DB.NHACUNGCAPs.Where(item => !item.ACTIVATE).ToList();
                    foreach (var item in items)
                    {
                        NhaCungCapList.Add(item);
                    }
                }
                else
                {
                    switch (SelectedValueFilter)
                    {
                        case "Tên":
                            foreach (var item in DataProvider.Ins.DB.NHACUNGCAPs.Where(item => item.TENNHACUNGCAP.ToUpper().Contains(query.ToUpper()) && !item.ACTIVATE))
                            {

                                NhaCungCapList.Add(item);
                            }
                            break;

                        case "Mã":
                            foreach (var item in DataProvider.Ins.DB.NHACUNGCAPs.Where(item => item.MNCC.ToUpper().Contains(query.ToUpper()) && !item.ACTIVATE))
                            {
                                NhaCungCapList.Add(item);
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
                var items = DataProvider.Ins.DB.NHACUNGCAPs.Where(item => !item.ACTIVATE).ToList();
                foreach (var item in items)
                {
                    NhaCungCapList.Add(item);
                }
            }

        }
        public void KhoiPhuc()
        {
            try
            {
                if (MessageBox.Show("Xác nhận khôi phục lại ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
                {
                    //do no stuff
                }
                else
                {
                    var ncc = DataProvider.Ins.DB.NHACUNGCAPs.Where(item => item.MNCC == SelectedNhaCungCap.MNCC).ToList()[0];
                    ncc.ACTIVATE = true;
                    DataProvider.Ins.DB.SaveChanges();
                    Helper.Helper.WriteLog("Khôi phục nhà cung cấp " + ncc.MNCC + " thành công vào lúc " + DateTime.UtcNow.ToString() + " bởi " + MainVM.Account.TAIKHOAN1, "NCC");
                    MessageBox.Show("Khôi phục nhà cung cấp thành công");
                    Init();
                }
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
