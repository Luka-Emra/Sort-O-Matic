using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    public interface ISortEngine
    {
        void algorithm();
        bool In_ascending_order();

        void Paint_Again();
    }
}
