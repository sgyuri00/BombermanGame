namespace Bomberman.Logic
{
    public interface IEnemyLogic
    {
        void InitTimer();
        int PlayerHealth { get; }
    }
}