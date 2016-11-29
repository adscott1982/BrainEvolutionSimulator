using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Brain_Evolution_Simulator
{
    public class Food
    {
        public PointF position;
        public PointF size;

        public Food(PointF position, PointF size)
        {
            this.position = position;
            this.size = size;
        }
    }
}
