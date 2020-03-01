using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class CameraRas
    {
        VectCartesien pos;

        VectCartesien targetPos;

        double FovX;
        double FovY;

        double distEcran = 0.1;
        double largeur;
        double hauteur;

        double r;
        double l;
        double t;
        double b;

        public CameraRas(VectCartesien posCamera, VectCartesien cibleCamera, double winResX, double winResY)
        {
            pos = posCamera;
            largeur = winResX;
            hauteur = winResY;
            r = largeur;
            l = hauteur;
            t = 0;
            b = 0;
        }

        public VectCartesien CameraToScreen(VectCartesien pCamera)
        {
            if (pCamera.getDim() != 3)
                throw new System.ArgumentException("Vect must be of size 3.");

            double x = distEcran * pCamera[0] / (0 - pCamera[2]);
            double y = distEcran * pCamera[1] / (0 - pCamera[2]);
            double z = -pCamera[2];

            return new VectCartesien(x, y, z);
        }

        public VectCartesien ScreenToNDC(VectCartesien pScreen)
        {
            double x = 2 * (pScreen[0] + 1) / (r - l) - (r + l) / (r - l);
            double y = 2 * (pScreen[1]) / (t - b) - (t + b) / (t - b);
            double z = pScreen[2];

            return new VectCartesien(x, y, z);
        }

        public VectCartesien ScreentoRaster(VectCartesien pScreen)
        {
            double x = (pScreen[0] + 1) / 2 * largeur;
            double y = (1 - pScreen[1]) / 2 * hauteur;
            double z = pScreen[2];

            return new VectCartesien(x, y, z);
        }
    }
}
