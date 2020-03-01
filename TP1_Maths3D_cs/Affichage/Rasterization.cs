using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class Rasterization
    {
        const float cameraFovY = 80.0f;
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

        public Rasterization(VectCartesien posCamera, VectCartesien cibleCamera, double winResX, double winResY, double fovX, double fovY)
        {
            this.winRes = new VectCartesien(winResX, winResY);
            placeCamera(posCamera, cibleCamera);
            //setZoom(fovX, fovY);
            imageRatio = winResX / winResY;
            scale(fovY);
        }

        public void placeCamera(VectCartesien from, VectCartesien to)
        {
            if (from.getDim() != 3 ||to.getDim() != 3)
                throw new System.ArgumentException("VectCartesiens 'from' and 'to' must be of size 3");

            VectCartesien forward = (from - to).normalize();
            VectCartesien tmp_up = new VectCartesien(0.0, 1.0, 0.0);
            VectCartesien left = tmp_up.normalize().produit_vectoriel(forward).normalize();
            VectCartesien up = forward.produit_vectoriel(left).normalize();

            left = left.increase_dim();
            up = up.increase_dim();
            forward = forward.increase_dim();
            from = from.increase_dim(); from[3] = 1;

            cameraToWorld = new Matrix(left, up, forward, from).transposee();
            //cameraToWorld = Matrix.rotation(left, up, forward);
            worldToCamera = cameraToWorld.inverse();
        }

        public VectCartesien projecteSurEcran(VectCartesien p)
        {
            if (p.getDim() != 4)
                throw new System.ArgumentException("VectCartesien ("+p.getDim()+") p must be of size 4");

            Matrix m = new Matrix(
                new VectCartesien(2 * proche / (droite - gauche), 0, (droite + gauche) / (droite - gauche), 0),
                new VectCartesien(0, 2 * proche / (haut - bas), (haut + bas) / (haut - bas), 0),
                new VectCartesien(0, 0, -(loin + proche) / (loin - proche), -2 * loin * proche / (loin - proche)),
                new VectCartesien(0, 0, -1, 0)
                );
            VectCartesien res = p * m;
            return res / res[3];
        }

       public VectCartesien projectionPerspective(VectCartesien p)
        {
            return p * Matrix.perspetive_projection(1) / p[3];
        }

        public bool estDansFrustrum(VectCartesien p)
        {
            double w = p[3];
            for (int i = 0; i < 3; i++)
                if (p[i] < -w || p[i] > w)
                    return false;
            return true;
        }

        public VectCartesien clipToEcran(VectCartesien clip)
        {
            VectCartesien winCenter = winRes / 2;

            double w2 = clip[3] * 2;

            double x = (clip[0] + winRes[0]) / w2 + winCenter[0];
            double y = - (clip[1] + winRes[1]) / w2 + winCenter[1];
            double z = clip[2] / clip[3];

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
            double zoom = Math.Tan(fov * 0.5 * Math.PI / 180) * proche;
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

        public VectCartesien placePointSurEcran(VectCartesien p)
        {
            VectCartesien p4 = p.increase_dim();
            p4[3] = 1;

            VectCartesien pCamera = p4 * worldToCamera;

            //VectCartesien projete = projectionPerspective(pCamera);
            VectCartesien projete = projecteSurEcran(pCamera);

            bool dedans = estDansFrustrum(projete);

            // VectCartesien clip = appliqueZoom(projete);
            VectCartesien clip = projete;

            //return clipToEcran(clip);
            return projete;
        }
    }
}
