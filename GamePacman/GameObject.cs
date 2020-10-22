using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GamePacman
{
    [Serializable]
    public abstract class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get;set; }
        public Color Color { get; set; }
        public Bitmap Texture { get; protected set; }
        public GameObject() { }
        public GameObject(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        public GameObject(int x, int y, int width, int height, Bitmap texture)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Texture = texture;
        }
        public bool IsCroosed(GameObject gameObject)
        {
            if (this.X + this.Width / 2 >= gameObject.X - gameObject.Width / 2
                && this.Y + this.Height / 2 >= gameObject.Y - gameObject.Height / 2
                && this.Y - this.Height / 2 <= gameObject.Y + gameObject.Height / 2
                && this.X - this.Width / 2 <= gameObject.X + gameObject.Width / 2
                ) return true;
            return false;
           

        }
        public virtual void Show(Graphics g)
        {
            g.DrawImage(Texture, new Rectangle(X - Width / 2, Y - Height / 2, Width, Height));
        }
    }
}
