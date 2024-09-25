using Bomberman.MapObjects;
using System.Collections.Generic;
using static Bomberman.Logic.BomberLogic;

namespace Bomberman.Logic
{
    public interface IBomberLogic
    {
        ObstacleType[,] Map { get; set; }
        int[] LocatePlayer();
        int[] LocateBomb();

        int[] LocateEnemyBomb();
        int[] LocateExplosion();

        int[] LocateExpEnemy();
        List<(int, int)> LocateBoxes();
        List<(int, int)> LocateWalls();
        List<(int, int)> LocateFloors();
        int[] LocateLevel();
        int[] LocateEnemy();
    }
}