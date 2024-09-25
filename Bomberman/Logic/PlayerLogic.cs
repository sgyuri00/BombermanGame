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
    public class PlayerLogic : IPlayerLogic
    {
        DispatcherTimer Timer1 = new DispatcherTimer();
        DispatcherTimer Timer2 = new DispatcherTimer();

        Bomb bomb = new Bomb(0,0);
        Blast blast = new Blast(0,0);

        IBomberLogic logic;
        public Player player { get; set; }
        Enemy enemy { get; set; }

        public int PlayerHealth
        {
            get
            {
                return player.Health;
                ;
            }
        }

        public PlayerLogic(IBomberLogic logic, Player player, Enemy enemy)
        {
            this.logic = logic;
            this.player = player;
            this.enemy = enemy;
        }

        public enum Directions
        {
            Up, Down, Left, Right, Space
        }
        
        public void Move(Directions direction)
        {
            int i;
            int j;

            var coords = logic.LocatePlayer();
            var coords2 = logic.LocateExplosion();
            if(coords[0] == -1)
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

            switch (direction)
            {
                case Directions.Up:
                    if (i - 1 >= 0)
                    {
                        i--;
                    }
                    break;
                case Directions.Down:
                    if (i + 1 < logic.Map.GetLength(0))
                    {
                        i++;
                    }
                    break;
                case Directions.Left:
                    if (j - 1 >= 0)
                    {
                        j--;
                    }
                    break;
                case Directions.Right:
                    if (j + 1 < logic.Map.GetLength(1))
                    {
                        j++;
                    }
                    break;
                case Directions.Space:                 
                    {
                        if (player.BombAmount == 1)
                        {
                            logic.Map[old_i, old_j] = ObstacleType.PPB;

                            bomb.X = old_i;
                            bomb.Y = old_j;
                            player.BombAmount = 0;

                            Timer1.Interval = new TimeSpan(0,0,3);
                            Timer2.Interval = new TimeSpan(0,0,4);
                            Timer1.Tick += Timer_Tick1;
                            Timer2.Tick += Timer_Tick2;
                            Timer1.Start();
                            Timer2.Start();
                        }
                        
                    }
                    break;
                default:
                    break;
            }


            if (logic.Map[i, j] == ObstacleType.Floor && logic.Map[old_i, old_j] == ObstacleType.PPB)
            {
                logic.Map[i, j] = ObstacleType.Player;
                logic.Map[old_i, old_j] = ObstacleType.Bomb;
            }
            else if((logic.Map[i, j] == ObstacleType.Blast || logic.Map[i, j] == ObstacleType.EnemyBlast) && logic.Map[old_i, old_j] == ObstacleType.Player)
            {
                logic.Map[i, j] = ObstacleType.Explosion;
                logic.Map[old_i, old_j] = ObstacleType.Floor;
                player.Health--;
                if (player.Health == 0)
                {
                    MessageBox.Show("Uggh, game over! You will get em next time!");
                }
            }
            else if(logic.Map[i, j] == ObstacleType.EnemyBlast && logic.Map[old_i, old_j] == ObstacleType.Explosion)
            {
                logic.Map[i, j] = ObstacleType.Explosion;
                logic.Map[old_i, old_j] = ObstacleType.EnemyBlast;
            }
            else if (logic.Map[i, j] == ObstacleType.Blast  && logic.Map[old_i, old_j] == ObstacleType.Explosion)
            {
                logic.Map[i, j] = ObstacleType.Explosion;
                logic.Map[old_i, old_j] = ObstacleType.Blast;
            }
            else if(logic.Map[i, j] == ObstacleType.Floor && logic.Map[old_i, old_j] == ObstacleType.Explosion)
            {
                logic.Map[i, j] = ObstacleType.Player;
                logic.Map[old_i, old_j] = ObstacleType.Floor;
            }
            else if(logic.Map[i, j] == ObstacleType.Floor)
            {
                logic.Map[i, j] = ObstacleType.Player;
                logic.Map[old_i, old_j] = ObstacleType.Floor;
            }
            else if (logic.Map[i, j] == ObstacleType.Floor)
            {
                logic.Map[i, j] = ObstacleType.Player;
                logic.Map[old_i, old_j] = ObstacleType.Floor;
            }
            else if (logic.Map[i, j] == ObstacleType.BlastRangeBoost)
            {
                logic.Map[i, j] = ObstacleType.Player;
                if(logic.Map[old_i, old_j] == ObstacleType.PPB) logic.Map[old_i, old_j] = ObstacleType.Bomb;
                else logic.Map[old_i, old_j] = ObstacleType.Floor;
                blast.BlastRange++;
            }
            else if (logic.Map[i, j] == ObstacleType.HealthBoost)
            {
                logic.Map[i, j] = ObstacleType.Player;
                if(logic.Map[old_i, old_j] == ObstacleType.PPB) logic.Map[old_i, old_j] = ObstacleType.Bomb;
                else logic.Map[old_i, old_j] = ObstacleType.Floor;
                player.Heal();
            }
            else if (logic.Map[i, j] == ObstacleType.SpeedBoost)
            {
                logic.Map[i, j] = ObstacleType.Player;
                if(logic.Map[old_i, old_j] == ObstacleType.PPB) logic.Map[old_i, old_j] = ObstacleType.Bomb;
                else logic.Map[old_i, old_j] = ObstacleType.Floor;
                player.SpeedUp();
            }
        } 

        private void Timer_Tick1(object sender, EventArgs e)
        {
            blast.CalculateRange(bomb.X, bomb.Y, logic.LocateBoxes(), logic.LocateWalls(), logic.LocateLevel(), logic.LocateBomb(), logic.LocateEnemyBomb());

            foreach (var item in blast.BlastPosition)
            {
                if (logic.Map[item.x, item.y] == ObstacleType.Bomb || logic.Map[item.x, item.y] == ObstacleType.EnemyBlast || logic.Map[item.x, item.y] == ObstacleType.Floor || logic.Map[item.x, item.y] == ObstacleType.SpeedBoost || logic.Map[item.x, item.y] == ObstacleType.HealthBoost || logic.Map[item.x, item.y] == ObstacleType.BlastRangeBoost)
                {
                    logic.Map[item.x, item.y] = ObstacleType.Blast;
                }
                else if (logic.Map[item.x, item.y] == ObstacleType.Box)
                {
                    logic.Map[item.x, item.y] = ObstacleType.BlastPowerUp; 
                }
                else if (logic.Map[item.x, item.y] == ObstacleType.Player)
                {
                    logic.Map[item.x, item.y] = ObstacleType.Explosion; //EZ EGY PLAYER DAMAGED KÉP
                    player.Health--;                    
                    if (player.Health == 0)
                    {
                        MessageBox.Show("Uggh, game over! You will get em next time!");
                    }
                }
                else if (logic.Map[item.x, item.y] == ObstacleType.PPB)
                {
                    logic.Map[item.x, item.y] = ObstacleType.Explosion; //EZ EGY PLAYER DAMAGED KÉP
                    player.Health--;
                    if (player.Health == 0)
                    {
                        MessageBox.Show("Uggh, game over! You will get em next time!");
                    }
                }
                else if (logic.Map[item.x, item.y] == ObstacleType.Enemy1 || logic.Map[item.x, item.y] == ObstacleType.EPB1)
                {
                    logic.Map[item.x, item.y] = ObstacleType.ExpEnemy; //EZ EGY PLAYER DAMAGED KÉP
                    enemy.Health--;
                    if (enemy.Health == 0)
                    {
                        MessageBox.Show("Congrats, You win!");
                    }
                }
            }

            Timer1.Stop();
        }

        private void Timer_Tick2(object sender, EventArgs e)
        {
            Random rnd = new Random();
            foreach (var item in blast.BlastPosition)
            {
                if (logic.Map[item.x, item.y] == ObstacleType.Blast)
                {
                    logic.Map[item.x, item.y] = ObstacleType.Floor;
                }
                else if(logic.Map[item.x, item.y] == ObstacleType.Explosion)
                {
                    logic.Map[item.x, item.y] = ObstacleType.Player;
                }
                else if (logic.Map[item.x, item.y] == ObstacleType.BlastPowerUp)
                { 
                    int rndnumber = rnd.Next(1,101);
                    if(rndnumber < 15)                    logic.Map[item.x, item.y] = ObstacleType.HealthBoost;
                    else if(rndnumber > 16 && rndnumber < 31)  logic.Map[item.x, item.y] = ObstacleType.SpeedBoost;
                    else if (rndnumber > 32 && rndnumber < 47) logic.Map[item.x, item.y] = ObstacleType.BlastRangeBoost;
                    else logic.Map[item.x, item.y] = ObstacleType.Floor;
                }
                else if (logic.Map[item.x, item.y] == ObstacleType.EPB1)
                {
                    logic.Map[item.x, item.y] = ObstacleType.Enemy1;
                }
                else if (logic.Map[item.x, item.y] == ObstacleType.ExpEnemy)
                {
                    logic.Map[item.x, item.y] = ObstacleType.Enemy1;
                }
            }

            player.BombAmount = 1;  //bugfix
            Timer2.Stop();
        }

    }
}
