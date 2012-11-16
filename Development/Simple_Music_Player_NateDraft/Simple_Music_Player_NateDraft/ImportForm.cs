using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using TagLib;

namespace Simple_Music_Player_NateDraft
{
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();
        }
        string path;
        private void cancel(object sender, EventArgs e) // Closes the import form after clicking cancel
        {
            this.Close();
        }

        private void browse(object sender, EventArgs e) // opens the folder browse dialogue box and saves the path of the file to a text box on the import form
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            path = fbd.SelectedPath;
            textBox1.Text = path;
        }

        private void folderBrowserText_Changed(object sender, EventArgs e) // enables the OK button after the text box contains an acceptable file path
        {
            button2.Enabled = true;
        }

        /*
         * Once a file or folder has been selected, the user will click the OK button and the contents
         * of the selected files/folder will be copied to the music player's main music content folder.
         * From there the user may delete the original files/folder or leave them for future use.
         */
        private void OK_Button(object sender, EventArgs e)
        {
            try
            {
                int filenameIndex = path.Length + 1;
                string musicDir = "..\\..\\..\\Music\\";
                string[] filepaths = Directory.GetFiles(path, "*.wav", SearchOption.AllDirectories);
                //string[] musicDirFiles = null;
                //musicDirFiles = Directory.GetFiles(musicDir, "*.wav", SearchOption.AllDirectories);
                //int i = 0;
                foreach (string song in filepaths)
                {
                    try
                    {
                        string copyToPath = musicDir + song.Substring(filenameIndex);
                        File.Copy(song, copyToPath);
                    }
                    catch (IOException ioe)
                    { 
                    }
                    //i++;
                }
                MessageBox.Show("Import Successful", "Import Status", MessageBoxButtons.OK);
                textBox1.Text = "";
                button2.Enabled = false;
            }
            catch (DirectoryNotFoundException dnfe)
            {
                MessageBox.Show(dnfe.Message);
            }
        }
    }
}
