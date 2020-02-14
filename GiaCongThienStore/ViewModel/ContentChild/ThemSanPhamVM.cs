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

namespace GiaCongThienStore.ViewModel.ContentChild
{
    public class ThemSanPhamVM : BaseViewModel
    {
        #region commands 
        public ICommand BackCommand { get; set; }
        public ICommand ImgUploadCommand { get; set; }
        #endregion 

        private ObservableCollection<ImageCustom> _MyImages;
        public ObservableCollection<ImageCustom> MyImages { get=> _MyImages; set { _MyImages = value; OnPropertyChanged(); } }
        public ThemSanPhamVM()
        {

            _MyImages = new ObservableCollection<ImageCustom>();

            BackCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p = p as Window;
                if (p != null)
                {
                    p.Close();
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
                    MessageBox.Show(fullPath);
                    //System.Windows.MessageBox.Show(delenFileName[(partsFileName.Length - 1)]);
                    //NewPatient.Image = partsFileName[(delenFileName.Length - 1)];
                }
                MessageBox.Show("asd");
                var d = new DirectoryInfo("C:/Users/Tamaki/source/repos/GiaCongThienStore/GiaCongThienStore/ResourceImage");
                var images = d.GetFiles();
                //MyImages = images.Select(x => new ImageCustom(x.Name, new BitmapImage(new Uri(x.FullName))));

            });
        }
    }
}
