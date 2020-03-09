using GiaCongThienStore.Database;
using GiaCongThienStore.Model;
using GiaCongThienStore.Helper;
using GiaCongThienStore.ModelCustom;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace GiaCongThienStore.ViewModel.ContentChild
{
    public class ThemSanPhamVM : BaseViewModel
    {
        #region commands 
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand ImgUploadCommand { get; set; }
        public ICommand ClickTaoMoiNhaCungCap { get; set; }
        public ICommand ClickComboboxThongTinSanPham { get; set; }
        public ICommand ClickTaoSanPhamMoi { get; set; }
        #endregion 
        private static bool _isUpdate = false;
        public static bool isUpdate { get => _isUpdate; set { _isUpdate = value; } }

        private static string _idUpdate = "";
        public static string idUpdate { get => _idUpdate; set { _idUpdate = value; } }

        private ObservableCollection<ImageCustom> _MyImages;
        public ObservableCollection<ImageCustom> MyImages { get => _MyImages; set { _MyImages = value; OnPropertyChanged(); } }

        private ObservableCollection<LOAISANPHAM> _LoaiSanPham;
        public ObservableCollection<LOAISANPHAM> LoaiSanPham { get => _LoaiSanPham; set { _LoaiSanPham = value; OnPropertyChanged(); } }

        private LOAISANPHAM _SelectedLoaiSanPham = new LOAISANPHAM();
        public LOAISANPHAM SelectedLoaiSanPham { get => _SelectedLoaiSanPham; set { _SelectedLoaiSanPham = value; OnPropertyChanged(); } }


        private ObservableCollection<NHACUNGCAP> _NhaCungCap;
        public ObservableCollection<NHACUNGCAP> NhaCungCap { get => _NhaCungCap; set { _NhaCungCap = value; OnPropertyChanged(); } }

        private NHACUNGCAP _SelectedNhaCungCap = new NHACUNGCAP();
        public NHACUNGCAP SelectedNhaCungCap { get => _SelectedNhaCungCap; set { _SelectedNhaCungCap = value; OnPropertyChanged(); } }

        private SANPHAM _SanPhamMoi;
        public SANPHAM SanPhamMoi { get => _SanPhamMoi; set { _SanPhamMoi = value; OnPropertyChanged(); } }

        private string _VisibleTaoMoiNhaCungCap = "Collapsed";
        public string VisibleTaoMoiNhaCungCap { get => _VisibleTaoMoiNhaCungCap; set { _VisibleTaoMoiNhaCungCap = value; OnPropertyChanged(); } }

        private string _VisibleChonNhaCungCap = "Visible";
        public string VisibleChonNhaCungCap { get => _VisibleChonNhaCungCap; set { _VisibleChonNhaCungCap = value; OnPropertyChanged(); } }

        private string _Header = "Thêm sản phẩm mới";
        public string Header { get => _Header; set { _Header = value; OnPropertyChanged(); } }

        private string _VisibleButtonAdd = "Visible";
        public string VisibleButtonAdd { get => _VisibleButtonAdd; set { _VisibleButtonAdd = value; OnPropertyChanged(); } }

        private string _VisibleButtonUpdate = "Collapsed";
        public string VisibleButtonUpdate { get => _VisibleButtonUpdate; set { _VisibleButtonUpdate = value; OnPropertyChanged(); } }

        private ObservableCollection<bool> _CheckEnableThongTinSanPham;
        public ObservableCollection<bool> CheckEnableThongTinSanPham { get => _CheckEnableThongTinSanPham; set { _CheckEnableThongTinSanPham = value; OnPropertyChanged(); } }

        private ObservableCollection<bool> _Checked;
        public ObservableCollection<bool> Checked { get => _Checked; set { _Checked = value; OnPropertyChanged(); } }

        private ObservableCollection<string> _TextThongTinSanPham;
        public ObservableCollection<string> TextThongTinSanPham { get => _TextThongTinSanPham; set { _TextThongTinSanPham = value; OnPropertyChanged(); } }
        public ThemSanPhamVM()
        {
            Init();

            LoadedWindowCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (ThemSanPhamVM.isUpdate)
                {
                    Init();
                    InitWithId(ThemSanPhamVM.idUpdate);
                }
                else
                {
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

            ClickTaoMoiNhaCungCap = new RelayCommand<Object>((p) => { return true; }, (p) =>
            {
                var temp = VisibleChonNhaCungCap;
                VisibleChonNhaCungCap = VisibleTaoMoiNhaCungCap;
                VisibleTaoMoiNhaCungCap = temp;

                if (VisibleChonNhaCungCap == "Collapsed")
                {
                    SelectedNhaCungCap = new NHACUNGCAP();
                }
            });

            ClickComboboxThongTinSanPham = new RelayCommand<String>((index) => { return true; }, (index) =>
            {
                int _index = Convert.ToInt32(index);
                CheckEnableThongTinSanPham[_index] = !CheckEnableThongTinSanPham[_index];
                if (CheckEnableThongTinSanPham[_index])
                {
                    TextThongTinSanPham[_index] = "0";
                }
                else
                {
                    TextThongTinSanPham[_index] = "";
                }
            });

            // Thêm mới sản phẩm
            ClickTaoSanPhamMoi = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (!CheckSanPham()) { return; }
                if (!ThemSanPhamVM.isUpdate)
                {
                    if (MessageBox.Show("Đồng ý tạo sản phẩm mới ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
                    {
                        //do no stuff
                    }
                    else
                    {
                        if (!AddHinhAnh()) return; ;
                        if (!AddThongSoChiTietSanPham()) return; ;
                        if (!AddNhaCungCap()) return;

                        DataProvider.Ins.DB.SANPHAMs.Add(SanPhamMoi);
                        DataProvider.Ins.DB.SaveChanges();
                        MessageBox.Show("Tạo sản phẩm mới thành công");
                        _CheckEnableThongTinSanPham.Clear();
                        _TextThongTinSanPham.Clear();
                        Init();
                        p.Close();
                    }
                }
                else
                {
                    if (MessageBox.Show("Xác nhận cập nhật ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.No)
                    {
                        //do no stuff
                    }
                    else
                    {
                        var spInDB = DataProvider.Ins.DB.SANPHAMs.Where(item => item.MSP == SanPhamMoi.MSP).FirstOrDefault();
                        if (spInDB.HINHANH != MyImages[0].Name.Replace(".jpg", ".png"))
                        {
                            if (!AddHinhAnh()) return;
                        }
                        if (Update())
                        {
                            MessageBox.Show("Cập nhật thành công");
                            Init();
                            ThemSanPhamVM.idUpdate = "";
                            ThemSanPhamVM.isUpdate = false;
                            p.Close();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thất bại");
                        }
                    }
                }
            });

            ImgUploadCommand = new RelayCommand<Object>((p) =>
            {
                return true;
            }, (p) =>
            {
                String fullPath = "";
                MyImages.Clear();
                BitmapImage image;
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    image = new BitmapImage(new Uri(op.FileName));
                    fullPath = op.FileName;
                    string[] partsFileName = fullPath.Split('\\');
                    MyImages.Add(new ImageCustom(fullPath, image));
                    //System.Windows.MessageBox.Show(delenFileName[(partsFileName.Length - 1)]);
                    //NewPatient.Image = partsFileName[(delenFileName.Length - 1)];
                }
                //var d = new DirectoryInfo("C:/Users/Tamaki/source/repos/GiaCongThienStore/GiaCongThienStore/ResourceImage");
                //var images = d.GetFiles();
                //MyImages = images.Select(x => new ImageCustom(x.Name, new BitmapImage(new Uri(x.FullName))));

            });
        }
        public bool Update()
        {
            var spInDB = DataProvider.Ins.DB.SANPHAMs.Where(item => item.MSP == SanPhamMoi.MSP).FirstOrDefault();
            spInDB.HINHANH = SanPhamMoi.HINHANH;
            spInDB.LCHIEUDAI = SanPhamMoi.LCHIEUDAI;
            if (IsNumberHelper.IsNumeric(TextThongTinSanPham[0]))
            {
                spInDB.DTRONG = Convert.ToDouble(TextThongTinSanPham[0]);
            }
            if (IsNumberHelper.IsNumeric(TextThongTinSanPham[1]))
            {
                spInDB.DNGOAI = Convert.ToDouble(TextThongTinSanPham[1]);
            }
            if (IsNumberHelper.IsNumeric(TextThongTinSanPham[2]))
            {
                spInDB.LCHIEUDAI = Convert.ToDouble(TextThongTinSanPham[2]);
            }
            if (IsNumberHelper.IsNumeric(TextThongTinSanPham[3]))
            {
                spInDB.PHIDUONGKINH = Convert.ToDouble(TextThongTinSanPham[3]);
            }
            spInDB.TENSANPHAM = SanPhamMoi.TENSANPHAM;
            spInDB.CODE = SanPhamMoi.CODE;
            spInDB.DONVITINH = SanPhamMoi.DONVITINH;
            spInDB.GHICHU = SanPhamMoi.GHICHU;
            spInDB.MLSP = SanPhamMoi.MLSP;
            spInDB.MNCC = SanPhamMoi.MNCC;
            var loaiSP = DataProvider.Ins.DB.LOAISANPHAMs.Where(item => item.MLSP == SelectedLoaiSanPham.MLSP).FirstOrDefault();
            var nhaCungCap = DataProvider.Ins.DB.NHACUNGCAPs.Where(item => item.MNCC == SelectedNhaCungCap.MNCC).FirstOrDefault();
            spInDB.LOAISANPHAM = loaiSP;
            spInDB.NHACUNGCAP = nhaCungCap;
            DataProvider.Ins.DB.SaveChanges();
            return true;
        }
        public bool InitWithId(string MSP)
        {
            ThemSanPhamVM.isUpdate = true;
            if (ThemSanPhamVM.isUpdate)
            {
                VisibleButtonUpdate = "Visible";
                VisibleButtonAdd = "Collapsed";
                VisibleTaoMoiNhaCungCap = "Collapsed";
                VisibleChonNhaCungCap = "Visible";
            }
            CheckEnableThongTinSanPham.Clear(); Checked.Clear();
            CheckEnableThongTinSanPham = new ObservableCollection<bool>() { false, false, false, false };
            Checked = new ObservableCollection<bool>() { false, false, false, false };

            Header = "Cập nhật sản phẩm";
            var sanPham = DataProvider.Ins.DB.SANPHAMs.Where(item => item.MSP == MSP).FirstOrDefault();
            SanPhamMoi = sanPham;
            if (sanPham == null)
            {
                return false;
            }
            else
            {
                var loaiSP = DataProvider.Ins.DB.LOAISANPHAMs.Where(item => item.MLSP == sanPham.MLSP).FirstOrDefault();
                var nhaCungCap = DataProvider.Ins.DB.NHACUNGCAPs.Where(item => item.MNCC == sanPham.MNCC).FirstOrDefault();
                SelectedLoaiSanPham = loaiSP;
                SelectedNhaCungCap = nhaCungCap;
                if (sanPham.DTRONG != 0)
                {
                    _Checked[0] = true;
                    _CheckEnableThongTinSanPham[0] = true;
                    TextThongTinSanPham[0] = sanPham.DTRONG.ToString();
                }
                if (sanPham.DNGOAI != 0)
                {
                    _Checked[1] = true;
                    _CheckEnableThongTinSanPham[1] = true;
                    TextThongTinSanPham[1] = sanPham.DNGOAI.ToString();
                }
                if (sanPham.LCHIEUDAI != 0)
                {
                    _Checked[2] = true;
                    _CheckEnableThongTinSanPham[2] = true;
                    TextThongTinSanPham[2] = sanPham.LCHIEUDAI.ToString();
                }
                if (sanPham.PHIDUONGKINH != 0)
                {
                    _Checked[3] = true;
                    _CheckEnableThongTinSanPham[3] = true;
                    TextThongTinSanPham[3] = sanPham.PHIDUONGKINH.ToString();
                }
                if (sanPham.HINHANH != @"default_product.png" && sanPham.HINHANH.Split('.').Length > 1 && sanPham.HINHANH.Split('.')[1] == "png")
                {
                    MyImages.Clear();
                    // load đường dẫn lấy hình ảnh
                    var dir = Directory.GetCurrentDirectory().Split('\\');
                    var fullPath = "";
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
                    fullPath = fullPath + @"ResourceImage\SanPham\" + sanPham.HINHANH;
                    BitmapImage image = new BitmapImage(new Uri(fullPath));
                    MyImages.Add(new ImageCustom(fullPath, image));
                }
            }
            return true;
        }


        public void Init()
        {
            _VisibleTaoMoiNhaCungCap = "Collapsed";
            _VisibleChonNhaCungCap = "Visible";
            _Header = "Thêm sản phẩm mới";
            _VisibleButtonAdd = "Visible";
            ThemSanPhamVM.isUpdate = false;
            if (!ThemSanPhamVM.isUpdate)
            {
                VisibleButtonUpdate = "Collapsed";
                VisibleButtonAdd = "Visible";
            }
            _LoaiSanPham = new ObservableCollection<LOAISANPHAM>();
            _NhaCungCap = new ObservableCollection<NHACUNGCAP>();
            if (_CheckEnableThongTinSanPham != null)
            {
                _CheckEnableThongTinSanPham.Clear(); _Checked.Clear();
            }
            _CheckEnableThongTinSanPham = new ObservableCollection<bool>() { false, false, false, false };
            _Checked = new ObservableCollection<bool>() { false, false, false, false };
            TextThongTinSanPham = new ObservableCollection<string>() { "", "", "", "" };
            _MyImages = new ObservableCollection<ImageCustom>();
            MyImages.Clear();
            _NhaCungCap.Clear();
            SelectedLoaiSanPham = new LOAISANPHAM();
            SelectedNhaCungCap = new NHACUNGCAP();
            SanPhamMoi = new SANPHAM();

            var pathDefault = @"..\..\ResourceImage\SanPham\default_product.png";
            BitmapImage image = new BitmapImage(new Uri(pathDefault, UriKind.Relative));
            MyImages.Add(new ImageCustom(pathDefault, image));
            SanPhamMoi.HINHANH = @"default_product.png";

            foreach (var item in DataProvider.Ins.DB.LOAISANPHAMs)
            {
                _LoaiSanPham.Add(item);
            }
            foreach (var item in DataProvider.Ins.DB.NHACUNGCAPs)
            {
                _NhaCungCap.Add(item);
            }


        }

        public bool CheckSanPham()
        {
            if (string.IsNullOrEmpty(SanPhamMoi.CODE))
            {
                MessageBox.Show("Bạn chưa điền code (mã) sản phẩm");
                return false;
            }
            if (string.IsNullOrEmpty(SanPhamMoi.TENSANPHAM))
            {
                MessageBox.Show("Bạn chưa điền tên sản phẩm");
                return false;
            }
            var isSanPham = DataProvider.Ins.DB.SANPHAMs.Select(item => item.TENSANPHAM == SanPhamMoi.TENSANPHAM || item.CODE == SanPhamMoi.CODE).FirstOrDefault();
            if (!ThemSanPhamVM.isUpdate)
            {
                if (DataProvider.Ins.DB.SANPHAMs.Select(item => item.TENSANPHAM == SanPhamMoi.TENSANPHAM || item.CODE == SanPhamMoi.CODE).FirstOrDefault())
                {
                    MessageBox.Show("Tên sản phẩm đã tồn tại");
                    return false;
                }
                if (DataProvider.Ins.DB.SANPHAMs.Select(item => item.CODE == SanPhamMoi.CODE).FirstOrDefault())
                {
                    MessageBox.Show("Code đã tồn tại");
                    return false;
                }
            }
            if (SelectedLoaiSanPham == null || string.IsNullOrEmpty(SelectedLoaiSanPham.TENLOAI))
            {
                MessageBox.Show("Bạn chưa chọn loại sản phẩm");
                return false;
            }
            if (string.IsNullOrEmpty(SanPhamMoi.QUYCACH))
            {
                MessageBox.Show("Bạn chưa điền quy cách");
                return false;
            }
            if (string.IsNullOrEmpty(SanPhamMoi.DONVITINH))
            {
                MessageBox.Show("Bạn chưa điền đơn vị tính");
                return false;
            }
            return true;
        }

        public bool AddHinhAnh()
        {
            foreach (var item in MyImages)
            {
                MessageBox.Show(item.Name);
                if (item.Name == "default_product.png")
                {
                    return true;
                }
                SanPhamMoi.HINHANH = item.Name.Replace(".jpg", ".png");
                item.SaveImage();
            }
            return true;
        }

        public bool AddNhaCungCap()
        {
            if (string.IsNullOrEmpty(SelectedNhaCungCap.TENNHACUNGCAP))
            {
                MessageBox.Show("Bạn chưa chọn nhà cung cấp");
                return false;
            }
            if (string.IsNullOrEmpty(SelectedNhaCungCap.SDT))
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại");
                return false;
            }
            if (string.IsNullOrEmpty(SelectedNhaCungCap.DIACHI))
            {
                SelectedNhaCungCap.DIACHI = "";
            }
            if (string.IsNullOrEmpty(SelectedNhaCungCap.GHICHU))
            {
                SelectedNhaCungCap.GHICHU = "";
            }

            // chọn nhà cung cấp có sẵn
            if (VisibleChonNhaCungCap == "Visible")
            {
                var ncc = DataProvider.Ins.DB.NHACUNGCAPs.Where(item => item.MNCC == SelectedNhaCungCap.MNCC).FirstOrDefault();
                if (ncc.MNCC != null)
                {
                    SanPhamMoi.MNCC = ncc.MNCC;
                    SanPhamMoi.NHACUNGCAP = ncc;
                }
            }
            else
            {
                var mc = NhaCungCap[NhaCungCap.Count() - 1];
                NHACUNGCAP nc = new NHACUNGCAP();
                if (mc != null)
                {
                    nc.MNCC = Helper.Helper.GetNewID("NCC", mc.MNCC);
                }
                else
                {
                    nc.MNCC = "NCC0000001";
                }
                nc.TENNHACUNGCAP = SelectedNhaCungCap.TENNHACUNGCAP;
                nc.SDT = SelectedNhaCungCap.SDT;
                nc.DIACHI = SelectedNhaCungCap.DIACHI;
                nc.GHICHU = SelectedNhaCungCap.GHICHU;
                nc.NGAYKHOITAO = DateTime.UtcNow;

                SanPhamMoi.MNCC = SelectedNhaCungCap.MNCC;
                SanPhamMoi.NHACUNGCAP = nc;
                MessageBox.Show("Tạo nhà cung cấp mới thành công !!");
                DataProvider.Ins.DB.NHACUNGCAPs.Add(nc);
                DataProvider.Ins.DB.SaveChanges();
            }
            return true;
        }

        public bool AddThongSoChiTietSanPham()
        {
            bool isChecked = false;
            for (int i = 0; i < CheckEnableThongTinSanPham.Count(); i++)
            {
                isChecked = CheckEnableThongTinSanPham.ElementAt(i);
                // kiểm tra ô đó đã check chưa và thông số có phải là số hay không?
                if (isChecked)
                {
                    if (IsNumberHelper.IsNumeric(TextThongTinSanPham[i]))
                    {
                        if (i == 0)
                        {
                            SanPhamMoi.DTRONG = Convert.ToDouble(TextThongTinSanPham[i]);
                        }
                        else if (i == 1)
                        {
                            SanPhamMoi.DNGOAI = Convert.ToDouble(TextThongTinSanPham[i]);
                        }
                        else if (i == 2)
                        {
                            SanPhamMoi.LCHIEUDAI = Convert.ToDouble(TextThongTinSanPham[i]);
                        }
                        else if (i == 3)
                        {
                            SanPhamMoi.PHIDUONGKINH = Convert.ToDouble(TextThongTinSanPham[i]);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Thông số phải là số: " + TextThongTinSanPham[i]);
                        return false;
                    }
                }
            }
            var listSP = DataProvider.Ins.DB.SANPHAMs.ToList();
            SanPhamMoi.MSP = Helper.Helper.GetNewID("SP", listSP[listSP.Count() - 1].MSP);
            SanPhamMoi.MLSP = SelectedLoaiSanPham.MLSP;
            SanPhamMoi.LOAISANPHAM = SelectedLoaiSanPham;
            SanPhamMoi.NGAYKHOITAO = DateTime.UtcNow;
            SanPhamMoi.SOLUONG = 0;
            SanPhamMoi.ACTIVATE = true;
            if (string.IsNullOrEmpty(SanPhamMoi.GHICHU))
            {
                SanPhamMoi.GHICHU = "";
            }
            return true;
        }
    }
}
