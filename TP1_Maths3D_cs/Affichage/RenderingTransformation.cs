﻿using System;
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

        public enum ProjectionMode { Perspective, Orthogonale }
        public ProjectionMode projectionMode;
        public double imageRatio { get; private set; }

        public RenderingTransformation(VectCartesien posCamera, VectCartesien cibleCamera, double winResX, double winResY, double fovX, double fovY, ProjectionMode projectionMode)
        {
            this.winRes = new VectCartesien(winResX, winResY);
            placeCamera(posCamera, cibleCamera);
            //setZoom(fovX, fovY);
            imageRatio = winResX / winResY;
            scale(fovY);
            cameraFovY = fovY;
            this.projectionMode = projectionMode;
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

        public Matrix orthographic_projection()
        {
            double aspect = winRes[0] / winRes[1];
            double tanFovY = Math.Tan(cameraFovY / 2);
            Matrix m = new Matrix(
                new VectCartesien(1 / (aspect * tanFovY), 0, 0, 0),
                new VectCartesien(0, 1 / tanFovY, 0, 0),
                new VectCartesien(0, 0, -2 / (loin - proche), 0),
                new VectCartesien((loin + proche) / (loin - proche), 0, (loin + proche) / (loin - proche) , 1)
            );
            return m;
        }

        public Matrix orthogonal_projection()
        {
            double aspect = winRes[0] / winRes[1];
            double tanFovY = Math.Tan(cameraFovY / 2);
            gauche = -aspect;
            droite = aspect;
            haut = 3;
            bas = -3;
            Matrix m = new Matrix(
                new VectCartesien(2 / droite - gauche, 0, 0, 0),
                new VectCartesien(0, 2 / haut - bas, 0, 0),
                new VectCartesien(0, 0, 2 / (loin - proche), 1),
                new VectCartesien(- (loin + proche) / (loin - proche), - (haut + bas) / (haut - bas),  - (loin + proche) / (loin - proche), 0)
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

        public VectCartesien placePointSurEcran(VectCartesien p, VectCartesien translation, Quaternion rotation, double agrandissement)
        {
            Matrix model = Matrix.translation(translation);

            p = p * Matrix.ordinal_scale(new VectCartesien(agrandissement, agrandissement, agrandissement));

            Quaternion q = new Quaternion(0, p);
            //Console.WriteLine("\n Original q = " + q);
            q = rotation * q * rotation.inverse();
            //Console.WriteLine("q = " + q);
            //Console.WriteLine("rot = " + rotation);
            //Console.WriteLine("rot inverse = " + rotation.inverse());
            //Quaternion.SLERP(q, rotation, 2);

            //VectCartesien p4 = p.increase_dim();
            //p4[3] = 1;
            VectCartesien p4 = new VectCartesien(q.getX(), q.getY(), q.getZ(), 1);
            //Console.WriteLine("p4 = " + p4);

            Matrix projection = (projectionMode == ProjectionMode.Perspective) ? perspective_projection() : orthographic_projection();
            Matrix MVP = model * worldToCamera * projection;

            VectCartesien pClip = p4 * MVP;

            VectCartesien pNormalized = clipToNormalized(pClip);
            VectCartesien pWindow = normalizedToWindow(pNormalized);
            return pWindow;
        }

        public Triangle placeTriangleSurEcran(Triangle untransformedTriangle, VectCartesien translation, Quaternion rotation, double agrandissement)
        {
            VectCartesien[] vertices = untransformedTriangle.getVertices();
            VectCartesien[] ptsEcran = new VectCartesien[3];
            for (int i = 0; i < 3; i++)
            {
                //vertices[i] = rotateTriangle(untransformedTriangle, 30 * Math.PI / 180, 'x');
                ptsEcran[i] = this.placePointSurEcran(vertices[i], translation, rotation, agrandissement);
            }

            Triangle triangleEcran3D = new Triangle(ptsEcran[0], ptsEcran[1], ptsEcran[2]);
            return triangleEcran3D;
        }

    }
}
