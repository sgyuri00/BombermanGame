using Bomberman.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Bomberman.Controller
{
    public class GameController
    {
        IPlayerLogic control;
        public GameController(IPlayerLogic control)
        {
            this.control = control;
        }

        public void KeyPressed(Key key)
        {
            switch (key)
            {
                case Key.Space:
                    control.Move(PlayerLogic.Directions.Space);
                    break;

                case Key.Up: 
                    control.Move(PlayerLogic.Directions.Up);
                    break;

                case Key.Down:
                    control.Move(PlayerLogic.Directions.Down);
                    break;

                case Key.Left:
                    control.Move(PlayerLogic.Directions.Left);
                    break;

                case Key.Right:
                    control.Move(PlayerLogic.Directions.Right);
                    break;
            }
        }  
    }
}
