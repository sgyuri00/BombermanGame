using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.MapObjects
{
    public class PowerUp : MapObject
    {
        public PowerUp(int x, int y) : base(x, y)
        {

        }
        public virtual void UsePowerup(Player player)
        {

        }

    }
}
