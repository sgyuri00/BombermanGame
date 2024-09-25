using System.Windows.Input;
using static Bomberman.Logic.IPlayerLogic;

namespace Bomberman.Controller
{
    public interface IGameControl
    {
        void Move(Directions direction);
    }
}