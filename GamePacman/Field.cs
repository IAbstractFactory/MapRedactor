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
    public class Field
    {

        Pacman pacman;
        public int WallSize { get; set; } = 10;
        public int CoinsSize { get; private set; } = 5;
        public Creator creator { get; set; }
        public List<GameObject> coins { get; private set; }
        public List<GameObject> walls { get; private set; }
        public List<GameObject> gameObjects { get; private set; }
        public int Width { get; set; } = 500;
        public int Height { get; set; } = 400;
        public Field(Pacman pacman)
        {
            this.pacman = pacman;
            walls = new List<GameObject>();
            coins = new List<GameObject>();
        }
        public Field()
        {
            walls = new List<GameObject>();
            coins = new List<GameObject>();

        }
        public void Add(int x, int y)
        {
            //walls.Add(creator.Create(x, y,c);
            if (creator is CoinCreator) coins.Add(creator.Create(x, y, 5));
            if (creator is WallCreator) walls.Add(creator.Create(x, y, WallSize));
        }
        public void Clear()
        {
            walls.Clear();
            coins.Clear();
        }

        public void Show(Graphics g)
        {

            foreach (var wall in walls)
            {
                wall.Show(g);
            }
            foreach (var coin in coins)
            {
                coin.Show(g);
            }
            g.DrawLine(new Pen(Color.Red, 2), 0, 0, Width, 0);
            g.DrawLine(new Pen(Color.Red, 2), 0, 0, 0, Height);
            g.DrawLine(new Pen(Color.Red, 2), 0, Height, Width, Height);
            g.DrawLine(new Pen(Color.Red, 2), Width, 0, Width, Height);
        }
        public void PacmanMove()
        {
            pacman.Move(this);
        }

    }
}
