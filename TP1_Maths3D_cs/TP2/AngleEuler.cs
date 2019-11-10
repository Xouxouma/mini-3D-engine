using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class AngleEuler
    {
        private double heading; // sens horaire autour de y ; y vers le haut
        private double pitch; // sens horaire autour de x ; x vers la droite
        private double bank; // sens anti-horaire autour de z ; z vers le fond

        public AngleEuler(double heading, double pitch, double bank)
        {
            this.heading = heading;
            this.pitch = pitch;
            this.bank = bank;
        }
        public AngleEuler() : this(0.0, 0.0, 0.0) { }

        public double GetHeading()
        {
            return heading;
        }
        public double GetPitch()
        {
            return pitch;
        }
        public double getBank()
        {
            return bank;
        }

        public override String ToString()
        {
            return "Angle d\'Euler (" + heading + ", " + pitch + ", " + bank + ")";
        }

        // Conversions
        public Quaternion toQuaterion()
        {
            double w = Math.Cos(heading / 2) * Math.Cos(pitch / 2) * Math.Cos(bank / 2) + Math.Sin(heading / 2) * Math.Sin(pitch / 2) * Math.Sin(bank / 2);
            double x = Math.Sin(heading / 2) * Math.Cos(pitch / 2) * Math.Cos(bank / 2) - Math.Cos(heading / 2) * Math.Sin(pitch / 2) * Math.Sin(bank / 2);
            double y = Math.Cos(heading / 2) * Math.Sin(pitch / 2) * Math.Cos(bank / 2) + Math.Sin(heading / 2) * Math.Cos(pitch / 2) * Math.Sin(bank / 2);
            double z = Math.Cos(heading / 2) * Math.Cos(pitch / 2) * Math.Sin(bank / 2) - Math.Sin(heading / 2) * Math.Sin(pitch / 2) * Math.Cos(bank / 2);
            return new Quaternion(w, x, y, z);
        }
    }
}
