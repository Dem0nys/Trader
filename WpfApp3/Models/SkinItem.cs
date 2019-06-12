using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfApp3.Models
{
    public class SkinItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BitmapImage Image { get; set; }
        public double Price { get; set; }
    }
}
