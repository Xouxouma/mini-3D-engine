using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moteur3D
{
    class Rasterization
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

        public Rasterization(VectCartesien posCamera, VectCartesien cibleCamera, double winResX, double winResY, double fovX, double fovY)
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
            if (from.getDim() != 3 ||to.getDim() != 3)
                throw new System.ArgumentException("VectCartesiens 'from' and 'to' must be of size 3");

            VectCartesien forward = (to - from).normalize();
            VectCartesien tmp_up = new VectCartesien(0.0, 1.0, 0.0);
            VectCartesien left = tmp_up.produit_vectoriel(forward).normalize();
            VectCartesien up = forward.produit_vectoriel(left).normalize();

            //left = left.increase_dim();
            //up = up.increase_dim();
            //forward = forward.increase_dim();
            //from = from.increase_dim(); from[3] = 1;
            //VectCartesien vecDot = new VectCartesien( - left * from, - up * from, - forward * from, 0);
            Matrix tmpMatrix = new Matrix(left, up, forward).transposee();
            cameraToWorld = tmpMatrix.increase_dim();
            cameraToWorld[3, 0] = - (left * from);
            cameraToWorld[3, 1] = - (up * from);
            cameraToWorld[3, 2] = - (forward * from);
            //cameraToWorld = new Matrix(left, up, forward, vecDot).transposee();
            //cameraToWorld = Matrix.rotation(left, up, forward);
            //worldToCamera = cameraToWorld.inverse();
            worldToCamera = cameraToWorld;
            Console.WriteLine("from = " + from);
            Console.WriteLine("forward = " + forward);
            Console.WriteLine("up = " + up);
            Console.WriteLine("left = " + left);
            Console.WriteLine("cameraToWorld : ");
            Console.WriteLine(cameraToWorld);
            Console.WriteLine("worldToCamera : ");
            Console.WriteLine(worldToCamera);
        }

        public VectCartesien projecteSurEcran(VectCartesien p)
        {
            Console.WriteLine("proche = " + proche);
            Console.WriteLine("loin = " + loin);
            Console.WriteLine("haut = " + haut);
            Console.WriteLine("bas = " + bas);
            Console.WriteLine("droite = " + droite);
            Console.WriteLine("gauche = " + gauche);
            if (p.getDim() != 4)
                throw new System.ArgumentException("VectCartesien ("+p.getDim()+") p must be of size 4");

            //Matrix m = new Matrix(
            //    new VectCartesien(2 * proche / (droite - gauche), 0, (droite + gauche) / (droite - gauche), 0),
            //    new VectCartesien(0, 2 * proche / (haut - bas), (haut + bas) / (haut - bas), 0),
            //    new VectCartesien(0, 0, -(loin + proche) / (loin - proche), -2 * loin * proche / (loin - proche)),
            //    new VectCartesien(0, 0, -1, 0)
            //    );
            double aspect = winRes[0] / winRes[1];
            double tanFovY = Math.Tan(cameraFovY / 2);
            Matrix m = new Matrix(
                new VectCartesien(1 / (aspect * tanFovY) , 0, 0, 0),
                new VectCartesien(0, 1 / tanFovY, 0, 0),
                new VectCartesien(0, 0, (loin + proche) / (loin - proche), 1),
                new VectCartesien(0, 0, -2 * loin * proche / (loin - proche), 0)
            );
            VectCartesien res = p * m;
            return res / res[3];
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
            //VectCartesien winCenter = winRes / 2;

            //double w2 = clip[3] * 2;

            //double x = (clip[0] + winRes[0]) / w2 + winCenter[0];
            //double y = - (clip[1] + winRes[1]) / w2 + winCenter[1];
            //double z = clip[2] / clip[3];

            double x = (2.0 * clip[0] - winRes[0]) / clip[1];
            double y = (2.0 * clip[1] - winRes[1]) / clip[1];
            double z = clip[2] / clip[3];

            return new VectCartesien(x, y, z);
        }
        public VectCartesien clipToNormalized(VectCartesien clip)
        {
            double x = clip[0] / clip[3];
            double y = clip[1] / clip[3];
            double z = clip[2] / clip[3];
            double w = 1;

            return new VectCartesien(x, -y, z , w);
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

        public VectCartesien placePointSurEcran(VectCartesien p)
        {
            Console.WriteLine("p : " + p);
            VectCartesien p4 = p.increase_dim();
            p4[3] = 1;

            VectCartesien pCamera = worldToCamera * p4;
            //Console.WriteLine("pCamera : " + pCamera);

            //VectCartesien projete = projectionPerspective(pCamera);
            VectCartesien projete = projecteSurEcran(pCamera);
            //Console.WriteLine("projete : " + projete);

            bool dedans = estDansFrustrum(projete);
            Console.WriteLine("dedans : " + dedans);

            // VectCartesien clip = appliqueZoom(projete);
            VectCartesien clip = projete;
            //Console.WriteLine("clip : " + clip);

            //return clipToEcran(clip);
            //return projete;
            Matrix MVP = Matrix.I(4) * worldToCamera * perspective_projection();

            Console.WriteLine("MODEL = " + Matrix.I(4));
            Console.WriteLine("VIEW = " + worldToCamera);
            Console.WriteLine("PROJECTION = " + perspective_projection());
            Console.WriteLine("MVP = " + MVP);
            Console.WriteLine("P4 = " + p4);
            Console.WriteLine("MVP * P4 = " + MVP * p4);
            Console.WriteLine("P4 * MVP = " + p4 * MVP);

            VectCartesien pClip = p4 * MVP;
            VectCartesien pClip2 = MVP * p4;
            //Console.WriteLine("PClip p4 * MVP= " + pClip);
            //Console.WriteLine("PClip MVP * p4 = " + pClip2);

            VectCartesien pNormalized = clipToNormalized(pClip);
            VectCartesien pNormalized2 = clipToNormalized(pClip2);
            Console.WriteLine("pNormalized = " + pNormalized);
            Console.WriteLine("pNormalized2 = " + pNormalized2);
            VectCartesien pWindow = normalizedToWindow(pNormalized);
            VectCartesien pWindow2 = normalizedToWindow(pNormalized2);
            Console.WriteLine("pWindow = " + pWindow);
            Console.WriteLine("pWindow2 = " + pWindow2);
            Console.WriteLine("^^winRes = " + winRes);
            Console.WriteLine("^^^^^^ P4 = " + p4);
            dedans = estDansFrustrum(pClip);
            Console.WriteLine("dedans : " + dedans);
            return pWindow;
        }
    }
}
