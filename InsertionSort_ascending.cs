using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SortVisualizer
{
    class InsertionSort_ascending : ISortEngine
    {
        private readonly int[] Elements;
        private readonly Graphics graphic;
        private readonly int MaximumValue;
        readonly Brush DarkGreen = new SolidBrush(Color.DarkGreen);
        readonly Brush LightGreen = new SolidBrush(Color.LightGreen);

        public InsertionSort_ascending(int[] elements, Graphics graphic, int maximumValue)
        {
            Elements = elements;
            this.graphic = graphic;
            MaximumValue = maximumValue;
        }

        public void algorithm()
        {
            for (int i = 1; i < Elements.Count(); ++i)
            {
                int key = Elements[i];
                int j = i - 1;

                while (j >= 0 && Elements[j] > key)
                {
                    Elements[j + 1] = Elements[j];
                    j -= 1;
                    Visualize(i, Elements[i]);
                    Visualize(j + 1, Elements[j + 1]);
                }
                Elements[j + 1] = key;

            }

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
                if (Elements[i] > Elements[i + 1]) return false;
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
