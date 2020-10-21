using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamePacman
{
    public class Pacman : GameObject
    {
        public int Coins { get; private set; } = 0;
        public Pacman(int x, int y, int width, int height, Bitmap texture) : base(x, y, width, height)
        {
            Texture = texture;
        }
        public Pacman(int x, int y, int width, int height) : base(x, y, width, height) { }


        private int speed = 10;
        private int x = 0;
        private int y = 0;
        private int k1 = 0;
        private int k2 = 0;


        public void Read(char key)
        {
            switch (key)
            {
                case 'a':

                    k1 = -1;

                    k2 = 0;
                    break;
                case 'w':
                    k1 = 0;
                    k2 = -1;
                    break;
                case 's':
                    k1 = 0;
                    k2 = 1;
                    break;
                case 'd':
                    k1 = 1;
                    k2 = 0;
                    break;
            }
            x = k1 * speed;
            y = k2 * speed;
        }
        private void GetCoin(Field field)
        {
            for(int i = 0; i < field.coins.Count; i++)
            {
                if (field.coins[i].IsCroosed(this))
                {
                   
                    field.coins.Remove(field.coins[i]);
                    this.Coins++;
                    
                }
            }
        }
        public void Move(Field field)
        {
            bool t = true;
            foreach (var wall in field.walls)
            {
                if (new Pacman(X + x, Y + y, Width, Height).IsCroosed(wall)) t = false;
            }
            if (t && !(X + x + this.Width / 2 > field.Width || X + x - this.Width / 2 < 0))

                X += x;

            if (t && !(Y + y + this.Height / 2 > field.Height || Y + y - this.Height / 2 < 0))
                Y += y;
            GetCoin(field);
        }
    }
}
