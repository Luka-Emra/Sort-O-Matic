using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SortVisualizer
{
    class SelectionSort_descending : ISortEngine
    {
        private readonly int[] Elements;
        private readonly Graphics graphic;
        private readonly int MaximumValue;
        readonly Brush DarkGreen = new SolidBrush(Color.DarkGreen);
        readonly Brush LightGreen = new SolidBrush(Color.LightGreen);

        public SelectionSort_descending(int[] elements, Graphics graphic, int maximumValue)
        {
            Elements = elements;
            this.graphic = graphic;
            MaximumValue = maximumValue;
        }

        public void algorithm()
        {
            for (int i = 0; i < Elements.Count() - 1; i++)
            {
                int max_item = i;
                for (int j = i + 1; j < Elements.Count(); ++j)
                {
                    if (Elements[j] > Elements[max_item])
                    {
                        max_item = j;
                    }

                    Swapping_items(max_item, i);
                }

            }
        }

        private void Swapping_items(int item_1, int item_2)
        {
            int Temp_value = Elements[item_1];
            Elements[item_1] = Elements[item_2];
            Elements[item_2] = Temp_value;

            Visualize(item_1, Elements[item_1]);
            Visualize(item_2, Elements[item_2]);
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
