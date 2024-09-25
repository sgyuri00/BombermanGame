using Bomberman.Logic;
using Bomberman.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.ViewModel
{
    public class MainWindowViewModel
    {
        public Player player;
        public int PlayerHealth
        {
            get
            {
                return player.Health;
            }
        }
        public void Setup(Player player)
        {
            this.player = player;
        }

        public MainWindowViewModel()
        {
        }

        //public MainWindowViewModel(Player player)
        //{
        //    this.player = player;
        //}
    }
}
