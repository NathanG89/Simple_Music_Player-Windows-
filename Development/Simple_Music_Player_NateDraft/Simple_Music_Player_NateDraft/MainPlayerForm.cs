using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Simple_Music_Player_NateDraft
{
    public partial class MainPlayerForm : Form
    {
        public MainPlayerForm()
        {
            InitializeComponent();
        }
        string musicDir = "..\\..\\..\\Music\\"; //default music directory string
        string[] currMusicDirFiles = null; //songs currently stored in music directory
        private NAudio.Wave.BlockAlignReductionStream stream = null; //block align reduction stream variable for playing mp3 or other audio files
        private NAudio.Wave.DirectSoundOut output = null; //variable that takes audio stream and outputs to available audio output system
        //string stopButtonState = "Stop"; 
        private void play_pause(object sender, EventArgs e)
        {
            if (output == null)
            {
                OpenFileDialog open = new OpenFileDialog();
                open.InitialDirectory = musicDir;
                open.Filter = "Audio File (*.mp3;*.wav)|*.mp3;*.wav;";
                if (open.ShowDialog() != DialogResult.OK) return;

                DisposeWave();

                if (open.FileName.EndsWith(".mp3"))
                {
                    NAudio.Wave.WaveStream pcm = NAudio.Wave.WaveFormatConversionStream.CreatePcmStream(new NAudio.Wave.Mp3FileReader(open.FileName));
                    stream = new NAudio.Wave.BlockAlignReductionStream(pcm);
                }
                else if (open.FileName.EndsWith(".wav"))
                {
                    NAudio.Wave.WaveStream pcm = new NAudio.Wave.WaveChannel32(new NAudio.Wave.WaveFileReader(open.FileName));
                    stream = new NAudio.Wave.BlockAlignReductionStream(pcm);
                }
                else throw new InvalidOperationException("Not a correct audio file type.");

                output = new NAudio.Wave.DirectSoundOut();
                output.Init(stream);
                output.Play();
            }
            else if (output != null)
            {
                if (output.PlaybackState == NAudio.Wave.PlaybackState.Playing) output.Pause();
                else if (output.PlaybackState == NAudio.Wave.PlaybackState.Paused) output.Play();
            }
        }
        private void stop(object sender, EventArgs e)
        {
            DisposeWave();
        }
        private void closingProtocol(object sender, FormClosingEventArgs e)
        {
            DisposeWave();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            ImportForm imform = new ImportForm();
            imform.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ExportForm exform = new ExportForm();
            exform.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TutorialForm tutform = new TutorialForm();
            tutform.Show();
        }

        private void MainPlayerForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'musicPlayerDatabaseDataSet.song' table. You can move, or remove it, as needed.
            this.songTableAdapter.Fill(this.musicPlayerDatabaseDataSet.song);
            this.MinimumSize = new System.Drawing.Size(600, 615);
            this.Size = new System.Drawing.Size(600, 615);
            button7.Location = new Point(button6.Location.X,button6.Location.Y);
            label5.Location = new Point(label11.Location.X, label11.Location.Y);
            currMusicDirFiles = Directory.GetFiles(musicDir, "*.wav", SearchOption.AllDirectories);
            for (int i = 0; i < songDataGridView1.Rows.Count; i++)
            {
                songList.Items.Add(songDataGridView1.Rows[i].Cells[1].Value);
            }
        }

        private void DisposeWave()
        {
            if (output != null)
            {
                if (output.PlaybackState == NAudio.Wave.PlaybackState.Playing) output.Stop();
                output.Dispose();
                output = null;
            }
            if (stream != null)
            {
                stream.Dispose();
                stream = null;
            }
        }

        private void databaseUpdate(object sender, EventArgs e)
        {
            string[] newMusicDirFiles = Directory.GetFiles(musicDir, "*.wav", SearchOption.AllDirectories);
            foreach (string song in newMusicDirFiles)
            {
                bool updateNeeded = true;
                foreach (string oldsong in currMusicDirFiles)
                {
                    int result = String.Compare(song, oldsong);
                    if (result == 0)
                    {
                        updateNeeded = false;
                        continue;
                    }
                }
                if (updateNeeded == true)
                {
                    int songfileIndex = song.Length + 1;
                    string songfileName = song.Substring(songfileIndex);
                    songDataGridView2.Rows.Add();
                    int lastrow = songDataGridView2.Rows.GetLastRow(DataGridViewElementStates.None);
                    songDataGridView2.Rows[lastrow].Cells[2].Value = songfileName;
                    tableAdapterManager1.UpdateAll(musicPlayerDatabaseDataSet);
                }
            }
        }

        private void advancedLibrarySearch(object sender, KeyEventArgs e)
        {
            if (Keys.Enter == e.KeyCode)
            {
                try
                {
                    if (textBox1.Text == "")
                    {
                        songBindingSource.Filter = textBox1.Text;
                    }
                    else if (comboBox1.Text == "(Search Category)")
                    {
                        songDataGridView2.Focus();
                        songBindingSource.Filter = "name like '%" + textBox1.Text +
                            "%' OR artist like '%" + textBox1.Text +
                            "%' OR genre like '%" + textBox1.Text +
                            "%' OR album like '%" + textBox1.Text + "%'";
                    }
                    else
                    {
                        songDataGridView2.Focus();
                        songBindingSource.Filter = comboBox1.Text + " like '%" + textBox1.Text + "%'";
                    }
                }
                catch (Exception exc)
                {
                }
            }
        }

        private void noviceSongSearch(object sender, EventArgs e)
        {
            try
            {
                int search = songList.FindString(textBox2.Text);
                songList.SelectedIndex = search;
            }
            catch (ArgumentOutOfRangeException aofre)
            {
            }
        }

        private void songDataDataGridView_AlbumCellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void songDataBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {

        }

        public string currMode = "novice";

        private void UIButton(object sender, EventArgs e)
        {
            if (currMode == "advanced")
            {
                int i = 0;
                object list = null;
                tabControl1.Visible = false;
                label4.Visible = false;
                comboBox1.Visible = false;
                textBox1.Visible = false;
                button8.Visible = false;
                button9.Visible = false;
                button10.Visible = false;
                label12.Visible = false;
                label13.Visible = false;
                label14.Visible = false;
                searchResultslbl.Visible = false;
                //button5.Visible = false;
                button6.Visible = false;
                //label10.Visible = false;
                label11.Visible = false;
                textBox2.Visible = true;
                label16.Visible = true;
                for(i = 0; i < songDataGridView1.Rows.Count; i++)
                {
                    list = songDataGridView1.Rows[i].Cells[1].Value;
                    songList.Items.Add(list);
                }
                currMode = "novice";
                button11.Text = "Advanced";
                label15.Text = "Click to switch \nto advanced \ninterface";
                label15.Location = new Point(194, 108);
                button7.Location = new Point(button6.Location.X,button6.Location.Y);
                label5.Location = new Point(label11.Location.X, label11.Location.Y);
                this.MinimumSize = new System.Drawing.Size(600, 615);
                this.Size = new System.Drawing.Size(600, 615);
            }
            else if (currMode == "novice")
            {
                tabControl1.Visible = true;
                label4.Visible = true;
                comboBox1.Visible = true;
                textBox1.Visible = true;
                button8.Visible = true;
                button9.Visible = true;
                button10.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
                label14.Visible = true;
                searchResultslbl.Visible = true;
                //button5.Visible = true;
                button6.Visible = true;
                //label10.Visible = true;
                label11.Visible = true;
                textBox2.Visible = false;
                label16.Visible = false;
                songList.Items.Clear();
                currMode = "advanced";
                button11.Text = "Novice";
                label15.Text = "Click to switch \nto novice interface";
                label15.Location = new Point (180,108);
                button7.Location = new Point(415, 45);
                label5.Location = new Point(412, 15);
                this.MinimumSize = new System.Drawing.Size(1175, 615);
                this.Size = new System.Drawing.Size(1175, 615);
            }
        }

        private void songTabDatabase(object sender, EventArgs e)
        {
            songBindingSource.Filter = "";
        }

        private void populateAlbumList(object sender, EventArgs e)
        {
            int i = 0;
            for (i = 0; i < songDataGridView1.Rows.Count; i++)
            {
                if(!checkedListBox1.Items.Contains(songDataGridView1.Rows[i].Cells[3].Value))
                    checkedListBox1.Items.Add(songDataGridView1.Rows[i].Cells[3].Value);
            }
        }

        /*private void albumTabDatabase(object sender, EventArgs e)
        {
            string defaultFilter = "";
            string filter = "Album like '%";
            if (checkedListBox1.SelectedItem != null)
            {
                filter += Convert.ToString(checkedListBox1.SelectedItem) + "%'";
                songBindingSource.Filter = filter;
            }
            else
            {
                songBindingSource.Filter = defaultFilter;
            }
        }*/

        private void albumListSelect(object sender, EventArgs e)
        {
            string filter = "Album like '%";
            filter += Convert.ToString(checkedListBox1.SelectedItem) + "%'";
            songBindingSource.Filter = filter;
        }

/*        private void playlistTabDatabase(object sender, EventArgs e)
        {
            string defaultFilter = "";
            string filter = "Album like '%";
            if (checkedListBox2.SelectedItem != null)
            {
                filter += Convert.ToString(checkedListBox2.SelectedItem) + "%'";
                songBindingSource.Filter = filter;
            }
            else
            {
                songBindingSource.Filter = defaultFilter;
            }
        }*/

        private void playlistListSelect(object sender, EventArgs e)
        {
            string filter = "Album like '%";
            filter += Convert.ToString(checkedListBox2.SelectedItem) + "%'";
            songBindingSource.Filter = filter;
        }

        private void songListPoplate(object sender, EventArgs e)
        {
            albumNamelbl.Text = Convert.ToString(checkedListBox1.SelectedItem);
        }

        private void songBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.songBindingSource.EndEdit();
            this.tableAdapterManager1.UpdateAll(this.musicPlayerDatabaseDataSet);

        }
    }
}
