using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Bomberman.MapObjects
{
    public abstract class MapObject
    {

        public int X { get; set; }
        public int Y { get; set; }

        public Image Image { get; set; }

        public MapObject(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

    }
}
