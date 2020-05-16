using System;
using System.Drawing;

namespace ParticleSimulation
{
    public class Simulation
    {
        public Program p;

        public double Scale = 1.0;
        public double Ke = 9e9;

        public Particle[] Particles;

        public Simulation( Program p)
        {
            this.p = p;

            MakeParticles( 200 );
        }

        public void MakeParticles(int count)
        {
            Random rng = new Random((int)DateTime.Today.Ticks);

            Particles = new Particle[count];

            for(int ii = 0; ii < Particles.Length; ii++ )
            {
                double x, y, mass, charge;

                x = rng.NextDouble() * p.Width;
                y = rng.NextDouble() * p.Height;
                mass = 1.109e-31;
                charge = -1.602e-19;

                Particles[ii] = new Particle(this, x, y, mass, charge);
            }
        }

        public void Step()
        {
            foreach (Particle particle in Particles) 
                foreach (Particle other in Particles)
                    if (other != particle)
                        particle.Interact( other );

            foreach (Particle particle in Particles) 
            {
                particle.Step();

                if (particle.X <= 0 || particle.X >= p.Width) particle.Xv *= -1;
                if (particle.Y <= 0 || particle.Y >= p.Height) particle.Yv *= -1;
            }
        }

        public void Draw( Graphics g)
        {
            g.FillRectangle( Brushes.White , 0 , 0 , p.Width, p.Height);

            foreach (Particle particle in Particles) 
                particle.Draw(g);            
        }
    }
}