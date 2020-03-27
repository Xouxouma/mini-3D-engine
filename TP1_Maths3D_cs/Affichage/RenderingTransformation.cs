using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class RenderingTransformation
    {
        double cameraFovY;
        VectCartesien winRes;
        Matrix worldToCamera;
        Matrix cameraToWorld;

        Matrix zoomMatrix;
        double proche = 0.1;
        double loin = 100;
        double haut;
        double bas;
        double gauche;
        double droite;

        public double imageRatio { get; private set; }

        public RenderingTransformation(VectCartesien posCamera, VectCartesien cibleCamera, double winResX, double winResY, double fovX, double fovY)
        {
            this.winRes = new VectCartesien(winResX, winResY);
            placeCamera(posCamera, cibleCamera);
            //setZoom(fovX, fovY);
            imageRatio = winResX / winResY;
            scale(fovY);
            cameraFovY = fovY;
        }

        public void placeCamera(VectCartesien from, VectCartesien to)
        {
            if (from.getDim() != 3 || to.getDim() != 3)
                throw new System.ArgumentException("VectCartesiens 'from' and 'to' must be of size 3");

            VectCartesien forward = (to - from).normalize();
            VectCartesien tmp_up = new VectCartesien(0.0, 1.0, 0.0);
            VectCartesien left = tmp_up.produit_vectoriel(forward).normalize();
            VectCartesien up = forward.produit_vectoriel(left).normalize();

            Matrix tmpMatrix = new Matrix(left, up, forward).transposee();
            cameraToWorld = tmpMatrix.increase_dim();
            cameraToWorld[3, 0] = -(left * from);
            cameraToWorld[3, 1] = -(up * from);
            cameraToWorld[3, 2] = -(forward * from);
            worldToCamera = cameraToWorld;
        }


        public Matrix perspective_projection()
        {
            double aspect = winRes[0] / winRes[1];
            double tanFovY = Math.Tan(cameraFovY / 2);
            Matrix m = new Matrix(
                new VectCartesien(1 / (aspect * tanFovY), 0, 0, 0),
                new VectCartesien(0, 1 / tanFovY, 0, 0),
                new VectCartesien(0, 0, (loin + proche) / (loin - proche), 1),
                new VectCartesien(0, 0, -2 * loin * proche / (loin - proche), 0)
            );
            return m;
        }

        public bool estDansFrustrum(VectCartesien p)
        {
            double w = p[3];
            for (int i = 0; i < 3; i++)
                if (p[i] < -w || p[i] > w)
                    return false;
            return true;
        }

        public VectCartesien clipToNormalized(VectCartesien clip)
        {
            double x = clip[0] / clip[3];
            double y = clip[1] / clip[3];
            double z = clip[2] / clip[3];
            double w = 1;

            return new VectCartesien(x, -y, z, w);
        }

        public VectCartesien normalizedToWindow(VectCartesien normalized)
        {
            double x = winRes[0] / 2 * normalized[0] + (winRes[0] / 2);
            double y = winRes[1] / 2 * normalized[1] + (winRes[1] / 2);
            double z = normalized[2];

            return new VectCartesien(x, y, z);
        }

        public VectCartesien appliqueZoom(VectCartesien p)
        {
            return p * zoomMatrix;
        }

        private void setZoom(double fovX, double fovY)
        {
            double zoomX = 1 / Math.Tan(fovX / 2);
            double zoomY = 1 / Math.Tan(fovY / 2);
            double zoomZ = -(loin + proche) / (loin - proche);
            Matrix m = new Matrix(
                new VectCartesien(zoomZ, 0, 0, 0),
                new VectCartesien(0, zoomY, 0, 0),
                new VectCartesien(0, 0, zoomZ, -2 * proche * loin / (loin - proche)),
                new VectCartesien(0, 0, -1, 0));
            zoomMatrix = m;

            haut = Math.Tan(fovY / 2) * proche;
            bas = -haut;
        }

        public void scale(double fov)
        {
            double zoom = Math.Tan(fov * 0.5) * proche;
            droite = zoom * imageRatio;
            gauche = -droite;
            haut = zoom;
            bas = -haut;

        }

        public void setZoomX(float zoomX)
        {
            double zoomY = zoomX * winRes[0] / winRes[1];
            this.setZoom(zoomX, zoomY);
        }
        public void setZoomY(float zoomY)
        {
            double zoomX = zoomY * winRes[1] / winRes[0];
            this.setZoom(zoomX, zoomY);
        }

        public VectCartesien placePointSurEcran(VectCartesien p, Matrix model)
        {
            //Console.WriteLine("p : " + p);
            VectCartesien p4 = p.increase_dim();
            p4[3] = 1;

            Matrix MVP = model * worldToCamera * perspective_projection();

            VectCartesien pClip = p4 * MVP;

            VectCartesien pNormalized = clipToNormalized(pClip);
            VectCartesien pWindow = normalizedToWindow(pNormalized);
            return pWindow;
        }
    }
}
