using GiaCongThienStore.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GiaCongThienStore.ModelCustom
{
    public class ImageCustom
    {

        public ImageSource ImageSource { get; set; }
        public Bitmap bmp { get; set; }

        public ImageCustom(string fullPath, BitmapImage image)
        {
            this.Path = fullPath;
            this.ImageSource = image; 
            var splitString = this.Path.Split('\\');
            this.Name = splitString[splitString.Length - 1];
        }

        public String Name { get; set; }
        public String Path { get; set; }


        public void SaveImage()
        {
            this.bmp = new Bitmap(this.Path); 
            this.Name = @"..\..\ResourceImage\SanPham\" + this.Name;
            this.bmp.Save(this.Name, ImageFormat.Png); 
        }
    }
}
