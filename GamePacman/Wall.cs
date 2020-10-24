using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePacman
{
    [Serializable]
    public class Wall : GameObject
    {
        private Color color;
        public Wall(int x, int y, int height, int width, Color color) : base(x, y, width, height)
        {
            this.color = color;

        }
        public Wall() : base() { }

        public override void Show(Graphics g)
        {
            Pen pen;
            if (Selected)
                pen = new Pen(Color.Aqua, 2);
            else
                pen = new Pen(color, 2);
            g.DrawLine(pen, X - Width / 2, Y - Height / 2, X + Width / 2, Y - Height / 2);
            g.DrawLine(pen, X - Width / 2, Y + Height / 2, X + Width / 2, Y + Height / 2);
            g.DrawLine(pen, X - Width / 2, Y - Height / 2, X - Width / 2, Y + Height / 2);
            g.DrawLine(pen, X + Width / 2, Y - Height / 2, X + Width / 2, Y + Height / 2);
        }

        public override object Clone()
        {
            return new Wall(X, Y, Height, Width, color);
        }
    }
}
