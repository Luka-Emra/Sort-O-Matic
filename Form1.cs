using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SortVisualizer
{
    public partial class Sorting : Form
    {
        int[] Elements;
        Graphics graphic;
        BackgroundWorker Background_Worker = null;
        bool Stopped = false;

        public Sorting()
        {
            InitializeComponent();
            PopulateDropdown();
        }

        private void PopulateDropdown()
        {
            List<string> ClassList = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(ISortEngine).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.Name).ToList();
            ClassList.Sort();
            foreach (string entry in ClassList)
            {
                comboBox1.Items.Add(entry);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (Elements == null) BtnReset_Click(null, null);

            Background_Worker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };
            Background_Worker.DoWork += new DoWorkEventHandler(Bgw_DoWork);
            Background_Worker.RunWorkerAsync(argument: comboBox1.SelectedItem);
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {

            if (!Stopped)
            {
                Background_Worker.CancelAsync();
                Stopped = true;
            }
            else
            {
                if (Background_Worker.IsBusy) return;
                int NumEntries = panel1.Width;
                int MaxVal = panel1.Height;
                Stopped = false;
                for (int i = 0; i < NumEntries; i++)
                {
                    graphic.FillRectangle(new SolidBrush(Color.LightGreen), i, 0, 1, MaxVal);
                    graphic.FillRectangle(new SolidBrush(Color.DarkGreen), i, MaxVal - Elements[i], 1, MaxVal);
                }
                Background_Worker.RunWorkerAsync(argument: comboBox1.SelectedItem);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            graphic = panel1.CreateGraphics();
            int NumEntries = panel1.Width;
            int MaxVal = panel1.Height;
            Elements = new int[NumEntries];
            graphic.FillRectangle(new SolidBrush(Color.LightGreen), 0, 0, NumEntries, MaxVal);
            Random rand = new Random();
            for (int i = 0; i < NumEntries; i++)
            {
                Elements[i] = rand.Next(0, MaxVal);
            }
            for (int i = 0; i < NumEntries; i++)
            {
                graphic.FillRectangle(new SolidBrush(Color.DarkGreen), i, MaxVal - Elements[i], 1, MaxVal);
            }
        }


        public void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            string SortEngineName = (string)e.Argument;
            Type type = Type.GetType("SortVisualizer." + SortEngineName);
            var ctors = type.GetConstructors();
            try
            {
                ISortEngine se = (ISortEngine)ctors[0].Invoke(new object[] { Elements, graphic, panel1.Height });
                while (!se.In_ascending_order() && (!Background_Worker.CancellationPending))
                {
                    se.algorithm();
                }
            }
            catch (Exception)
            {
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.Equals("SelectionSort_ascending") || comboBox1.SelectedItem.Equals("InsertionSort_ascending") || comboBox1.SelectedItem.Equals("SelectionSort_descending") || comboBox1.SelectedItem.Equals("InsertionSort_descending"))
            {
                btnPause.Visible = false;
            }
            else
                btnPause.Visible = true;
        }
    }
}
 