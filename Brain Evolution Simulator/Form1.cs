using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Brain_Evolution_Simulator
{
    public partial class MainForm : Form
    {
        World world = new World();

        delegate void PerformanceLabelsCallback(string iterations, string secs, string fps);
        delegate void SetStartButtonCallback(bool startButtonEnabled, bool framerateBoxEnabled, bool stopping);

        bool stopping = false;

        public MainForm() // Constructor
        {
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            Debug.WriteLine("World created");

            for (int i = 0; i < 1; i++)
            {
                world.CreateEntity();
            }

            for (int i = 0; i < 10; i++)
            {
                world.CreateFood();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void start_Click(object sender, EventArgs e) // Process button clicked
        {
            startButton.Enabled = false;
            stopButton.Enabled = true;

            framerateBox.Enabled = false;

            //Task.Factory.StartNew(() => Process());
            Task.Run(() => Process());
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            stopButton.Enabled = false;
            stopping = true;
        }

        private void Process()
        {
            Stopwatch sw = new Stopwatch();

            TimeSpan totalMillisecs = TimeSpan.FromMilliseconds(0d);
            long totalIterations = 0L;

            while (!stopping)
            {
                sw.Start();

                int i;
                for (i = 0; i < framerateBox.Value; i++)
                {
                    world.Update();
                }

                sw.Stop();

                if (displayCheckBox.Checked) panel2D.Invalidate();

                totalIterations += i;
                totalMillisecs = totalMillisecs + TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds);
                string iterations = totalIterations + " iterations";
                string secs = ((float)totalMillisecs.TotalMilliseconds / 1000) + " secs";
                string fps = (1f / (((float)sw.ElapsedMilliseconds / 1000f) / i) + " fps");

                UpdatePerformanceLabels(iterations, secs, fps);

                sw.Reset();
            }

            SetStartButtons(true, true, false);
        }

        private void SetStartButtons (bool startButtonEnabled, bool framerateBoxEnabled, bool stopping)
        {
            if (startButton.InvokeRequired)
            {
                SetStartButtonCallback d = new SetStartButtonCallback(SetStartButtons);
                this.Invoke(d, new object[] { startButtonEnabled, framerateBoxEnabled, stopping });
            }
            else
            {
                startButton.Enabled = startButtonEnabled;
                framerateBox.Enabled = framerateBoxEnabled;
                this.stopping = stopping;
            }
        }

        private void UpdatePerformanceLabels(string iterations, string secs, string fps)
        {
            if (label1.InvokeRequired)
            {
                PerformanceLabelsCallback d = new PerformanceLabelsCallback(UpdatePerformanceLabels);
                this.Invoke(d, new object[] { iterations, secs, fps });
            }
            else
            {
                label1.Text = iterations;
                label2.Text = secs;
                label3.Text = fps;
            }
        }
  
        private void saveButton_Click(object sender, EventArgs e) // Save button clicked
        {
            foreach (Entity entity in world.entityList)
            {
                Utility.SaveEntity(entity, @"C:\Entities");
            }
        }

        private void panel2D_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            float ratioX = (float)panel2D.Size.Width / (float)world.worldXSize; // Ratio to use to scale panel coordinates to world size
            float ratioY = (float)panel2D.Size.Height / (float)world.worldYSize;

            foreach (Entity entity in world.entityList)
            {
                Utility.DrawEntity(entity, e.Graphics, ratioX, ratioY);

                Debug.WriteLine("Entity drawn");
            }

            foreach (Food food in world.foodList)
            {
                Utility.DrawFood(food, e.Graphics, ratioX, ratioY);

                Debug.WriteLine("Food drawn");
            }

            base.OnPaint(e);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            while (stopping) ;
        }

    }
}
