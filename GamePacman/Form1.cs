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
        Rectangle selectedRegion;
        Point pointStart;
        bool selected = false;
        public Form1()
        {
            InitializeComponent();

            CursorImage = new CursorImage();
            CursorImage.GameObject = new Wall(0, 0, trackBar1.Value, trackBar1.Value, Color.Red);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);


            field = new Field();
            field.WallSize = trackBar1.Value;
            field.creator = new WallCreator();
            label2.BackColor = Color.Red;
            UpdateStyles();

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            field.Show(e.Graphics);
            e.Graphics.DrawRectangle(new Pen(Color.Black, 1), selectedRegion);
            CursorImage?.Draw(e.Graphics);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (CursorImage.GameObject == null)
            {

                pointStart = new Point(e.X, e.Y);
                selectedRegion = new Rectangle(pointStart, new Size(0, 0));

            }
            else
            {
                field.Add(e.X, e.Y);
                Refresh();
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            label2.BackColor = Color.Blue;
            label3.BackColor = Color.Blue;
            label.BackColor = Color.Red;

            field.creator = new CoinCreator();
            CursorImage.GameObject = new Coin();
            CursorImage.GameObject = new Coin(0, 0, field.CoinsSize, field.CoinsSize);
            Refresh();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label1.BackColor = Color.Blue;
            label3.BackColor = Color.Blue;
            label.BackColor = Color.Red;
            field.creator = new WallCreator();
            CursorImage.GameObject = new Wall(0, 0, field.WallSize, field.WallSize, Color.Red);
            Refresh();
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
            if (CursorImage.GameObject != null)
            {
                CursorImage.GameObject.X = e.X;
                CursorImage.GameObject.Y = e.Y;
            }
            else
            {
                if (e.Button == MouseButtons.Left)
                {
                    var size1 = e.X - pointStart.X > 0 ? e.X - pointStart.X : pointStart.X - e.X;
                    var size2 = e.Y - pointStart.Y > 0 ? e.Y - pointStart.Y : pointStart.Y - e.Y;
                    var x = e.X - pointStart.X > 0 ? pointStart.X : e.X;
                    var y = e.Y - pointStart.Y > 0 ? pointStart.Y : e.Y;

                    selectedRegion = new Rectangle(x, y, size1, size2);
                    selected = true;
                }
            }

            if (e.Y <= field.Height && (field.Width + 4 >= e.X && e.X >= field.Width - 4))
            {

                Cursor.Current = Cursors.SizeWE;

            }
            if (e.X <= field.Width && (field.Height + 4 >= e.Y && e.Y >= field.Height - 4))
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

        private void label3_MouseDown(object sender, MouseEventArgs e)
        {
            Label label = sender as Label;
            label1.BackColor = Color.Blue;
            label2.BackColor = Color.Blue;
            label.BackColor = Color.Red;
            field.creator = null;
            CursorImage.GameObject = null;//TODO: Сделать адекватно
            Refresh();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (CursorImage.GameObject == null)
            {

                selected = false;

                for (var i = 0; i < field.coins.Count; i++)
                {
                    if (selectedRegion.Contains(field.coins[i].X, field.coins[i].Y))
                        field.coins[i].Color = Color.Red;
                    else
                        field.coins[i].Color = Color.Gold;


                } 
                selectedRegion = new Rectangle();
                Refresh();
            }
        }
        private bool Contains(Rectangle rectangle, int x, int y)
        {
            return x >= rectangle.X && x <= rectangle.X + rectangle.Width && y >= rectangle.Y && y <= rectangle.Y + rectangle.Height;
        }
    }
}
