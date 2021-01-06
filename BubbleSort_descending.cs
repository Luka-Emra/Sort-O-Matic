using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SortVisualizer
{
    class BubbleSort_descending : ISortEngine
    {
        private readonly int[] Elements;
        private readonly Graphics graphic;
        private readonly int MaximumValue;
        readonly Brush DarkGreen = new SolidBrush(Color.DarkGreen);
        readonly Brush LightGreen = new SolidBrush(Color.LightGreen);

        public BubbleSort_descending(int[] elements, Graphics graphic, int maximumValue)
        {
            Elements = elements;
            this.graphic = graphic;
            MaximumValue = maximumValue;
        }

        public void algorithm()
        {
            for (int i = 0; i < Elements.Count() - 1; i++)
            {
                if (Elements[i] < Elements[i + 1])
                {
                    Swapping_items(i, i + 1);
                }
            }
        }

        private void Swapping_items(int i, int p)
        {
            int temp = Elements[i];
            Elements[i] = Elements[i + 1];
            Elements[i + 1] = temp;

            Visualize(i, Elements[i]);
            Visualize(p, Elements[p]);
        }

#pragma warning disable IDE0060 
        private void Visualize(int placing, int H)
#pragma warning restore IDE0060 
        {
            graphic.FillRectangle(LightGreen, placing, 0, 1, MaximumValue);
            graphic.FillRectangle(DarkGreen, placing, MaximumValue - Elements[placing], 1, MaximumValue);
        }

        public bool In_ascending_order()
        {
            for (int i = 0; i < Elements.Count() - 1; i++)
            {
                if (Elements[i] < Elements[i + 1]) return false;
            }
            return true;
        }

        public void Paint_Again()
        {
            for (int i = 0; i < (Elements.Count() - 1); i++)
            {
                graphic.FillRectangle(new SolidBrush(Color.DarkGreen), i, MaximumValue - Elements[i], 1, MaximumValue);
            }
        }
    }
}
