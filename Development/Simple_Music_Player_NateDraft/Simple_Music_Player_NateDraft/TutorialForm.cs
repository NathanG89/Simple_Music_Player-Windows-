using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Simple_Music_Player_NateDraft
{
    public partial class TutorialForm : Form
    {
        string title;
        string[] arry;
        public TutorialForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            arry = new string[3];
            clearArry();
            title = "Importing Music: ";
            arry[0] = title;

            string content1 = "\tTo import music, go to the main media player window" +
                " and click on the 'Import' button located at the top left of player" +
                ". Next click the 'Browse' to locate where the music you want to import is and finally click 'OK'";
            arry[2] = content1;

            richTextBox1.Lines = arry;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            arry = new string[3];
            clearArry();
            title = "Exporting Music";
            arry[0] = title;

            string content1 = "\tTo export music to a CD or to another location on your computer" +
                " go to the main media player window and click on the 'Export' button at the top left of the player"
                + ". Next you click 'Browse' to select the location you want to add your selected songs.";
            arry[2] = content1;
            richTextBox1.Lines = arry;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            arry = new string[3];
            clearArry();
            title = "Create A New Playlist";
            arry[0] = title;

            string content1 = "\tTo create a new playlist, navigate to the main media player window" +
                " and click on the 'Add New Playlist' button located at the top right of the player";
            arry[2] = content1;
            richTextBox1.Lines = arry;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            arry = new string[17];
            clearArry();
            title = "Main Music Controls";
            arry[0] = title;

            string play = "Play Button";
            arry[2] = play;

            string playText = "\tTo start the selected music you press the 'Play/Pause' button."
                + " The button is colored green and located in the top left of the main media player window.";
            arry[4] = playText;

            string pause = "Pause Button";
            arry[6] = pause;

            string pauseText = "\tTo pause your music, you press the 'Play/Pause' button." +
                " The button is colored green and located in the top left of the main media player window.";
            arry[8] = pauseText;

            string reverse = "Reverse Button";
            arry[10] = reverse;

            string reverseText = "\tTo rewind your music, you press the '<<Rev' button." +
                " The button is located at the top to the left of the 'Play/Pause' button on the main media player window.";
            arry[12] = reverseText;

            string forward = "Forward Button";
            arry[14] = forward;

            string forwardText = "\tTo fast forward your music, you press the 'Skip>>' button." +
                " The button is located at the top left of the player, to the right of the 'Play/Pause' button on the main media player window.";
            arry[16] = forwardText;

            richTextBox1.Lines = arry;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            arry = new string[3];
            clearArry();
            title = "Sorting Music";
            arry[0] = title;

            string content = "\tTo sort your music you use the controls under the 'Songs/Albums/Playlist' tabs" +
                " To do this you click on the title that you want to sort by (ex clicking on 'artist' will sort the songs by artist alphabetically).";
            arry[2] = content;

            richTextBox1.Lines = arry;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            arry = new string[3];
            clearArry();

            title = "Delete Existing Playlist";
            arry[0] = title;

            string content = "\tTo delete a playlist, you select the playlist you want to delete from your music library." +
                " Then click on the 'Delete Playlist' button located at the top right of the main media player window.";
            arry[2] = content;

            richTextBox1.Lines = arry;
        }

        private void clearArry()
        {
            for (int i = 0; i < arry.Length; i++)
            {
                arry[i] = "";
            }
        }
    }
}
