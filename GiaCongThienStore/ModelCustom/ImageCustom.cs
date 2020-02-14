using GiaCongThienStore.ViewModel;
using System;
using System.Collections.Generic;
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

        public ImageCustom(string fullPath, BitmapImage image)
        {
            this.Name = fullPath;
            this.ImageSource = image;
        }

        public String Name { get; set; }

    }
}
