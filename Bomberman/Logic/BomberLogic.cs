using Bomberman.MapObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.Logic
{
    public class BomberLogic : IBomberLogic
    {
        public enum ObstacleType  //ÁTÍRNI ENEMY1RE ÉS EPB1 RE!!!!!!
        {
            Bomb, EnemyBomb, Blast, EnemyBlast, Floor, Wall, Player, Enemy1, Box, PowerUp, PPB, EPB1, Explosion, BlastRangeBoost, HealthBoost, SpeedBoost, BlastPowerUp, Enemy2, Enemy3 , EPB2, EPB3, ExpEnemy
        }

        public ObstacleType[,] Map { get; set; } //pálya, ami a gamematrix

        private Queue<string> levels = new Queue<string>();


        public BomberLogic()
        {
            var lvls = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Resources/Levels"));

            foreach (string lvl in lvls)
            {
                levels.Enqueue(lvl);
            }
            LoadLevel(levels.Dequeue());
        }


        public int[] LocatePlayer()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == ObstacleType.Player || Map[i, j] == ObstacleType.PPB || Map[i, j] == ObstacleType.Explosion)
                    {
                        return new int [] { i, j };
                    }
                }
            }
            return new int[] { -1, -1 };
        }

        public int[] LocateEnemy()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == ObstacleType.Enemy1 || Map[i, j] == ObstacleType.EPB1 || Map[i, j] == ObstacleType.ExpEnemy)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return new int[] { -1, -1 };
        }


        public List<(int, int)> LocateBoxes()
        {
            List<(int, int)> locateboxes = new List<(int, int)>();
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == ObstacleType.Box)
                    {
                        locateboxes.Add((i, j));
                    }
                }
            }
            return locateboxes;
        }


        public List<(int, int)> LocateWalls()
        {
            List<(int, int)> locatewalls = new List<(int, int)>();
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == ObstacleType.Wall)
                    {
                        locatewalls.Add((i, j));
                    }
                }
            }
            return locatewalls;
        }

        public List<(int, int)> LocateFloors()
        {
            List<(int, int)> locatefloors = new List<(int, int)>();
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == ObstacleType.Floor)
                    {
                        locatefloors.Add((i, j));
                    }
                }
            }
            return locatefloors;
        }


        public int[] LocateBomb()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == ObstacleType.Bomb)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return new int[] { -1, -1 };
        }

        public int[] LocateEnemyBomb()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == ObstacleType.EnemyBomb)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return new int[] { -1, -1 };
        }


        public int[] LocateLevel()
        {
            return new int[] { Map.GetLength(0), Map.GetLength(1) };
        }


        public int[] LocateExplosion()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == ObstacleType.Explosion)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return new int[] { -1, -1 };
        }

        public int[] LocateExpEnemy()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == ObstacleType.ExpEnemy)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return new int[] { -1, -1 };
        }


        protected void LoadLevel(string path)
        {
            string[] lines = File.ReadAllLines(path);
            Map = new ObstacleType[int.Parse(lines[1]), int.Parse(lines[0])];
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    Map[i, j] = ConvertToEnum(lines[i + 2][j]);
                }
            }
        }


        private ObstacleType ConvertToEnum(char v)
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 101); 

            switch (v)
            {
                case 'W': return ObstacleType.Wall;
                case 'P': return ObstacleType.Player;
                case 'E': return ObstacleType.Enemy1;
                case 'R':
                    if (number < 30)
                    {
                        return ObstacleType.Box;
                    }
                    return ObstacleType.Floor;

                default:
                    return ObstacleType.Floor;
            }
        }
    }
}
