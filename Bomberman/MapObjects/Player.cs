using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bomberman.MapObjects
{
    public class Player : MapObject
    {
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int Speed { get; set; }
        public int Health { get; set; }
        public bool IsDead { get; set; }

        public int BombAmount { get; set; }



        public Player(int x, int y) : base(x, y)
        {
            this.Health = 3;
            this.IsDead = false;
            this.BombAmount = 1;
            this.Speed = 700;
        }

        public void Heal()
        {
            if (Health < 5)
            {
                Health++;
            }
        }

        public void SpeedUp()
        {
            Speed += 50;
        }

    }
}
