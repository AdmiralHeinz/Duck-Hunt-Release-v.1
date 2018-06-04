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
using System.IO;

namespace Duck_Hunt_2._0
{
    class Background
    {
        public Cursor crossHair;
        public Rectangle bulletDisplay;
        public Label scorebox;
        Rectangle background = new Rectangle();
        public Rectangle livesDisplay;

        public Background(Canvas canvas)
        {
            // aidens stuff
            FileStream fileStream;//set cursor
            fileStream = new FileStream("Crosshair.cur", FileMode.Open);
            crossHair = new Cursor(fileStream);

            //set the background to the splash
            background.Height = 800;
            background.Width = 1000;
            BitmapImage bitmapSplash = new BitmapImage(new Uri("Splash.png", UriKind.Relative));
            ImageBrush splashBrush = new ImageBrush(bitmapSplash);
            background.Fill = splashBrush;
            canvas.Children.Add(background);
        }

        public void Start(Canvas canvas)
        {
            //jakobs stuff
            canvas.Children.Remove(scorebox);

            //set the background to the game background
            //based on austins code
            BitmapImage bitmapBackground = new BitmapImage(new Uri("Background.png", UriKind.Relative));
            ImageBrush backgroundBrush = new ImageBrush(bitmapBackground);
            background.Fill = backgroundBrush;


            //adds the bullet display
            bulletDisplay = new Rectangle();
            bulletDisplay.Height = 100;
            bulletDisplay.Width = 160;
            Canvas.SetBottom(bulletDisplay, 10);
            canvas.Children.Add(bulletDisplay);

            //creating displays for lives
            livesDisplay = new Rectangle();
            livesDisplay.Height = 100;
            livesDisplay.Width = 200;
            Canvas.SetBottom(livesDisplay, -5);
            Canvas.SetLeft(livesDisplay, 300);
            canvas.Children.Add(livesDisplay);

            //adds the score label
            scorebox = new Label();
            scorebox.Width = 1000;
            scorebox.Height = 160;
            Canvas.SetBottom(scorebox, -70);
            Canvas.SetLeft(scorebox, 600);
            scorebox.FontSize = 50;
            //scorebox.FontStyle;
            canvas.Children.Add(scorebox);

            canvas.UpdateLayout();//updates the layout
        }

        public void GameOver(Canvas canvas)
        {
            //aidens stuff
            //sets the backgroung to the gmae over screen

            Canvas.SetBottom(scorebox, 100);
            Canvas.SetLeft(scorebox, 300);
            scorebox.FontSize = 80;

            BitmapImage bitmapGameover = new BitmapImage(new Uri("Gameover.png", UriKind.Relative));
            ImageBrush GameoverBrush = new ImageBrush(bitmapGameover);
            background.Fill = GameoverBrush;
            
            canvas.Children.Remove(livesDisplay);
            canvas.Children.Remove(bulletDisplay);

        }
    }
}