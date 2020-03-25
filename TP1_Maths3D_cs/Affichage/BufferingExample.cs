﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Moteur3D
{
    public class BufferingExample : Form
    {
        private BufferedGraphicsContext context;
        private BufferedGraphics grafx;

        private byte bufferingMode;

        private System.Windows.Forms.Timer timer1;
        private byte count;

        private Bitmap bm;
        private RenderingTransformation renderingTransformation;
        public enum RenderingMode { Line, Fill };
        public RenderingMode renderingMode = RenderingMode.Fill;

        public BufferingExample() : base()
        {
            double winResX = 512;
            double winResY = 288;
            this.Width = 512;
            this.Height = 288;
            // Configure the Form for this example.
            this.Text = "User double buffering";
            this.MouseDown += new MouseEventHandler(this.MouseDownHandler);
            this.Resize += new EventHandler(this.OnResize);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            bm = new Bitmap((int)winResX, (int)winResY, PixelFormat.Format48bppRgb);

            // Configure a timer to draw graphics updates.
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 200;
            timer1.Tick += new EventHandler(this.OnTimer);

            bufferingMode = 2;
            count = 0;

            // Retrieves the BufferedGraphicsContext for the 
            // current application domain.
            context = BufferedGraphicsManager.Current;

            // Sets the maximum size for the primary graphics buffer
            // of the buffered graphics context for the application
            // domain.  Any allocation requests for a buffer larger 
            // than this will create a temporary buffered graphics 
            // context to host the graphics buffer.
            context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);

            // Allocates a graphics buffer the size of this form
            // using the pixel format of the Graphics created by 
            // the Form.CreateGraphics() method, which returns a 
            // Graphics object that matches the pixel format of the form.
            grafx = context.Allocate(this.CreateGraphics(),
                 new Rectangle(0, 0, this.Width, this.Height));

            // Draw the first frame to the buffer.
            DrawToBuffer(grafx.Graphics);
        }

        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Cycle the buffering mode.
                if (++bufferingMode > 2)
                    bufferingMode = 0;

                // If the previous buffering mode used 
                // the OptimizedDoubleBuffering ControlStyle,
                // disable the control style.
                if (bufferingMode == 1)
                    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                
                // If the current buffering mode uses
                // the OptimizedDoubleBuffering ControlStyle,
                // enabke the control style.
                if (bufferingMode == 2)
                    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
                    
                // Cause the background to be cleared and redraw.
                count = 6;
                DrawToBuffer(grafx.Graphics);
                this.Refresh();
            }
            else
            {
                // Toggle whether the redraw timer is active.
                if (timer1.Enabled)
                    timer1.Stop();
                else
                    timer1.Start();
            }
        }

        private void OnTimer(object sender, EventArgs e)
        {
            //DrawToBuffer(grafx.Graphics);
            Refresh();
        }

        private void OnResize(object sender, EventArgs e)
        {
            // Re-create the graphics buffer for a new window size.
            context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
            if (grafx != null)
            {
                grafx.Dispose();
                grafx = null;
            }
            grafx = context.Allocate(this.CreateGraphics(),
                new Rectangle(0, 0, this.Width, this.Height));

            // Cause the background to be cleared and redraw.
            count = 6;
            DrawToBuffer(grafx.Graphics);
            this.Refresh();
        }

        private void DrawLine(VectCartesien pt0, VectCartesien pt1, Color color)
        {
            double x0 = pt0[0];
            double x1 = pt1[0];
            double y0 = pt0[1];
            double y1 = pt1[1];

            double dx = Math.Abs(x1 - x0);
            double sx = x0 < x1 ? 1 : -1;
            double dy = -Math.Abs(y1 - y0);
            double sy = y0 < y1 ? 1 : -1;

            if (Math.Min(Math.Abs(dx), Math.Abs(dy)) == Math.Abs(dx))
                sx = sx * Math.Abs(dx / dy);
            else sy = sy * Math.Abs(dy / dx);

            while ((int) Math.Round(x0) != (int)x1 && (int) Math.Round(y0) != (int)y1)
            {
                x0 += sx;
                y0 += sy;
                bm.SetPixel((int)x0, (int)y0, color);
            }
        }

        private void DrawTriangleLine(Triangle triangle)
        {
            VectCartesien[] vertices = triangle.getVertices();
            VectCartesien[] ptsEcran = new VectCartesien[3];

            for (int i = 0; i < 3; i++)
            {
                ptsEcran[i] = renderingTransformation.placePointSurEcran(vertices[i]);
                Console.WriteLine("PtEcran = " + ptsEcran[i]);
            }

            for (int i = 0; i < 3; i++)
            { 
                bm.SetPixel((int)ptsEcran[i][0], (int)ptsEcran[i][1], Color.Yellow);
                DrawLine(ptsEcran[i], ptsEcran[(i + 1) % 3], Color.Green);
            }
        }

        private void DrawTriangleFill(Triangle untransformedTriangle)
        {
            VectCartesien[] vertices = untransformedTriangle.getVertices();
            VectCartesien[] ptsEcran = new VectCartesien[3];

            for (int i = 0; i < 3; i++)
            {
                ptsEcran[i] = renderingTransformation.placePointSurEcran(vertices[i]);
                Console.WriteLine("PtEcran = " + ptsEcran[i]);
            }

            Triangle triangle = new Triangle(ptsEcran[0], ptsEcran[1], ptsEcran[2]);

            AABB aabb = new AABB(triangle.getVertices());
            VectCartesien min = aabb.getMin();
            VectCartesien max = aabb.getMax();
            int iMin = Convert.ToInt32(min[0]);
            int jMin = Convert.ToInt32(min[1]) + 1;
            int iMax = Convert.ToInt32(max[0]);
            int jMax = Convert.ToInt32(max[1]) + 1;
            //Console.WriteLine("iMin = " + iMin + " , iMax = " + iMax);
            //Console.WriteLine("jMin = " + iMin + " , jMax = " + jMax);
            for (int i = iMin; i < iMax; i++)
                for (int j = jMin; j < jMax; j++)
                {
                    //Console.WriteLine("i = " + i + " , j = " + j);
                    VectCartesien ptFenetre = new VectCartesien(i, j);
                    VectCartesien ptBarycentrique = triangle.ToBarycentrique2D(ptFenetre);
                    //Console.WriteLine("ptBarycentrique = " + ptBarycentrique);
                    //Console.WriteLine("IS IN = " + triangle.ptBarycentriqueIsIn(ptBarycentrique));
                    if (triangle.ptBarycentriqueIsIn(ptBarycentrique))
                        bm.SetPixel(i, j, Color.BlueViolet);
                    //else bm.SetPixel(i, j, Color.Orange);
                }
        }

        private void DrawTriangle(Triangle triangle)
        {
            if (renderingMode == RenderingMode.Line)
                DrawTriangleLine(triangle);
            else DrawTriangleFill(triangle);
        }

        private void DrawToBuffer(Graphics g)
        {
            // Clear the graphics buffer every update.
            if (++count > 1)
            {
                count = 0;
                grafx.Graphics.FillRectangle(Brushes.Black, 0, 0, this.Width, this.Height);
            }

            double winResX = 512;
            double winResY = 288;
            double fovX = 80 * Math.PI / 180;
            double fovY = 80 * Math.PI / 180;
            VectCartesien cameraPos = new VectCartesien(6, -4, 5);
            VectCartesien cameraCible = new VectCartesien(3, 1, -8);

            //VectCartesien cameraPos = new VectCartesien(0, 0, 0);
            //VectCartesien cameraCible = new VectCartesien(0, 5, -10);

            renderingTransformation = new RenderingTransformation(cameraPos, cameraCible, winResX, winResY, fovX, fovY);

            VectCartesien pointTest = cameraCible + new VectCartesien(0, 1, 2);
            VectCartesien pScreen = renderingTransformation.placePointSurEcran(pointTest);
            Console.WriteLine("buff pScreen1 = " + pScreen);

            //Triangle triangle = new Triangle(cameraCible + new VectCartesien(0, 1, 2), cameraCible + new VectCartesien(-10,-10,11), cameraCible + new VectCartesien(6,50,60));
            Triangle triangle = new Triangle(new VectCartesien(-0.5,-0.5,0.5), new VectCartesien(-0.5,0.5,0.5), new VectCartesien(0.5,0.5,0.5));
            DrawTriangle(triangle);

            //bm.SetPixel((int)pScreen[0], (int)pScreen[1], Color.Red);
            //bm.SetPixel(0, 0, Color.Yellow);

            //for (int Xcount = ((int)pScreen[0]) - 10; Xcount < (int)pScreen[0] + 10; Xcount++)
            //{
            //    for (int Ycount = ((int)pScreen[1]) - 10; Ycount < (int)pScreen[1] + 10; Ycount++)
            //    {
            //        bm.SetPixel(Xcount, Ycount, Color.BlueViolet);
            //    }
            //}

            //VectCartesien pointTest2 = cameraCible + new VectCartesien(-4, -2, -10);
            //VectCartesien pScreen2 = rasterization.placePointSurEcran(pointTest2);
            //Console.WriteLine("buff pScreen2 = " + pScreen2);

            Random rnd = new Random();
            /* for (int i = 0; i < this.Height; i++)
                 for (int j = 0; j < this.Height; j++)
                 {
                     int px = rnd.Next(20, this.Width - 40);
                     int py = rnd.Next(20, this.Height - 40);
                     (Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)), 1);
                     for (int y = 0; y < this.Height; y++)
                         for (int x = 0; x < this.Width; x++)
                             pixels[y, x] = colorData;
                 }*/

            //for (int Xcount = 0; Xcount < bm.Width; Xcount++)
            //{
            //    for (int Ycount = 0; Ycount < bm.Height; Ycount++)
            //    {
            //        bm.SetPixel(Xcount, Ycount, Color.Black);
            //    }
            //}

        }

        //bm.SetPixel(Xcount, Ycount, Color.DarkViolet);

        protected override void OnPaint(PaintEventArgs e)
        {
            grafx.Render(e.Graphics);
            e.Graphics.DrawImage(bm, 0, 0, bm.Width, bm.Height);
        }

        /*[STAThread]
        public static void Main(string[] args)
        {
            Application.Run(new BufferingExample());
        }*/
    }
}