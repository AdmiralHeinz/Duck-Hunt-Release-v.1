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
    class Duck
    {
        // adien did most of this
        public Rectangle duck;
        MediaPlayer duckmusicPlayer = new MediaPlayer();// initalise the media player
        Random random = new Random();
        public int pos_x = 950 / 2;//x pos of the duck
        public int pos_y = 599;//y pos of the duck
        public int counter = 0;//counter
        public int speed;//speed of duck
        public bool movingLeft = true;//direction
        public bool movingUp = true;//direction
        public bool isDuck = false;//does a duck exist
        public int shots;//# of shots taken
        public int DucksKilled;//# of ducks killed

        public void Spawn(Canvas canvas)//spawns the duck
        {
            //jakbo did this
            duck = new Rectangle();//creates rectangle that is duck
            duck.Height = 100;//sets the size
            duck.Width = 100;
            
            //asigns a random speed
            speed = random.Next(8, 15);
            
            //assigns a starting direction
            if (speed % 2 == 0) { movingLeft = true; }
            if (speed % 2 != 0) { movingLeft = false; }

            canvas.Children.Add(duck);//adds the duck
            pos_x = 950 / 2;
            pos_y = 599;
            isDuck = true;//there is a duck on the screen

            shots = 0;// there are no shots taken on this duck
        }

        public void Move(int counter)// moves the duck
        {
            //moves it per tick
            //checks if it has hit an edge and defines a new direction if it has
            if (pos_x <= 1)
            {
                movingLeft = true;
            }
            else if (pos_x >= 949)
            {
                movingLeft = false;
            }
            if (pos_y <= 1)
            {
                movingUp = true;
            }
            else if (pos_y >= 599)
            {
                movingUp = false;
            }
            
            //sends a change direction command
            if (movingLeft == false & movingUp == true)
            {
                ChangeDirection(0);
            }
            else if (movingLeft == true & movingUp == true)
            {
                ChangeDirection(1);
            }
            else if (movingLeft == false & movingUp == false)
            {
                ChangeDirection(2);
            }
            else if (movingLeft == true & movingUp == false)
            {
                ChangeDirection(3);
            }

            //moves the duck
            if (movingLeft == true)
            {
                pos_x = pos_x + speed;
                Canvas.SetLeft(duck, pos_x);
            }
            else if (movingLeft == false)
            {
                pos_x = pos_x - speed;
                Canvas.SetLeft(duck, pos_x);
            }
            if (movingUp)
            {
                pos_y = pos_y + speed;
                Canvas.SetTop(duck, pos_y);
            }
            else
            {
                pos_y = pos_y - speed;
                Canvas.SetTop(duck, pos_y);
            }
            RandomChangeDirection();
        }

        public void Kill(double Shot_x, double Shot_Y, Canvas canvas, int counter)//if the shot hits the duck kill it
        {
            if (Shot_x >= pos_x & Shot_x <= pos_x + 100 & Shot_Y >= pos_y & Shot_Y <= pos_y + 100)//checks if the duck is hit
            {
                //jakob edited and fixed this
                canvas.Children.Remove(duck);//removes the duck
                isDuck = false;//there is no duck
                DucksKilled += 1;//more death
                duckmusicPlayer.Open(new Uri("Hit.mp3", UriKind.Relative));
                duckmusicPlayer.Play();
            }
            else
            {
                shots += 1;//no death and shots missed is more
            }
        }

        public void RandomChangeDirection()// bounces the duck off of the sides of the screen
        {
            int change = random.Next(0, 500); //generates a random number
            if (change >= 490)//chance to change direction
            {
                //defines a random direction based on whether the triggering number
                if (change % 2 == 0)
                {
                    //changes the horizontal direction if even
                    if (movingLeft) { movingLeft = false; }
                    else { movingLeft = true; }
                }
                else
                {
                    //chnages the vertical direction if odd
                    if (movingUp) { movingUp = false; }
                    else { movingUp = true; }
                }
                //MessageBox.Show("Random Change!");
            }
        }

        public void ChangeDirection(int direction)// changes the graphic to reflect the ducks direction of travel
        {
            //peter did image change stuff
            if (direction == 0)
            {
                //duck.Fill = //duck facing NE
                BitmapImage bitmapImage = new BitmapImage(new Uri("DuckDownLeft.png", UriKind.Relative));
                ImageBrush img = new ImageBrush(bitmapImage);
                duck.Fill = img;
            }
            else if (direction == 1)
            {
                //duck.Fill = //duck facing NW
                BitmapImage bitmapImage = new BitmapImage(new Uri("DuckDownRight.png", UriKind.Relative));
                ImageBrush img = new ImageBrush(bitmapImage);
                duck.Fill = img;
            }
            else if (direction == 2)
            {
                //duck.Fill = //duck facing SE
                BitmapImage bitmapImage = new BitmapImage(new Uri("DuckUpLeft.png", UriKind.Relative));
                ImageBrush img = new ImageBrush(bitmapImage);
                duck.Fill = img;
            }
            else if (direction == 3)
            {
                //duck.Fill = //duck facing SW
                BitmapImage bitmapImage = new BitmapImage(new Uri("DuckUpRight.png", UriKind.Relative));
                ImageBrush img = new ImageBrush(bitmapImage);
                duck.Fill = img;
            }
        }
    }
}