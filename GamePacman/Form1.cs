using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamePacman
{
    public partial class Form1 : Form
    {
        CursorImage CursorImage;
        Field field;


        public Form1()
        {
            CursorImage = new CursorImage();


            InitializeComponent();
            CursorImage.GameObject = new Wall(0, 0, trackBar1.Value, trackBar1.Value, Color.Red);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);


            field = new Field();
            field.WallSize = trackBar1.Value;
            UpdateStyles();

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            var graphics = e.Graphics;
            field.Show(e.Graphics);
            CursorImage?.Draw(e.Graphics);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!(e.X > field.Width - field.WallSize / 2 || e.Y > field.Height - field.WallSize / 2))
            {
                field.Add(e.X, e.Y);
                Refresh();
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            label2.BackColor = Color.Blue;
            label.BackColor = Color.Red;

            field.creator = new CoinCreator();
            CursorImage.GameObject = new Coin();
            CursorImage.GameObject = new Coin(0, 0, field.CoinsSize, field.CoinsSize);

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label1.BackColor = Color.Blue;
            label.BackColor = Color.Red;
            field.creator = new WallCreator();
            CursorImage.GameObject = new Wall(0, 0, field.WallSize, field.WallSize, Color.Red);
        }



        private void button1_MouseDown(object sender, MouseEventArgs e)
        {

            Settings settings = new Settings();
            settings.coins = field.coins;
            settings.walls = field.walls;
            settings.fildHeight = field.Height;
            settings.fildWidt = field.Width;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "settings";
            saveFileDialog.DefaultExt = ".dat";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                string filename = saveFileDialog.FileName;

                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    binaryFormatter.Serialize(stream, settings);
                    MessageBox.Show("Сохранено");
                }

            }


        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            TrackBar trackBar = sender as TrackBar;
            field.WallSize = trackBar.Value;
            if (CursorImage.GameObject is Wall)
            {
                CursorImage.GameObject.Height = field.WallSize;
                CursorImage.GameObject.Width = field.WallSize;
            }
            Refresh();
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            field.Clear();
            Refresh();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            CursorImage.GameObject.X = e.X;
            CursorImage.GameObject.Y = e.Y;
            if (e.Y <= Height && (field.Width + 4 >= e.X && e.X >= field.Width - 4))
            {
                Cursor.Current = Cursors.SizeWE;

            }
            else if (e.Y <= Width && (field.Height + 4 >= e.Y && e.Y >= field.Height - 4))
            {
                Cursor.Current = Cursors.SizeNS;

            }
            if (e.Button == MouseButtons.Left && Cursor.Current == Cursors.SizeWE)
            {

                field.Width = e.X;

            }
            if (e.Button == MouseButtons.Left && Cursor.Current == Cursors.SizeNS)
            {

                field.Height = e.Y;

            }
            Refresh();
        }
    }
}
