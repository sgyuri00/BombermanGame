using Bomberman.Controller;
using Bomberman.Logic;
using Bomberman.MapObjects;
using Bomberman.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Bomberman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameController controller;
        Timer moveDelay;
        KeyEventArgs lastkey;
        Player player = new Player(0,0);
        Enemy enemy = new Enemy(0, 0);
        public int Difficulty { get; set; }


        public MainWindow(int diffSetup)
        {
            
            this.Difficulty = diffSetup;
            BomberLogic bomberLogic = new BomberLogic();
            PlayerLogic playerLogic = new PlayerLogic(bomberLogic, player, enemy);
            EnemyLogic enemyLogic = new EnemyLogic(bomberLogic, Difficulty, player, enemy);
           
            this.DataContext = new MainWindowViewModel(); //ez az initalizecomponent null ref miatt
            (this.DataContext as MainWindowViewModel).Setup(player);
            InitializeComponent();

            display.SetupLogic(bomberLogic);
            controller = new GameController(playerLogic);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            display.SetupSize(new Size(grid.ActualWidth, grid.ActualHeight));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            display.SetupSize(new Size(grid.ActualWidth, grid.ActualHeight));

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(100);
            dt.Tick += Dt_Tick; ;
            dt.Start();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            this.InvalidateVisual();
            display.InvalidateVisual();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            lastkey = e;
            if (e.Key == Key.Space)
            {
                controller.KeyPressed(e.Key);
            }
            else
            {
                InitMoveDelay(player.Speed);
            }
            display.InvalidateVisual();
            //Thread.Sleep(250);   mozgás lassításának tesztelése
        }

        public void InitMoveDelay(int moveSpeed)
        {
            if (moveSpeed < 900)
            {
                moveDelay = new Timer(moveActivated, null, 1000 - moveSpeed, 0);
            }
            else moveDelay = new Timer(moveActivated, null, 100, 0);
        }

        private void moveActivated(Object stateInfo)
        {
            controller.KeyPressed(lastkey.Key);
        }
    }
}
