using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePacman
{
    public class CoinCreator : Creator
    {

        public override GameObject Create(int x, int y,int Size)
        {
            return new Coin(x, y, Size, Size);
        }
    }
}
