using System;
using System.Timers;

namespace ParticleSimulation
{
    public class ConPrinter
    {
        Program p;

        Timer PrintTimer;

        public ConPrinter( Program p)
        {
            this.p = p;

            PrintTimer = new System.Timers.Timer(1000);
            PrintTimer.Elapsed += Print;
            PrintTimer.AutoReset = true;
            PrintTimer.Enabled = true;
        }

        public void Print( object o, ElapsedEventArgs e)
        {
            double sps = p.StepsPerSec,
                   AvgSteps = 1000 / p.StepsPerSec,
                   fps = p.GFX.FramesPerSec,
                   AvgFrames = 1000 / p.GFX.FramesPerSec;

            Console.Clear();
            Console.WriteLine("Calculating {0} Particles", p.Sim.Particles.Length);
            Console.WriteLine("{0} Steps/sec, with Average Step Time {1} ms", sps.ToString("F2"), AvgSteps.ToString("F2"));
            Console.WriteLine("{0} Frames/second, with Average Frame Time {1} ms", fps.ToString("F2"), AvgFrames.ToString("F2"));
        }
    }
}