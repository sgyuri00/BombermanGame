using System;
using System.Windows.Input;

namespace Bomberman.Logic
{
    public interface IPlayerLogic
    {
        enum Directions { Up, Down, Left, Right };
        void Move(PlayerLogic.Directions direction);
        int PlayerHealth { get;}
    }
}