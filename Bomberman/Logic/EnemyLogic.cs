using Bomberman.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using static Bomberman.Logic.BomberLogic;

namespace Bomberman.Logic
{
    public class EnemyLogic : IEnemyLogic
    {
        Timer Timer1;
        Timer BlastTimer;
        Timer BombTimer;
        Timer FiveTimer;
        public int difficulty;

        DispatcherTimer Timer2 = new DispatcherTimer();
        DispatcherTimer PlayerDelay = new DispatcherTimer();
        Bomb enemyBomb = new Bomb(0, 0);
        Blast enemyBlast = new Blast(0, 0);      
        Random rnd = new Random();
        IBomberLogic logic;
        Player player { get; set; }
        Enemy enemy { get; set; }

        public int PlayerHealth
        {
            get
            {
                return player.Health;
            }
        }

        public EnemyLogic(IBomberLogic logic, int diff, Player player, Enemy enemy )
        {
            this.logic = logic;
            this.difficulty = diff;
            this.player = player;
            this.enemy = enemy;
            InitTimer();
            InitFiveTimer();
        }

        public void InitTimer()
        {   
            Timer1 = new Timer(TimerCallback, null, 0, 1000 - 100 * difficulty);
            ;
        }

        public void InitFiveTimer()
        {           
            FiveTimer = new Timer(FiveTimerCallback, null, 0, 7020);
        }

        public void InitBlastTimer()
        {
            var coords = logic.LocateEnemy();
            while (coords[0] < 0 || coords[1] < 0)
            {
                coords = logic.LocateEnemy();
            }
            logic.Map[coords[0], coords[1]] = ObstacleType.EPB1;

            enemyBomb.X = coords[0];
            enemyBomb.Y = coords[1];

            BlastTimer = new Timer(BlastTimerCallback, null, 3010, 0);
        }

        public void InitBombTimer()
        {
            BombTimer = new Timer(BombTimerCallback, null, 4010, 0);
        }

        private void TimerCallback(Object stateInfo)
        {
            int i;
            int j;

            var coords = logic.LocateEnemy();
            var coords2 = logic.LocateExpEnemy();
            var coords3 = logic.LocatePlayer();
            int[] moveToPlayer = { coords3[0] - coords[0], coords3[1] - coords[1] };
            if (coords[0] == -1)
            {
                i = coords2[0];
                j = coords2[1];
            }
            else
            {
                i = coords[0];
                j = coords[1];
            }
            int old_i = i;
            int old_j = j;

            List<int> moveRoulette = new List<int>();
            moveRoulette.Add(rnd.Next(1, 9));
            bool fel = false;
            bool le = false;
            bool balra = false;
            bool jobbra = false;

            if (logic.Map[i+1, j] == ObstacleType.Floor) //le
            {
                le = true;
            }

            if (logic.Map[i - 1, j] == ObstacleType.Floor) //fel
            {
                fel = true;
            }

            if (logic.Map[i, j + 1] == ObstacleType.Floor) //jobbra
            {
                jobbra = true;
            }

            if (logic.Map[i, j - 1] == ObstacleType.Floor) //balra
            {
                balra = true;
            }

            if (moveToPlayer[0] < 0 && moveToPlayer[1] < 0 && (fel || balra))
            {
                moveRoulette.Add(rnd.Next(4, 6));
            }

            if (moveToPlayer[0] < 0 && moveToPlayer[1] > 0 && (fel || jobbra))
            {
                moveRoulette.Add(rnd.Next(1, 3));
            }

            if (moveToPlayer[0] > 0 && moveToPlayer[1] < 0 && (le || balra))
            {
                moveRoulette.Add(rnd.Next(3, 5));
            }

            if (moveToPlayer[0] > 0 && moveToPlayer[1] > 0 && (le || jobbra))
            {
                moveRoulette.Add(rnd.Next(2, 4));
            }

            if (moveToPlayer[0] == 0 && (fel || le))
            {
                moveRoulette.Add(rnd.Next(5, 7));
            }

            if (moveToPlayer[1] == 0 && (jobbra || balra))
            {
                moveRoulette.Add(rnd.Next(7, 9));
            }

            int whereToMove = moveRoulette[rnd.Next(moveRoulette.Count)];

            switch (whereToMove)
            {
                case 1: case 5: //fel
                    if (i - 1 >= 0)
                    {
                        i--;
                    }
                    break;
                case 3: case 6: //le
                    if (i + 1 < logic.Map.GetLength(0))
                    {
                        i++;
                    }
                    break;
                case 4: case 7: //balra
                    if (j - 1 >= 0)
                    {
                        j--;
                    }
                    break;
                case 2: case 8: //jobbra
                    if (j + 1 < logic.Map.GetLength(1))
                    {
                        j++;
                    }
                    break;
                default:
                    break;
            }


            if (logic.Map[i, j] == ObstacleType.Floor && logic.Map[old_i, old_j] == ObstacleType.EPB1)
            {
                logic.Map[i, j] = ObstacleType.Enemy1;
                logic.Map[old_i, old_j] = ObstacleType.Bomb;
            }
            else if (logic.Map[i, j] == ObstacleType.Floor && logic.Map[old_i, old_j] == ObstacleType.ExpEnemy)
            {
                logic.Map[i, j] = ObstacleType.Enemy1;
                logic.Map[old_i, old_j] = ObstacleType.Floor;
            }
            else if (logic.Map[i, j] == ObstacleType.Floor)
            {
                logic.Map[i, j] = ObstacleType.Enemy1;
                logic.Map[old_i, old_j] = ObstacleType.Floor;
            }
        }

        private void BlastTimerCallback(Object stateInfo)
        {
            enemyBlast.BlastRange = 3;
            enemyBlast.CalculateRange(enemyBomb.X, enemyBomb.Y, logic.LocateBoxes(), logic.LocateWalls(), logic.LocateLevel(), logic.LocateBomb(), logic.LocateEnemyBomb());

            foreach (var item in enemyBlast.BlastPosition)
            {
                if (logic.Map[item.x, item.y] == ObstacleType.Bomb || logic.Map[item.x, item.y] == ObstacleType.Box || logic.Map[item.x, item.y] == ObstacleType.Blast || logic.Map[item.x, item.y] == ObstacleType.EnemyBomb || logic.Map[item.x, item.y] == ObstacleType.Floor || logic.Map[item.x, item.y] == ObstacleType.SpeedBoost || logic.Map[item.x, item.y] == ObstacleType.HealthBoost || logic.Map[item.x, item.y] == ObstacleType.BlastRangeBoost)
                {
                    logic.Map[item.x, item.y] = ObstacleType.EnemyBlast;
                }
                else if (logic.Map[item.x, item.y] == ObstacleType.Player || logic.Map[item.x, item.y] == ObstacleType.PPB)
                {
                    logic.Map[item.x, item.y] = ObstacleType.Explosion; //EZ EGY PLAYER DAMAGED KÉP
                    player.Health--;                  
                    if (player.Health == 0)
                    {
                        MessageBox.Show("Uggh, game over! You will get em next time!");
                    }
                }
            }
        }

        private void BombTimerCallback(Object stateInfo)
        {
            foreach (var item in enemyBlast.BlastPosition)
            {
                if (logic.Map[item.x, item.y] == ObstacleType.EnemyBlast || logic.Map[item.x, item.y] == ObstacleType.Blast)
                {
                    logic.Map[item.x, item.y] = ObstacleType.Floor;
                }
                else if (logic.Map[item.x, item.y] == ObstacleType.ExpEnemy)
                {
                    logic.Map[item.x, item.y] = ObstacleType.Enemy1;
                }
                else if (logic.Map[item.x, item.y] == ObstacleType.EPB1)
                {
                    logic.Map[item.x, item.y] = ObstacleType.Enemy1;
                }
                else if (logic.Map[item.x, item.y] == ObstacleType.BlastPowerUp) // ENEMY NEM SPAWNOL TÁRGYAT
                {
                    logic.Map[item.x, item.y] = ObstacleType.Floor;
                }
                else if (logic.Map[item.x, item.y] == ObstacleType.Explosion)
                {
                    logic.Map[item.x, item.y] = ObstacleType.Player;
                }
            }
        }

        private void FiveTimerCallback(Object stateInfo)
        {
            InitBlastTimer();
            InitBombTimer();
        }
    }
}
