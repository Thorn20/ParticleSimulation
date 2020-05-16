using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Timers;

namespace ParticleSimulation
{
    public class Gfx
    {
        Program p;

        public Timer FrameTimer;
        public DateTime FrameStart, FrameEnd;
        public double FrameTime;
        public double FrameLimit = 60;
        public double FramesPerSec = 0.0;

        public BufferedGraphicsContext Context;
        public BufferedGraphics Buffer;       

        public Gfx( Program p)
        {
            this.p = p;

            InitGraphics();
        }

        public void InitGraphics()
        {
            Context = BufferedGraphicsManager.Current;

            Context.MaximumBuffer = new Size( p.Width + 1, p.Height + 1);

            if ( Buffer != null)
            {
                Buffer.Dispose();
                Buffer = null;
            }

            Buffer = Context.Allocate(p.CreateGraphics(), new Rectangle( 0, 0, p.Width, p.Height));

            p.Refresh();

            FrameTimer = new System.Timers.Timer(1000);
            FrameTimer.Elapsed += RenderFrame;
            FrameTimer.AutoReset = true;
            FrameTimer.Enabled = true;
        }

        public void RenderFrame(object o, ElapsedEventArgs e)
        {
            FrameTimer.Enabled = false;
            FrameStart = DateTime.Now;

            Graphics g = Buffer.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            //Stuff to draw----
            p.Sim.Draw( g);
            //-----------------

            Buffer.Render(Graphics.FromHwnd(p.Handle));

            FrameEnd = DateTime.Now;
            FrameTime = FrameEnd.Subtract(FrameStart).TotalMilliseconds;

            if (FrameTime < (1000/FrameLimit)) 
            {
                FrameTimer.Interval = ((1000/FrameLimit) - FrameTime);
                FrameTime = (1000/FrameLimit);
            }
            else
                FrameTimer.Interval = FrameTime;

            FramesPerSec = (1000 / FrameTime);
            FrameTimer.Enabled = true;
        }
    }
}