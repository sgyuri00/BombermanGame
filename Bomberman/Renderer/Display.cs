using Bomberman.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Bomberman.Renderer
{
    public class Display : FrameworkElement
    {
        IBomberLogic logic;
        Size size;

        public void SetupSize(Size size)
        {
            this.size = size;
            this.InvalidateVisual();
        }

        public void SetupLogic(IBomberLogic logic)
        {
            this.logic = logic;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            BitmapImage box = new BitmapImage();
            BitmapImage wall = new BitmapImage();
            BitmapImage floor = new BitmapImage();
            BitmapImage player = new BitmapImage();
            BitmapImage bomb = new BitmapImage();
            BitmapImage ppb = new BitmapImage(); //player plus bomb
            
            BitmapImage blast = new BitmapImage();
            BitmapImage speedPU = new BitmapImage();
            BitmapImage rangePU = new BitmapImage();
            BitmapImage healthkitPU = new BitmapImage();

            BitmapImage enemy1 = new BitmapImage();
            BitmapImage epb1 = new BitmapImage(); //enemy1 plus bomb --> enemy was damaged
            BitmapImage enemyexp = new BitmapImage(); //enemy dmgd state
            BitmapImage enemyblast = new BitmapImage();
            BitmapImage enemybomb = new BitmapImage();

            BitmapImage enemy2 = new BitmapImage();
            BitmapImage epb2 = new BitmapImage(); //enemy2 plus bomb --> enemy was damaged

            BitmapImage enemy3 = new BitmapImage();
            BitmapImage epb3 = new BitmapImage(); //enemy3 plus bomb --> enemy was damaged
            BitmapImage exp = new BitmapImage();

            exp.BeginInit();
            exp.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.player_dead.png");
            exp.EndInit();

            box.BeginInit();
            box.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.wood.png");
            box.EndInit();

            wall.BeginInit();
            wall.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.wall.png");
            wall.EndInit();

            floor.BeginInit();
            floor.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.floor.png");
            floor.EndInit();

            player.BeginInit();
            player.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.player.png");
            player.EndInit();

            bomb.BeginInit();
            bomb.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.bomb.png");
            bomb.EndInit();

            ppb.BeginInit();
            ppb.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.PPB.png");
            ppb.EndInit();

            blast.BeginInit();
            blast.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.blast.png");
            blast.EndInit();

            speedPU.BeginInit();
            speedPU.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.speedPU.png");
            speedPU.EndInit();

            rangePU.BeginInit();
            rangePU.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.rangePU.png");
            rangePU.EndInit();

            healthkitPU.BeginInit();
            healthkitPU.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.healthkitPU.png");
            healthkitPU.EndInit();

            enemy1.BeginInit();
            enemy1.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.enemy1.png");
            enemy1.EndInit();

            epb1.BeginInit();
            epb1.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.EPB1.png");
            epb1.EndInit();

            enemyexp.BeginInit();
            enemyexp.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.enemy1_dead.png");
            enemyexp.EndInit();

            enemy2.BeginInit();
            enemy2.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.enemy2.png");
            enemy2.EndInit();

            epb2.BeginInit();
            epb2.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.EPB2.png");
            epb2.EndInit();
            
            enemy3.BeginInit();
            enemy3.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.enemy3.png");
            enemy3.EndInit();

            epb3.BeginInit();
            epb3.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.EPB3.png");
            epb3.EndInit();

            enemyblast.BeginInit();
            enemyblast.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.enemy_blast.png");
            enemyblast.EndInit();

            enemybomb.BeginInit();
            enemybomb.StreamSource = Assembly.GetExecutingAssembly().GetManifestResourceStream("Bomberman.Resources.Images.bomb.png");
            enemybomb.EndInit();

            if (logic != null)
            {
                //15sor 21oszlop
                double rectWidth = size.Width / logic.Map.GetLength(1);
                double rectHeight = size.Height / logic.Map.GetLength(0);

                for (int i = 0; i < logic.Map.GetLength(0); i++)
                {
                    for (int j = 0; j < logic.Map.GetLength(1); j++)
                    {
                        BitmapImage brush = new BitmapImage();
                        switch (logic.Map[i, j])
                        {
                            case BomberLogic.ObstacleType.Floor:
                                brush = floor;
                                break;

                            case BomberLogic.ObstacleType.Wall:
                                brush = wall;
                                break;

                            case BomberLogic.ObstacleType.Player:
                                brush = player;
                                break;

                            case BomberLogic.ObstacleType.PPB:
                                brush = ppb;
                                break;

                            case BomberLogic.ObstacleType.Box:
                                brush = box;
                                break;

                            case BomberLogic.ObstacleType.Bomb:
                                brush = bomb;
                                break;

                            case BomberLogic.ObstacleType.Blast:
                                brush = blast;
                                break;

                            case BomberLogic.ObstacleType.BlastRangeBoost:
                                brush = rangePU;
                                break;

                            case BomberLogic.ObstacleType.SpeedBoost:
                                brush = speedPU;
                                break;

                            case BomberLogic.ObstacleType.HealthBoost:
                                brush = healthkitPU;
                                break;

                            case BomberLogic.ObstacleType.BlastPowerUp:
                                brush = blast;
                                break;

                            case BomberLogic.ObstacleType.Enemy1:
                                brush = enemy1;
                                break;

                            case BomberLogic.ObstacleType.Enemy2:
                                brush = enemy2;
                                break;

                            case BomberLogic.ObstacleType.Enemy3:
                                brush = enemy3;
                                break;

                            case BomberLogic.ObstacleType.EPB1:
                                brush = epb1;
                                break;

                            case BomberLogic.ObstacleType.EPB2:
                                brush = epb2;
                                break;

                            case BomberLogic.ObstacleType.EPB3:
                                brush = epb3;
                                break;

                            case BomberLogic.ObstacleType.ExpEnemy:
                                brush = enemyexp;
                                break;

                            case BomberLogic.ObstacleType.EnemyBlast:
                                brush = enemyblast;
                                break;

                            case BomberLogic.ObstacleType.EnemyBomb:
                                brush = enemybomb;
                                break;

                            case BomberLogic.ObstacleType.Explosion:
                                brush = exp;
                                break;

                            default:
                                break;
                        }

                        ImageBrush ib = new ImageBrush();
                        ib.ImageSource = brush;
                        drawingContext.DrawRectangle(ib, new Pen(Brushes.Black, 0), new Rect(j*rectWidth, i*rectHeight, rectWidth, rectHeight));
                    }
                }
            }
            
        }
    }
}
