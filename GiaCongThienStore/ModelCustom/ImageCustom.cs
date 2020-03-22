using GiaCongThienStore.ViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
            var name = this.Name.Split('.');
            this.Name = name[0] + "_" + DateTime.UtcNow.ToBinary() + ".png";
            var dir = Directory.GetCurrentDirectory().Split('\\');
            string fullPath = "";
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
            this.bmp = new Bitmap(this.Path);
            string destinationPath = fullPath + @"ResourceImage\SanPham\" + this.Name;
            if (!Directory.Exists(destinationPath))
            {
                this.bmp.Save(destinationPath, ImageFormat.Png);
                string initFile = System.IO.Path.GetFileName(this.Path);
            } 
            File.Copy(this.Path, destinationPath, true);

        }
    }
}
