using GiaCongThienStore.Database;
using GiaCongThienStore.Model;
using GiaCongThienStore.ModelCustom;
using GiaCongThienStore.ContentChild;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;

namespace GiaCongThienStore.ViewModel.ContentChild
{
    public class NhaCungCapVM : BaseViewModel
    {
        #region commands
        public ICommand LoadDanhMucSanPhamCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand CapNhatNhaCungCapCommand { get; set; }
        public ICommand XoaNhaCungCapCommand { get; set; }
        public ICommand OnKeyUpFilterTextCommand { get; set; }
        public ICommand OnKeyUpFilterTextSPCommand { get; set; }
        public ICommand KhoiPhucNhaCungCapCommand { get; set; }
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

        private String _FilterTextSP = "";
        public String FilterTextSP { get => _FilterTextSP; set { _FilterTextSP = value; OnPropertyChanged(); } }

        private String _SelectedValueFilterSP;
        public String SelectedValueFilterSP
        {
            get => _SelectedValueFilterSP; set
            {
                _SelectedValueFilterSP = value; OnPropertyChanged();
                FilterSP(FilterTextSP);
            }
        }

        private ObservableCollection<NHACUNGCAP> _NhaCungCapList;
        public ObservableCollection<NHACUNGCAP> NhaCungCapList { get => _NhaCungCapList; set { _NhaCungCapList = value; OnPropertyChanged(); } }

        private ObservableCollection<SANPHAM> _SanPhamList;
        public ObservableCollection<SANPHAM> SanPhamList { get => _SanPhamList; set { _SanPhamList = value; OnPropertyChanged(); } }

        private ObservableCollection<LOGHISTORY> _LogHistory;
        public ObservableCollection<LOGHISTORY> LogHistory { get => _LogHistory; set { _LogHistory = value; OnPropertyChanged(); } }

        public string fullPath = "";

        public string _VisibleCapNhatButton = "Hidden";
        public string VisibleCapNhatButton { get => _VisibleCapNhatButton; set { _VisibleCapNhatButton = value; OnPropertyChanged(); } }

        public string _VisibleXoaButton = "Hidden";
        public string VisibleXoaButton { get => _VisibleXoaButton; set { _VisibleXoaButton = value; OnPropertyChanged(); } }

        public bool _EnableCapNhatButton = false;
        public bool EnableCapNhatButton { get => _EnableCapNhatButton; set { _EnableCapNhatButton = value; OnPropertyChanged(); } }

        public bool _EnableXoaButton = false;
        public bool EnableXoaButton { get => _EnableXoaButton; set { _EnableXoaButton = value; OnPropertyChanged(); } }

        public bool _EnableUpdateText = false;
        public bool EnableUpdateText { get => _EnableUpdateText; set { _EnableUpdateText = value; OnPropertyChanged(); } }

        private NHACUNGCAP _SelectedNhaCungCap = new NHACUNGCAP();
        public NHACUNGCAP SelectedNhaCungCap
        {
            get => _SelectedNhaCungCap; set
            {
                _SelectedNhaCungCap = value; OnPropertyChanged();
                if (_SelectedNhaCungCap != null)
                {
                    SanPhamList.Clear();
                    var items = DataProvider.Ins.DB.SANPHAMs.Where(item => item.MNCC == SelectedNhaCungCap.MNCC && item.ACTIVATE).ToList();
                    foreach (var item in items)
                    {
                        SanPhamList.Add(item);
                    }
                    EnableXoaButton = true;
                    EnableCapNhatButton = true;
                }
                else
                {
                    EnableXoaButton = false;
                    EnableCapNhatButton = false;
                }
            }
        }

        private ObservableCollection<String> _LoaiFilter;
        public ObservableCollection<String> LoaiFilter { get => _LoaiFilter; set { _LoaiFilter = value; OnPropertyChanged(); } }

        private ObservableCollection<String> _LoaiFilterSP;
        public ObservableCollection<String> LoaiFilterSP { get => _LoaiFilterSP; set { _LoaiFilterSP = value; OnPropertyChanged(); } }

        public NhaCungCapVM()
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

            OnKeyUpFilterTextCommand = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                Filter(p.Text);
            });


            OnKeyUpFilterTextSPCommand = new RelayCommand<TextBox>((p) =>
           {
               return true;
           }, (p) =>
           {
               FilterSP(p.Text);
           });

            CapNhatNhaCungCapCommand = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                UpdateLoaiSanPham();
            });

            XoaNhaCungCapCommand = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                DeleteLoaiSanPham();
            });


            KhoiPhucNhaCungCapCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (p != null)
                {
                    KhoiPhucNhaCungCap KhoiPhucForm = new KhoiPhucNhaCungCap();
                    KhoiPhucForm.ShowDialog();
                    Init();
                }
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
            SanPhamList = new ObservableCollection<SANPHAM>();
            NhaCungCapList = new ObservableCollection<NHACUNGCAP>();
            LoaiFilter = new ObservableCollection<String>();
            LoaiFilterSP = new ObservableCollection<String>();
            LogHistory = new ObservableCollection<LOGHISTORY>();
            LoaiFilter.Clear();
            LoaiFilterSP.Clear();
            NhaCungCapList.Clear();
            var loaisp = DataProvider.Ins.DB.NHACUNGCAPs.Where(item => item.ACTIVATE);
            foreach (var item in loaisp)
            {
                NhaCungCapList.Add(item);
            }

            var listLog = DataProvider.Ins.DB.LOGHISTORies.Where(i => i.CODE == "NCC" ||  i.CODE == "SP").ToList();
            listLog.Reverse();
            foreach (var item in listLog)
            {
                LogHistory.Add(item);
            }

            LoaiFilter.Add("Tên");
            LoaiFilter.Add("Mã");
            LoaiFilterSP.Add("Tên");
            LoaiFilterSP.Add("Mã");
        }

        public void Filter(String query)
        {
            NhaCungCapList.Clear();

            if (query != null || SelectedValueFilter != null)
            {
                FilterText = query;
                if (query.Length == 0)
                {
                    var items = DataProvider.Ins.DB.NHACUNGCAPs.Where(item => item.ACTIVATE).ToList();
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
                            foreach (var item in DataProvider.Ins.DB.NHACUNGCAPs.Where(item => item.TENNHACUNGCAP.ToUpper().Contains(query.ToUpper()) && item.ACTIVATE))
                            {
                                NhaCungCapList.Add(item);
                            }
                            break;

                        case "Mã":
                            foreach (var item in DataProvider.Ins.DB.NHACUNGCAPs.Where(item => item.MNCC.ToUpper().Contains(query.ToUpper()) && item.ACTIVATE))
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
                var items = DataProvider.Ins.DB.NHACUNGCAPs.Where(item => item.ACTIVATE).ToList();
                foreach (var item in items)
                {
                    NhaCungCapList.Add(item);
                }
            }
        }

        public void FilterSP(String query)
        {
            SanPhamList.Clear();

            if (query != null || SelectedValueFilterSP != null)
            {
                FilterTextSP = query;
                if (query.Length == 0)
                {
                    var items = DataProvider.Ins.DB.SANPHAMs.Where(item => item.MNCC == SelectedNhaCungCap.MNCC && item.ACTIVATE).ToList();
                    foreach (var item in items)
                    {
                        SanPhamList.Add(item);
                    }
                }
                else
                {
                    switch (SelectedValueFilterSP)
                    {
                        case "Tên":
                            foreach (var item in DataProvider.Ins.DB.SANPHAMs.Where(item => item.MNCC == SelectedNhaCungCap.MNCC && item.TENSANPHAM.ToUpper().Contains(query.ToUpper()) && item.ACTIVATE))
                            {
                                SanPhamList.Add(item);
                            }
                            break;

                        case "Mã":
                            foreach (var item in DataProvider.Ins.DB.SANPHAMs.Where(item => item.MNCC == SelectedNhaCungCap.MNCC && item.MSP.ToUpper().Contains(query.ToUpper()) && item.ACTIVATE))
                            {
                                SanPhamList.Add(item);
                            }
                            break;

                        default:
                            SelectedValueFilterSP = "Tên";
                            break;
                    }
                }
            }
            else
            {
                var items = DataProvider.Ins.DB.SANPHAMs.Where(item => item.MNCC == SelectedNhaCungCap.MNCC && item.ACTIVATE).ToList();
                foreach (var item in items)
                {
                    SanPhamList.Add(item);
                }
            }
        }


        public void UpdateLoaiSanPham()
        {
            try
            {
                if (MessageBox.Show("Xác nhận cập nhật ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
                {
                    //do no stuff
                }
                else
                {
                    var ncc = DataProvider.Ins.DB.NHACUNGCAPs.Where(item => item.MNCC == SelectedNhaCungCap.MNCC).ToList()[0];
                    ncc.TENNHACUNGCAP = SelectedNhaCungCap.TENNHACUNGCAP;
                    ncc.DIACHI = SelectedNhaCungCap.DIACHI;
                    ncc.GHICHU = SelectedNhaCungCap.GHICHU;
                    ncc.SDT = SelectedNhaCungCap.SDT;
                    DataProvider.Ins.DB.SaveChanges();
                    Helper.Helper.WriteLog("Cập nhật nhà cung cấp " + ncc.MNCC + " thành công vào lúc " + DateTime.UtcNow.ToString() + " bởi " + MainVM.Account.TAIKHOAN1, "NCC");
                    MessageBox.Show("Cập nhật nhà cung cấp thành công");
                    Init();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Cập nhật không thành công", e.Message);
            }
        }

        public void DeleteLoaiSanPham()
        {
            try
            {
                if (MessageBox.Show("Xác nhận xóa, điều này sẽ ảnh hưởng tới sản phẩm ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
                {
                    //do no stuff
                }
                else
                {
                    var ncc = DataProvider.Ins.DB.NHACUNGCAPs.Where(item => item.MNCC == SelectedNhaCungCap.MNCC).ToList()[0];
                    ncc.ACTIVATE = false;
                    DataProvider.Ins.DB.SaveChanges();
                    Helper.Helper.WriteLog("Xóa nhà cung cấp " + ncc.MNCC + " thành công vào lúc " + DateTime.UtcNow.ToString() + " bởi " + MainVM.Account.TAIKHOAN1, "NCC");
                    MessageBox.Show("Xóa nhà cung cấp thành công");
                    Init();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Xóa không thành công", e.Message);
            }
        }

        public void CheckChucVu()
        {
            var nhanvien = DataProvider.Ins.DB.NHANVIENs.Where(item => item.TAIKHOAN == MainVM.Account.TAIKHOAN1).ToList();
            if (nhanvien.Count == 1 && nhanvien[0].CHUCVU1.TENCHUCVU == "admin")
            {
                EnableUpdateText = true;
                VisibleCapNhatButton = "Visible";
                VisibleXoaButton = "Visible";
            }
        }
    }
}
