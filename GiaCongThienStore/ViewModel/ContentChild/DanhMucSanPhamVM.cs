using GiaCongThienStore.Database;
using GiaCongThienStore.Model;
using GiaCongThienStore.ModelCustom;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace GiaCongThienStore.ViewModel.ContentChild
{
    public class DanhMucSanPhamVM : BaseViewModel
    {
        #region commands
        public ICommand LoadDanhMucSanPhamCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand CapNhatSanPham { get; set; }
        public ICommand OnKeyUpFilterText { get; set; }
        public ICommand XemChiTietSanPham { get; set; }
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

        private ObservableCollection<SANPHAM> _SanPhamList;
        public ObservableCollection<SANPHAM> SanPhamList { get => _SanPhamList; set { _SanPhamList = value; OnPropertyChanged(); } }

        public string fullPath = "";

        public bool _EnaleCapNhatButton = false;
        public bool EnaleCapNhatButton { get => _EnaleCapNhatButton; set { _EnaleCapNhatButton = value; OnPropertyChanged(); } }

        public string _ThongBaoImage;
        public string ThongBaoImage { get => _ThongBaoImage; set { _ThongBaoImage = value; OnPropertyChanged(); } }

        private SANPHAM _SelectedSanPham = new SANPHAM();
        public SANPHAM SelectedSanPham
        {
            get => _SelectedSanPham; set
            {
                _SelectedSanPham = value; OnPropertyChanged();
                BitmapImage image;
                string dirImage = fullPath + @"ResourceImage\SanPham\default_product.png";
                MyImages.Clear();
                if (SelectedSanPham != null)
                { 
                    EnaleCapNhatButton = true;
                    bool fileExists = File.Exists(fullPath + @"ResourceImage\SanPham\" + SelectedSanPham.HINHANH.Replace(".jpg", ".png"));
                    if (!string.IsNullOrEmpty(SelectedSanPham.HINHANH) && fileExists)
                    {
                        dirImage = fullPath + @"ResourceImage\SanPham\" + SelectedSanPham.HINHANH;
                        ThongBaoImage = "";
                    }
                    else
                    {
                        ThongBaoImage = "Không có hình ảnh, thêm một hình ảnh vào sản phẩm này";
                    }
                }
                else
                {
                    ThongBaoImage = "";
                    EnaleCapNhatButton = false;
                }
                image = new BitmapImage(new Uri(dirImage));
                MyImages.Add(new ImageCustom(fullPath, image));
            }
        }

        private ObservableCollection<LOAISANPHAM> _LoaiSPList;
        public ObservableCollection<LOAISANPHAM> LoaiSPList { get => _LoaiSPList; set { _LoaiSPList = value; OnPropertyChanged(); } }

        private ObservableCollection<String> _LoaiFilter;
        public ObservableCollection<String> LoaiFilter { get => _LoaiFilter; set { _LoaiFilter = value; OnPropertyChanged(); } }

        public DanhMucSanPhamVM()
        {
            InitDanhMucSanPhamVM();

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


            CapNhatSanPham = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                MessageBox.Show(p.Text);
            });

            XemChiTietSanPham = new RelayCommand<string>((p) =>
            {
                System.Windows.MessageBox.Show("as");
                MessageBox.Show(p);
                if (string.IsNullOrEmpty(p)) { return false; }
                return true;
            }, (p) =>
            {
                MessageBox.Show(p);
            });

            BackCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                SanPhamList.Clear(); InitDanhMucSanPhamVM();
                p = p as Window;
                if (p != null)
                {
                    p.Close();
                }
            });
        }

        public void InitDanhMucSanPhamVM()
        {
            FilterText = "";
            MyImages = new ObservableCollection<ImageCustom>();
            SanPhamList = new ObservableCollection<SANPHAM>();
            LoaiFilter = new ObservableCollection<String>();
            LoaiFilter.Clear();
            _SanPhamList.Clear();
            foreach (var item in DataProvider.Ins.DB.SANPHAMs)
            {
                _SanPhamList.Add(item);
            }
            LoaiFilter.Add("Tên");
            LoaiFilter.Add("Mã");
            LoaiFilter.Add("Nhà cung cấp");
            LoaiFilter.Add("Loại sản phẩm");


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
            _SanPhamList.Clear();
            SanPhamList.Clear();

            if (query != null || SelectedValueFilter != null)
            {
                FilterText = query;
                if (query.Length == 0)
                {
                    foreach (var item in DataProvider.Ins.DB.SANPHAMs)
                    {
                        SanPhamList.Add(item);
                    }
                }
                else
                {
                    switch (SelectedValueFilter)
                    {
                        case "Tên":
                            foreach (var item in DataProvider.Ins.DB.SANPHAMs.Where(item => item.TENSANPHAM.ToUpper().Contains(query.ToUpper())))
                            {

                                SanPhamList.Add(item);
                            }
                            break;

                        case "Mã":
                            foreach (var item in DataProvider.Ins.DB.SANPHAMs.Where(item => item.CODE.ToUpper().Contains(query.ToUpper())))
                            {
                                SanPhamList.Add(item);
                            }
                            break;
                        case "Nhà cung cấp":
                            foreach (var item in DataProvider.Ins.DB.NHACUNGCAPs.Where(ncc => ncc.TENNHACUNGCAP.ToUpper().Contains(query.ToUpper())).SelectMany(ncc => DataProvider.Ins.DB.SANPHAMs.Where(item => item.MNCC == ncc.MNCC)).ToList())
                            {
                                SanPhamList.Add(item);
                            }
                            break;
                        case "Loại sản phẩm":
                            foreach (var item in DataProvider.Ins.DB.LOAISANPHAMs.Where(lsp => lsp.TENLOAI.ToUpper().Contains(query.ToUpper())).SelectMany(ncc => DataProvider.Ins.DB.SANPHAMs.Where(item => item.MLSP == ncc.MLSP)).ToList())
                            {
                                SanPhamList.Add(item);
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
                foreach (var item in DataProvider.Ins.DB.SANPHAMs)
                {
                    SanPhamList.Add(item);
                }
            }
        }


    }
}
