using GiaCongThienStore.Database;
using GiaCongThienStore.Helper;
using GiaCongThienStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GiaCongThienStore.ViewModel.ContentChild
{
    public class DanhMucSanPhamVM : BaseViewModel
    {
        #region commands
        public ICommand LoadDanhMucSanPhamCommand { get; set; }
        public ICommand BackCommand { get; set; }
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

        private ObservableCollection<SANPHAM> _SanPhamList;
        public ObservableCollection<SANPHAM> SanPhamList { get => _SanPhamList; set { _SanPhamList = value; OnPropertyChanged(); } }

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

            BackCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                SanPhamList.Clear();
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
                            foreach (var item in DataProvider.Ins.DB.NHACUNGCAPs.Where(ncc => ncc.TENNHACUNGCAP.ToUpper().Contains(query.ToUpper())).SelectMany(ncc => DataProvider.Ins.DB.SANPHAMs.Where(item => item.NHACUNGCAP == ncc.MNCC)).ToList())
                            {
                                SanPhamList.Add(item);
                            }
                            break;
                        case "Loại sản phẩm":
                            foreach (var item in DataProvider.Ins.DB.LOAISANPHAMs.Where(lsp => lsp.TENLOAI.ToUpper().Contains(query.ToUpper())).SelectMany(ncc => DataProvider.Ins.DB.SANPHAMs.Where(item => item.LOAISANPHAM == ncc.MLSP)).ToList())
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
