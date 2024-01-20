using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Act2_Colision
{
    public class Block
    {
        public Point pos;
        public Size size;

        public Block(Point position, Size blockSize)
        {
            pos = position;
            size = blockSize;
        }
    }
}
