using System;
using System.Drawing;

namespace ParticleSimulation
{
    public class Particle
    {
        Simulation Sim;
        public double X, Y, Xv, Yv, C, M;
        public Particle( Simulation s, double x, double y, double m, double c)
        {
            this.Sim = s;

            this.X = x * Sim.Scale;
            this.Y = y * Sim.Scale;
            this.C = c;
            this.M = m;
            this.Xv = 0;
            this.Yv = 0;
        }
        
        public void Interact ( Particle other)
        {
            double Distance = Math.Sqrt( Math.Pow( this.X - other.X , 2) + Math.Pow(this.Y - other.Y , 2) );

            double Force = Sim.Ke * ((this.C * other.C) / Math.Pow( Distance, 2));
            Force *= (Sim.p.StepTime / 1000);

            double AngleRad = Math.Atan2( other.Y - this.Y , other.X - this.X ) + Math.PI;

            this.Xv += Math.Cos(AngleRad) * (Force / M);
            this.Yv += Math.Sin(AngleRad) * (Force / M);
        }

        public void Step()
        {
            this.X += Xv;
            this.Y += Yv;
        }

        public void Draw( Graphics g)
        {
            float xx = (float)(X / Sim.Scale);
            float yy = (float)(Y / Sim.Scale);

            Color color;
            if (C < 0) color = Color.Black;
            else color = Color.Red;

            g.FillEllipse( new SolidBrush(color), xx - 2.5f, yy - 2.5f, 5f, 5f); 
        } 
    }
}