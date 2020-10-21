using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePacman
{
    public abstract class Creator
    {
        public abstract GameObject Create(int x, int y,int size);
    }
}
