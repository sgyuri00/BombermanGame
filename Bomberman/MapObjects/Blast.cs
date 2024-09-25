using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.MapObjects
{
    public class Blast : MapObject
    {
        public int BlastRange { get; set; }

        public List<(int x, int y)> BlastPosition { get; set; }
        public Blast(int x, int y) : base(x, y)
        {
            this.BlastRange = 1;
            this.BlastPosition = new List<(int x, int y)>();
        }

        public void CalculateRange(int x, int y, List<(int x, int y)>  locateBoxes, List<(int x, int y)> locateWalls, int[] locateLevel, int[] locateBomb, int[] locateEnemyBomb)
        {
            BlastPosition = new List<(int x, int y)>();
            BlastPosition.Add((x, y));

            for (int i = 1; i <= BlastRange ; i++)
            {
                if (x+i<locateLevel[0] && y < locateLevel[1] && x + i > 0 && y > 0)
                {
                    if (locateWalls.Contains((x + i, y)))
                    {
                        break;
                    }
                    BlastPosition.Add((x + i, y));
                    if (locateBoxes.Contains((x + i, y)))
                    {
                        break;
                    }
                }
            }
            for (int i = 1; i <= BlastRange; i++)
            {
                if (x < locateLevel[0] && y + i < locateLevel[1] && x > 0 && y + i > 0)
                {
                    if (locateWalls.Contains((x, y + i)))
                    {
                        break;
                    }
                    BlastPosition.Add((x, y + i));
                    if (locateBoxes.Contains((x, y + i)))
                    {
                        break;
                    }
                }
            }
            for (int i = 1; i <= BlastRange; i++)
            {
                if (x - i < locateLevel[0] && y < locateLevel[1] && x - i > 0 && y > 0)
                {
                    if (locateWalls.Contains((x - i, y)))
                    {
                        break;
                    }
                    BlastPosition.Add((x - i, y));
                    if (locateBoxes.Contains((x - i, y)))
                    {
                        break;
                    }
                }
            }
            for (int i = 1; i <= BlastRange; i++)
            {
                if (x < locateLevel[0] && y - i < locateLevel[1] && x > 0 && y - i > 0)
                {
                    if (locateWalls.Contains((x, y - i)))
                    {
                        break;
                    }
                    BlastPosition.Add((x, y - i));
                    if (locateBoxes.Contains((x, y - i)))
                    {
                        break;
                    }
                }
            }
        }   
    }
}
