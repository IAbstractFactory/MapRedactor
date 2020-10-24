using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamePacman
{
    public class Field:ICloneable
    {

        Pacman pacman;
        public int WallSize { get; set; } = 10;
        public int CoinsSize { get; private set; } = 5;
        public Color Color { get; private set; }
        public Creator creator { get; set; }
        public List<GameObject> gameObjects { get; private set; }
        public int Width { get; set; } = 500;
        public int Height { get; set; } = 400;
        public Field(Pacman pacman)
        {
            this.pacman = pacman;
            gameObjects = new List<GameObject>();
            Color = Color.Blue;
        }
        public Field()
        {
            gameObjects = new List<GameObject>();
            Color = Color.Blue;

        }
        public void Add(int x, int y)
        {
            gameObjects.Add(creator.Create(x, y, this.creator is WallCreator ? this.WallSize : this.CoinsSize));
            //if (creator is CoinCreator) coins.Add(creator.Create(x, y, 5));
            //if (creator is WallCreator) walls.Add(creator.Create(x, y, WallSize));
        }
        public void Clear()
        {
            gameObjects.Clear();
        }
        public int SelectedCount
        {
            get
            {
                int k = 0;
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    if (gameObjects[i].Selected)
                    {
                        k++;
                    }
                }
                return k;
            }
            private set { }
        }



        public void Show(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color), 0, 0, Width, Height);
            foreach (var obj in gameObjects)
            {
                if (!(obj.X > this.Width || obj.Y > this.Height)) obj.Show(g);
            }

            g.DrawLine(new Pen(Color.Red, 2), 0, 0, Width, 0);
            g.DrawLine(new Pen(Color.Red, 2), 0, 0, 0, Height);
            g.DrawLine(new Pen(Color.Red, 2), 0, Height, Width, Height);
            g.DrawLine(new Pen(Color.Red, 2), Width, 0, Width, Height);
        }

        public object Clone()
        {
            Field field = new Field();
            field.Width = this.Width;
            field.Height = this.Height;
            field.WallSize = this.WallSize;
            field.creator = this.creator;
            field.Color = this.Color;
            for(int i = 0; i < this.gameObjects.Count; i++)
            {
                field.gameObjects.Add(this.gameObjects[i].Clone() as GameObject);
            }

            return field;
        }
    }
}
