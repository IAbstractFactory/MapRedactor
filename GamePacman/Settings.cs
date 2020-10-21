using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamePacman
{
    [Serializable]
    public class Settings
    {
        public List<GameObject> coins;
        public List<GameObject> walls;
        public int fildWidt;
        public int fildHeight;
    }
}
