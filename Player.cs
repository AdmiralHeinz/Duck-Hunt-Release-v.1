//Aidan, Jakob, Peter, Austin
//June 4, 2018
//Duck Hunt
//A recreation of the game Duck HuntPlayer
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
    class Player
    {
        //peters stuff
        MediaPlayer playermusicPlayer = new MediaPlayer();// initalise the media player
        MouseButtonState Previousmbs = MouseButtonState.Released;// sets the previous mouse state
        public bool MouseClicked()//run if mouse clicked
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && Previousmbs == MouseButtonState.Released)// determines if the mouse was clicked
            {
                Previousmbs = Mouse.LeftButton;
                playermusicPlayer.Open(new Uri("ShotgunSound.mp3", UriKind.Relative));
                playermusicPlayer.Play();
                return true;
                
            }
            else
            {
                Previousmbs = Mouse.LeftButton;
                return false;
            }

        }

    }
}