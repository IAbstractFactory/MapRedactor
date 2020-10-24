using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamePacman
{
    [Serializable]
    public class Coin : GameObject
    {
        public Coin() : base() { }
        public Coin(int x, int y, int height, int width) : base(x, y, width, height)
        {
            Color = Color.Gold;
        }

        public override object Clone()
        {
            return new Coin(X, Y, Height, Width);
        }

        public override void Show(Graphics g)
        {
            if(Selected)
                g.DrawEllipse(new Pen(Color.Aqua, 4), X - Width / 2, Y - Height / 2, Width, Height);
            else
            g.DrawEllipse(new Pen(Color, 4), X - Width / 2, Y - Height / 2, Width, Height);

        }
    }
}
