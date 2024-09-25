using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.MapObjects
{
    public class Enemy : MapObject
    {
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int Health { get; set; }
        public bool IsDead { get; set; }
        public int BombAmount { get; set; }

        public Enemy(int x, int y) : base(x, y)
        {
            this.Health = 3;
            this.IsDead = false;
            this.BombAmount = 1;
        }
    }
}
