using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePacman
{
    public class CursorImage
    {
        public GameObject GameObject { get; set; }
        public void Draw(Graphics g)
        {
            GameObject?.Show(g);
        }
    }
}
