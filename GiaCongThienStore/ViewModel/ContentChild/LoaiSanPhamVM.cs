using GiaCongThienStore.Database;
using GiaCongThienStore.Model;
using GiaCongThienStore.ModelCustom;
using GiaCongThienStore.ContentChild;
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
    class LoaiSanPhamVM : BaseViewModel
    {
        #region commands
        public ICommand LoadDanhMucSanPhamCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand CapNhatLoaiSanPham { get; set; }
        public ICommand XoaLoaiSanPham { get; set; }
        public ICommand OnKeyUpFilterText { get; set; }
        public ICommand KhoiPhucLoaiSanPhamCommand { get; set; }
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
        private ObservableCollection<ImageCustom> _MyImages;
        public ObservableCollection<ImageCustom> MyImages { get => _MyImages; set { _MyImages = value; OnPropertyChanged(); } }

        private ObservableCollection<LOAISANPHAM> _LoaiSanPhamList;
        public ObservableCollection<LOAISANPHAM> LoaiSanPhamList { get => _LoaiSanPhamList; set { _LoaiSanPhamList = value; OnPropertyChanged(); } }

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

        public string _ThongBaoImage;
        public string ThongBaoImage { get => _ThongBaoImage; set { _ThongBaoImage = value; OnPropertyChanged(); } }

        private LOAISANPHAM _SelectedLoaiSanPham = new LOAISANPHAM();
        public LOAISANPHAM SelectedLoaiSanPham
        {
            get => _SelectedLoaiSanPham; set
            {
                _SelectedLoaiSanPham = value; OnPropertyChanged();
                BitmapImage image;
                string dirImage = fullPath + @"ResourceImage\SanPham\default_product.png";
                MyImages.Clear();
                if (SelectedLoaiSanPham != null)
                {
                    //EnaleCapNhatButton = true;
                    //bool fileExists = File.Exists(fullPath + @"ResourceImage\SanPham\" + SelectedLoaiSanPham.HINHANH.Replace(".jpg", ".png"));
                    //if (!string.IsNullOrEmpty(SelectedLoaiSanPham.HINHANH) && fileExists)
                    //{
                    //    dirImage = fullPath + @"ResourceImage\SanPham\" + SelectedLoaiSanPham.HINHANH;
                    //    ThongBaoImage = "";
                    //}
                    //else
                    //{
                    //    ThongBaoImage = "Không có hình ảnh, thêm một hình ảnh vào sản phẩm này";
                    //}

                    EnableXoaButton = true;
                    EnableCapNhatButton = true;
                }
                else
                {
                    ThongBaoImage = "";
                    EnableXoaButton = false;
                    EnableCapNhatButton = false;
                }
                image = new BitmapImage(new Uri(dirImage));
                MyImages.Add(new ImageCustom(fullPath, image));
            }
        }

        private ObservableCollection<LOAISANPHAM> _LoaiSPList;
        public ObservableCollection<LOAISANPHAM> LoaiSPList { get => _LoaiSPList; set { _LoaiSPList = value; OnPropertyChanged(); } }

        private ObservableCollection<String> _LoaiFilter;
        public ObservableCollection<String> LoaiFilter { get => _LoaiFilter; set { _LoaiFilter = value; OnPropertyChanged(); } }

        public LoaiSanPhamVM()
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

            CapNhatLoaiSanPham = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                UpdateLoaiSanPham();
            });

            XoaLoaiSanPham = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                DeleteLoaiSanPham();
            });


            KhoiPhucLoaiSanPhamCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (p != null)
                {
                    KhoiPhucLoaiSanPham KhoiPhucForm = new KhoiPhucLoaiSanPham();
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
            MyImages = new ObservableCollection<ImageCustom>();
            LoaiSanPhamList = new ObservableCollection<LOAISANPHAM>();
            LoaiFilter = new ObservableCollection<String>();
            LogHistory = new ObservableCollection<LOGHISTORY>();
            LoaiFilter.Clear();
            _LoaiSanPhamList.Clear();
            var loaisp = DataProvider.Ins.DB.LOAISANPHAMs.Where(item => item.ACTIVATE);
            foreach (var item in loaisp)
            {
                _LoaiSanPhamList.Add(item);
            }
             
            var listLog = DataProvider.Ins.DB.LOGHISTORies.Where(i => i.CODE == "LSP").ToList();
            listLog.Reverse();
            foreach (var item in listLog)
            {
                LogHistory.Add(item);
            }

            LoaiFilter.Add("Tên");
            LoaiFilter.Add("Mã");

            // load đường dẫn lấy hình ảnh
            var dir = Directory.GetCurrentDirectory().Split('\\');
            fullPath = "";
            foreach (var item in dir)
            {
                if (item != "bin")
                {
                    fullPath += item + @"\";
                }
                else
                {
                    break;
                }
            }
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
                    var loaisp = DataProvider.Ins.DB.LOAISANPHAMs.Where(item => item.MLSP == SelectedLoaiSanPham.MLSP).ToList()[0];
                    loaisp.TENLOAI = SelectedLoaiSanPham.TENLOAI;
                    DataProvider.Ins.DB.SaveChanges();
                    Helper.Helper.WriteLog("Cập nhật loại sản phẩm " + loaisp.MLSP + " thành công vào lúc " + DateTime.UtcNow.ToString() + " bởi " + MainVM.Account.TAIKHOAN1, "LSP");
                    MessageBox.Show("Cập nhật loại sản phẩm thành công");
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
                    var loaisp = DataProvider.Ins.DB.LOAISANPHAMs.Where(item => item.MLSP == SelectedLoaiSanPham.MLSP).ToList()[0];
                    loaisp.ACTIVATE = false;
                    DataProvider.Ins.DB.SaveChanges();
                    Helper.Helper.WriteLog("Xóa loại sản phẩm " + loaisp.MLSP + " thành công vào lúc " + DateTime.UtcNow.ToString() + " bởi " + MainVM.Account.TAIKHOAN1, "LSP");
                    MessageBox.Show("Xóa loại sản phẩm thành công"); 
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
