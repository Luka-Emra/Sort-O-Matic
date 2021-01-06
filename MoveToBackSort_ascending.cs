using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer
{
    class MoveToBackSort_ascending : ISortEngine
    {
        private readonly int[] Elements;
        private readonly Graphics graphic;
        private readonly int MaximumValue;
        readonly Brush DarkGreen = new SolidBrush(Color.DarkGreen);
        readonly Brush LightGreen = new SolidBrush(Color.LightGreen);

        private int Cur_List_Ptr = 0;

        public MoveToBackSort_ascending(int[] elements, Graphics graphic, int maximumValue)
        {
            Elements = elements;
            this.graphic = graphic;
            MaximumValue = maximumValue;
        }

        public void algorithm()
        {
            if (Cur_List_Ptr >= Elements.Count() - 1) Cur_List_Ptr = 0;
            if (Elements[Cur_List_Ptr] > Elements[Cur_List_Ptr + 1])
            {
                Turn_Around(Cur_List_Ptr);
            }
            Cur_List_Ptr++;
        }

#pragma warning disable IDE0060 
        private void Turn_Around(int Cur_Ptr)
#pragma warning restore IDE0060 
        {
            int Temp_Value = Elements[Cur_List_Ptr];
            int Ending = Elements.Count() - 1;
            for (int i = Cur_List_Ptr; i < Ending; i++)
            {
                Elements[i] = Elements[i + 1];
                Visualize(i, Elements[i]);
            }
            Elements[Ending] = Temp_Value;
            Visualize(Ending, Elements[Ending]);
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
