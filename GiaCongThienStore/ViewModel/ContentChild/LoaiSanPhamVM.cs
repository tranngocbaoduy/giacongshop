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

        private ObservableCollection<LOAISANPHAM> _LoaiSanPhamList;
        public ObservableCollection<LOAISANPHAM> LoaiSanPhamList { get => _LoaiSanPhamList; set { _LoaiSanPhamList = value; OnPropertyChanged(); } }

        public string fullPath = "";

        public bool _EnaleCapNhatButton = false;
        public bool EnaleCapNhatButton { get => _EnaleCapNhatButton; set { _EnaleCapNhatButton = value; OnPropertyChanged(); } }

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

        public LoaiSanPhamVM()
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

        }

        public void InitDanhMucSanPhamVM()
        {
            FilterText = "";
            MyImages = new ObservableCollection<ImageCustom>();
            LoaiSanPhamList = new ObservableCollection<LOAISANPHAM>();
            LoaiFilter = new ObservableCollection<String>();
            LoaiFilter.Clear();
            _LoaiSanPhamList.Clear();
            foreach (var item in DataProvider.Ins.DB.LOAISANPHAMs)
            {
                _LoaiSanPhamList.Add(item);
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
                            foreach (var item in DataProvider.Ins.DB.LOAISANPHAMs.Where(item => item.TENLOAI.ToUpper().Contains(query.ToUpper())))
                            {

                                LoaiSanPhamList.Add(item);
                            }
                            break;

                        case "Mã":
                            foreach (var item in DataProvider.Ins.DB.LOAISANPHAMs.Where(item => item.MLSP.ToUpper().Contains(query.ToUpper())))
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


    }
}
