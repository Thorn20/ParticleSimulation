using System;
using System.Timers;
using System.Drawing;
using System.Windows.Forms;

namespace ParticleSimulation
{
    public class Program : Form
    {
        private System.Timers.Timer StepTimer;
        private DateTime StepStart, StepEnd;
        public double StepTime;
        public double StepsPerSec = 0.0;
        public double StepLimit = 1000;

        private Size WinSize = new Size( 800, 600);
        public Gfx GFX;
        public ConPrinter ConPrint;
        public Simulation Sim;
        public Program()
        {
            this.Text = "Particle Simulation";
            this.Size = WinSize;
            this.Resize += new EventHandler(this.OnResize);
            this.SetStyle( ControlStyles.OptimizedDoubleBuffer, true);

            GFX = new Gfx(this);
            Sim = new Simulation(this);
            ConPrint = new ConPrinter(this);

            StepTimer = new System.Timers.Timer(1000);
            StepTimer.Elapsed += GameStep;
            StepTimer.AutoReset = true;
            StepTimer.Enabled = true;
        }

        private void GameStep(object o, ElapsedEventArgs e)
        {
            StepTimer.Enabled = false;
            StepStart = DateTime.Now;

            Sim.Step();

            StepEnd = DateTime.Now;
            StepTime = StepEnd.Subtract(StepStart).TotalMilliseconds;

            if (StepTime < (1000 / StepLimit)) 
            {
                StepTimer.Interval = (1000 / StepLimit) - StepTime; 
                StepTime = (1000 / StepLimit);
            }
            else
                StepTimer.Interval = StepTime; 

            StepsPerSec = (1000 / StepTime);                       
            StepTimer.Enabled = true;      
        }

        protected override void OnPaint( PaintEventArgs e) 
        {
            GFX.Buffer.Render(e.Graphics);
        }

        private void OnResize(object o, EventArgs e)
        {
            this.Size = WinSize;
            GFX.InitGraphics();
        }

        public static void Main(string[] args)
        {
            Application.Run( new Program() );
        }
    }
}