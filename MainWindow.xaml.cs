//Aidan, Jakob, Peter, Austin
//June 4, 2018
//Duck Hunt
//A recreation of the game Duck Hunt
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Duck_Hunt_2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Duck duck = new Duck();//initalize the duck
        Player player = new Player();//initalize the player
        MediaPlayer musicPlayer = new MediaPlayer();// initalise the media player
        DispatcherTimer gameTimer = new DispatcherTimer();//initalize the timer
        Background background;// initalize the background
        int counter = 0;// initalize e counter
        int Lives = 3;//# of lives
        double Shot_X;//x pos of shot taken
        double Shot_Y;//y pos of shot taken
        bool gameOn;// set the game state

        public MainWindow()
        {
            InitializeComponent();

            background = new Background(Canvas);// initalize background class

            this.Cursor = background.crossHair;//set crosshair by peter
            this.ForceCursor = true;

            gameTimer.Tick += GameTimer_Tick;//gametimer 
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60);//fps
            gameTimer.Start();// start the game

            //start splash music by jakob
            musicPlayer.Open(new Uri("Duck Hunt Intro.mp3", UriKind.Relative));
            musicPlayer.Play();

        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (gameOn)//begin the game
            {
                if (duck.isDuck == false)
                {
                    duck.Spawn(Canvas);
                }//spawn duck if no duck exists

                if (player.MouseClicked())//if the mouse is clicked
                {
                    Shot_X = Mouse.GetPosition(this).X + 25;//get the position and then adjust it for the size of the duck
                    Shot_Y = Mouse.GetPosition(this).Y + 25;
                    duck.Kill(Shot_X, Shot_Y, Canvas, counter);
                }

                //change shots remaining graphic by jakob
                if (duck.shots == 0)
                {
                    BitmapImage ThreeShots = new BitmapImage(new Uri("Three Shots.png", UriKind.Relative));
                    ImageBrush Three = new ImageBrush(ThreeShots);
                    background.bulletDisplay.Fill = Three;
                }
                if (duck.shots == 1)
                {
                    if (duck.isDuck == false)
                    {
                        duck.shots = 0;
                    }
                    BitmapImage TwoShots = new BitmapImage(new Uri("Two Shots.png", UriKind.Relative));
                    ImageBrush Two = new ImageBrush(TwoShots);
                    background.bulletDisplay.Fill = Two;
                }
                if (duck.shots == 2)
                {
                    if (duck.isDuck == false)
                    {
                        duck.shots = 0;
                    }
                    BitmapImage OneShot = new BitmapImage(new Uri("One Shot.png", UriKind.Relative));
                    ImageBrush One = new ImageBrush(OneShot);
                    background.bulletDisplay.Fill = One;
                }
                if (duck.shots == 3)
                {
                    BitmapImage NoShot = new BitmapImage(new Uri("No Shots.png", UriKind.Relative));
                    ImageBrush None = new ImageBrush(NoShot);
                    background.bulletDisplay.Fill = None;

                    Lives--;
                    if (Lives == 0)
                    {
                        duck.isDuck = false;
                        Canvas.Children.Remove(duck.duck);
                        background.GameOver(Canvas);
                        //MessageBox.Show("GameOver");
                        gameOn = false;

                        musicPlayer.Open(new Uri("Game Over.mp3", UriKind.Relative));
                        musicPlayer.Play();

                        Lives = 3;
                        duck.shots = 0;
                    }
                    else if (Lives > 0)
                    {
                        //MessageBox.Show("out of ammo");
                        Canvas.Children.Remove(duck.duck);
                        duck.isDuck = false;
                        musicPlayer.Open(new Uri("Miss.mp3", UriKind.Relative));
                        musicPlayer.Play();
                    }
                }

                //change lives graphics by jakob
                if (Lives == 3) ///change Lives remaining graphic
                {
                    BitmapImage ThreeLives = new BitmapImage(new Uri("Three Hearts.png", UriKind.Relative));
                    ImageBrush ThreeHearts = new ImageBrush(ThreeLives);
                    background.livesDisplay.Fill = ThreeHearts;

                }
                if (Lives == 2)
                {
                    BitmapImage TwoLives = new BitmapImage(new Uri("Two Hearts.png", UriKind.Relative));
                    ImageBrush TwoHearts = new ImageBrush(TwoLives);
                    background.livesDisplay.Fill = TwoHearts;
                }
                if (Lives == 1)
                {
                    BitmapImage OneLives = new BitmapImage(new Uri("One Heart.png", UriKind.Relative));
                    ImageBrush OneHearts = new ImageBrush(OneLives);
                    background.livesDisplay.Fill = OneHearts;
                }
                if (Lives == 0)
                {
                    BitmapImage NoShot = new BitmapImage(new Uri("No Lives.png", UriKind.Relative));
                    ImageBrush None = new ImageBrush(NoShot);
                    background.bulletDisplay.Fill = None;
                }

                duck.Move(counter);//update duck position

                background.scorebox.Content = "Score: " + (duck.DucksKilled * 100).ToString();// change score shown
            }
            counter++;//update counter
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {//by aiden

            if (Keyboard.IsKeyDown(Key.Enter))
            {
                if (gameOn == false)
                {
                    background.Start(Canvas);
                    gameOn = true;
                    duck.DucksKilled = 0;
                }
            }
        }//control button click for splash screen
    }
}