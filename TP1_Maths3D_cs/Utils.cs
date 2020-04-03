using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class Utils
    {
        public static double ConvertDegreesToRadians(double degrees)
        {
            return (Math.PI / 180) * degrees;
        }

        public static double ConvertRadiansToDegrees(double radians)
        {
            return (180 / Math.PI) * radians;
        }
    }
}
