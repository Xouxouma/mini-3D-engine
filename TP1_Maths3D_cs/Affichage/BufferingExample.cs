using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Moteur3D
{
    public class BufferingExample : Form
    {
        private Random rnd = new Random();
        private BufferedGraphicsContext context;
        private BufferedGraphics grafx;

        private byte bufferingMode;

        private System.Windows.Forms.Timer timer1;
        private byte count;

        private Bitmap bm;
        private RenderingTransformation renderingTransformation;
        public enum RenderingMode { Line, Fill };
        public RenderingMode renderingMode = RenderingMode.Line;
        private double[,] z_buffer;
        VectCartesien translationCube;
        VectCartesien rotationCube;
        
        double rotation_x;
        double rotation_y;

        //VectCartesien cameraPos = new VectCartesien(6, -4, 5);
        VectCartesien cameraPos = new VectCartesien(4, 3, 3);
        //VectCartesien cameraCible = new VectCartesien(3, 1, -8);
        VectCartesien cameraCible = new VectCartesien(0, 0, 0);

        bool changeTranslation = false;
        bool changeRotation = false;
        int mouseX;
        int mouseY;

        enum TransformObject { Camera, Cube }
        TransformObject transformObject = TransformObject.Camera;

        Polygone cube;
        Polygone cubeColors;

        VectCartesien[] cube_vertices = {
	        new VectCartesien(-0.5,-0.5, 0.5),
            new VectCartesien(-0.5, 0.5, 0.5),
            new VectCartesien(0.5, 0.5, 0.5),
            new VectCartesien(0.5,-0.5, 0.5),
            new VectCartesien(-0.5,-0.5,-0.5),
            new VectCartesien(-0.5, 0.5,-0.5),
            new VectCartesien(0.5, 0.5,-0.5),
            new VectCartesien(0.5,-0.5,-0.5)
        };

        VectCartesien[] cube_colors = {
            new VectCartesien(1.0, 1.0, 0.0, 0.0), // red
            new VectCartesien(1.0, 1.0, 1.0, 0.0), // yellow
            new VectCartesien(1.0, 0.0, 1.0, 0.0), // green
            new VectCartesien(1.0, 0.0, 0.0, 1.0), // blue
            new VectCartesien(1.0, 1.0, 0.0, 1.0), // magenta
            new VectCartesien(1.0, 0.0, 1.0, 1.0), // cyan
            new VectCartesien(1.0, 0.0, 0.0, 0.0), // black
            new VectCartesien(1.0, 1.0, 1.0, 1.0) // white
        };

        public BufferingExample() : base()
        {
            this.Width = 512;
            this.Height = 288;
            // Configure the Form for this example.
            this.Text = "User double buffering";
            this.MouseDown += new MouseEventHandler(this.MouseDownHandler);
            this.MouseUp += new MouseEventHandler(this.MouseUpHandler);
            this.MouseMove += new MouseEventHandler(this.MouseMoveHandler);
            this.Resize += new EventHandler(this.OnResize);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.KeyPreview = true;
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);
            bm = new Bitmap((int)Width, (int)Height, PixelFormat.Format48bppRgb);
            z_buffer = new double[Width, Height];
            // Configure a timer to draw graphics updates.
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 200;
            timer1.Tick += new EventHandler(this.OnTimer);

            bufferingMode = 2;
            count = 0;

            rotation_x = 0;
            rotation_y = 0;
            translationCube = new VectCartesien(0, 0, 0);
            cube = InitCube();
            cubeColors = initCubeColors();
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
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            // Draw the first frame to the buffer.
            DrawToBuffer(grafx.Graphics);
            timer1.Start();
        }

        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Console.WriteLine("Start Rotation");
                changeRotation = true;
                mouseX = e.X;
                mouseY = e.Y;
            }
            else if (e.Button == MouseButtons.Left)
            {
                Console.WriteLine("Start Translation");
                changeTranslation = true;
                mouseX = e.X;
                mouseY = e.Y;
            }
            
        }

        private void MouseUpHandler(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Console.WriteLine("Fin Rotation");
                changeRotation = false;
            }
            else if (e.Button == MouseButtons.Left)
            {
                Console.WriteLine("Fin Translation");
                changeTranslation = false;
            }

        }

        private void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            int newMouseX = e.X;
            int newMouseY = e.Y;
            double diffX = newMouseX - mouseX;
            double diffY = newMouseY - mouseY;
            double dX = (double)diffX / (double)Width;
            double dY = (double)diffY / (double)Height;
            mouseX = newMouseX;
            mouseY = newMouseY;

            if (changeRotation)
            {
                Console.WriteLine("rotation ");
            }
            if (changeTranslation)
            {
                Console.WriteLine("translation");

                VectCartesien translation = new VectCartesien(dX * 5, dY * 5, 0);
                Console.WriteLine(" = " + translation);

                if (transformObject == TransformObject.Camera)
                {
                    cameraPos += translation;
                }
                if (transformObject == TransformObject.Cube)
                {
                    translationCube += translation;
                }
                
            }
            

        }

        private void MouseDownHandlerOriginal(object sender, MouseEventArgs e)
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
                // enable the control style.
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



        private Polygone InitCube()
        {
            Triangle[] triangles = new Triangle[12];
            // Face avant
            triangles[0] = new Triangle(cube_vertices[0], cube_vertices[1], cube_vertices[2]);
            triangles[1] = new Triangle(cube_vertices[0], cube_vertices[3], cube_vertices[2]);
            // Face gauche
            triangles[2] = new Triangle(cube_vertices[0], cube_vertices[1], cube_vertices[5]);
            triangles[3] = new Triangle(cube_vertices[5], cube_vertices[0], cube_vertices[4]);
            // Face doite
            triangles[4] = new Triangle(cube_vertices[3], cube_vertices[2], cube_vertices[6]);
            triangles[5] = new Triangle(cube_vertices[6], cube_vertices[7], cube_vertices[3]);
            // Face arrière
            triangles[6] = new Triangle(cube_vertices[4], cube_vertices[6], cube_vertices[7]);
            triangles[7] = new Triangle(cube_vertices[4], cube_vertices[5], cube_vertices[6]);
            // Face haut
            triangles[8] = new Triangle(cube_vertices[1], cube_vertices[5], cube_vertices[2]);
            triangles[9] = new Triangle(cube_vertices[5], cube_vertices[2], cube_vertices[6]);
            // Face bas
            triangles[10] = new Triangle(cube_vertices[0], cube_vertices[4], cube_vertices[3]);
            triangles[11] = new Triangle(cube_vertices[4], cube_vertices[3], cube_vertices[7]);

            //Console.WriteLine("Triangles = " + triangles);
            Polygone cube = new Polygone(triangles);
            return cube;
        }

        private Polygone initCubeColors()
        {
            Triangle[] triangles = new Triangle[12];
            // Face avant
            triangles[0] = new Triangle(cube_colors[0], cube_colors[1], cube_colors[2]);
            triangles[1] = new Triangle(cube_colors[0], cube_colors[3], cube_colors[2]);
            // Face gauche
            triangles[2] = new Triangle(cube_colors[0], cube_colors[1], cube_colors[5]);
            triangles[3] = new Triangle(cube_colors[5], cube_colors[0], cube_colors[4]);
            // Face doite
            triangles[4] = new Triangle(cube_colors[3], cube_colors[2], cube_colors[6]);
            triangles[5] = new Triangle(cube_colors[6], cube_colors[7], cube_colors[3]);
            // Face arrière
            triangles[6] = new Triangle(cube_colors[4], cube_colors[6], cube_colors[7]);
            triangles[7] = new Triangle(cube_colors[4], cube_colors[5], cube_colors[6]);
            // Face haut
            triangles[8] = new Triangle(cube_colors[1], cube_colors[5], cube_colors[2]);
            triangles[9] = new Triangle(cube_colors[5], cube_colors[2], cube_colors[6]);
            // Face bas
            triangles[10] = new Triangle(cube_colors[0], cube_colors[4], cube_colors[3]);
            triangles[11] = new Triangle(cube_colors[4], cube_colors[3], cube_colors[7]);

            //Console.WriteLine("Triangles = " + triangles);
            Polygone cube = new Polygone(triangles);
            return cube;
        }
        private void OnTimer(object sender, EventArgs e)
        {
            DrawToBuffer(grafx.Graphics);
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
            z_buffer = new double[Width, Height];
            bm = new Bitmap(Width, Height, PixelFormat.Format48bppRgb);
            // Cause the background to be cleared and redraw.
            count = 6;
            DrawToBuffer(grafx.Graphics);
            this.Refresh();
        }

        private void DrawLine(VectCartesien pt0, VectCartesien pt1, Color color, Triangle triangle2D)
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
                if (IsInWindow((int)x0, (int)y0))
                {
                    bm.SetPixel((int)x0, (int)y0, color);
                }
                /*VectCartesien ptFenetre = new VectCartesien(x0, y0);
                VectCartesien ptBarycentrique = triangle2D.ToBarycentrique2D(ptFenetre);
                if (Math.Abs(ptBarycentrique[0]) < 0.000001 || Math.Abs(ptBarycentrique[1]) < 0.000001 || Math.Abs(ptBarycentrique[2]) < 0.000001)
                {
                    if (IsInWindow((int)x0, (int)y0))
                    {
                        bm.SetPixel((int)x0, (int)y0, Color.Violet);
                    }
                }*/
            }
        }

        private void DrawPixel(int i,int j, Color c, double z)
        {
            if (z <= z_buffer[i, j])
            {
                bm.SetPixel(i, j, c);
                z_buffer[i, j] = z;
            }
        }

        private void DrawTriangleLine(Triangle triangle, Matrix model)
        {
            VectCartesien[] vertices = triangle.getVertices();
            VectCartesien[] ptsEcran = new VectCartesien[3];

            for (int i = 0; i < 3; i++)
            {
                ptsEcran[i] = renderingTransformation.placePointSurEcran(vertices[i], model);
                //Console.WriteLine("PtEcran = " + ptsEcran[i]);
            }
            Triangle triangle2D = new Triangle(
                new VectCartesien(ptsEcran[0][0], ptsEcran[0][1]),
                new VectCartesien(ptsEcran[1][0], ptsEcran[1][1]),
                new VectCartesien(ptsEcran[2][0], ptsEcran[2][1])
                );
            for (int i = 0; i < 3; i++)
            {
                if (IsInWindow((int)ptsEcran[i][0], (int)ptsEcran[i][1]))
                {
                    bm.SetPixel((int)ptsEcran[i][0], (int)ptsEcran[i][1], Color.Yellow);
                }
                DrawLine(ptsEcran[i], ptsEcran[(i + 1) % 3], Color.Green, triangle2D);
            }
        }

        private bool IsInWindow(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }

        private void DrawTriangleFill(Triangle untransformedTriangle, Matrix model, Triangle triangleColors = null)
        {
            //Color color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            Color color = Color.BlueViolet;

            VectCartesien[] vertices = untransformedTriangle.getVertices();
            VectCartesien[] ptsEcran = new VectCartesien[3];
            
            for (int i = 0; i < 3; i++)
            {
                ptsEcran[i] = renderingTransformation.placePointSurEcran(vertices[i], model);
                //Console.WriteLine("PtEcran = " + ptsEcran[i]);
            }

            Triangle triangleEcran3D = new Triangle(ptsEcran[0], ptsEcran[1], ptsEcran[2]);
            //Triangle triangle2D = new Triangle(
            //    new VectCartesien(ptsEcran[0][0], ptsEcran[0][1]),
            //    new VectCartesien(ptsEcran[1][0], ptsEcran[1][1]),
            //    new VectCartesien(ptsEcran[2][0], ptsEcran[2][1])
            //    );
            for (int i = 0; i < 3; i++)
            {
                //Console.WriteLine("PtEcran = " + ptsEcran[i]);
                VectCartesien pt2D = new VectCartesien(ptsEcran[i][0], ptsEcran[i][1]);
                //Console.WriteLine("pt2D = " + pt2D);
                VectCartesien ptBarycentriqueTest = triangleEcran3D.ToBarycentrique2D(pt2D);
                //Console.WriteLine("ptBaycentriqueTest = " + ptBarycentriqueTest);
            }
                //VectCartesien ptBarycentriqueBarycentre = triangleEcran3D.ToBarycentrique2D(triangleEcran3D.Barycentre());
                //Console.WriteLine("Barycentre en barycentrique = " + ptBarycentriqueBarycentre+ "\n\n");

            AABB aabb = new AABB(triangleEcran3D.getVertices());
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
                    if (IsInWindow(i, j))
                    {
                        //Console.WriteLine("i = " + i + " , j = " + j);
                        VectCartesien ptFenetre = new VectCartesien(i, j);
                        VectCartesien ptBarycentrique = triangleEcran3D.ToBarycentrique2D(ptFenetre);
                        double z = triangleEcran3D.getZFromBarycentrique(ptBarycentrique);
                        //Console.WriteLine("ptBarycentrique = " + ptBarycentrique);
                        //Console.WriteLine("IS IN = " + triangle.ptBarycentriqueIsIn(ptBarycentrique));
                        if (triangleEcran3D.ptBarycentriqueIsIn(ptBarycentrique))
                        {
                            if (triangleColors != null)
                            {
                                VectCartesien vectColor = triangleColors.FromBarycentrique(ptBarycentrique);
                                color = vectColor.ToArgbColor();
                            }
                            //bm.SetPixel(i, j, color);
                            DrawPixel(i, j, color, z);
                        }
                        //else bm.SetPixel(i, j, Color.Orange);
                    }
                }
        }

        private void DrawTriangle(Triangle triangle, Matrix model, Triangle triangleColors = null)
        {
            if (renderingMode == RenderingMode.Line)
                DrawTriangleLine(triangle, model);
            else DrawTriangleFill(triangle, model, triangleColors);
        }

        private void DrawPolygone(Polygone polygone, Matrix model, Polygone polygoneColors = null)
        {
            Triangle[] triangles = polygone.GetTriangles();
            Triangle[] trianglesColors = polygoneColors.GetTriangles();
            int length = polygone.Length();
            for (int i = 0; i < length; i++)
            {
                DrawTriangle(triangles[i], model, trianglesColors[i]);
            }
        }

        private void DrawToBuffer(Graphics g)
        {
            // Clear the graphics buffer every update.
            /*if (++count > 1)
            {
                count = 0;
                grafx.Graphics.FillRectangle(Brushes.Black, 0, 0, this.Width, this.Height);
            }*/

            // Clear bitmap & z-buffer
            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                {
                    bm.SetPixel(i, j, Color.Black);
                    z_buffer[i, j] = double.MaxValue;
                }

            double fovX = 80 * Math.PI / 180;
            double fovY = 80 * Math.PI / 180;


            //VectCartesien cameraPos = new VectCartesien(0, 0, 0);
            //VectCartesien cameraCible = new VectCartesien(0, 5, -10);

            renderingTransformation = new RenderingTransformation(cameraPos, cameraCible, Width, Height, fovX, fovY);

            //VectCartesien pointTest = cameraCible + new VectCartesien(0, 1, 2);
            //VectCartesien pScreen = renderingTransformation.placePointSurEcran(pointTest);
            //Console.WriteLine("buff pScreen1 = " + pScreen);


            // Construction de la matrice Model : passage coordonnées locales -> Monde
            Matrix model = Matrix.translation(translationCube);
            Matrix rotation = Matrix.rotation_x(rotation_x) * Matrix.rotation_y(rotation_y);
            model *= rotation.increase_dim();

            //Console.WriteLine("Translation = " + Matrix.translation(translation));
            //Console.WriteLine("Rotation_x = " + Matrix.rotation_x(rotation_x));
            //Console.WriteLine("Rotation_y = " + Matrix.rotation_y(rotation_x));
            //Console.WriteLine("Rotation = " + rotation);
            //Console.WriteLine("Rotation icreased = " + rotation.increase_dim());
            //Console.WriteLine("Model = " + model);

            // DRAW FIGURES

            //Triangle triangle = new Triangle(new VectCartesien(-0.5, -0.5, 0.5), new VectCartesien(-0.5, 0.5, 0.5), new VectCartesien(0.5, 0.5, 0.5));
            //DrawTriangle(triangle, model);

            DrawPolygone(cube, model, cubeColors);
        }

        //bm.SetPixel(Xcount, Ycount, Color.DarkViolet);

        protected override void OnPaint(PaintEventArgs e)
        {
            //Console.WriteLine("ONPAINT");
            DrawToBuffer(e.Graphics);
            grafx.Render(e.Graphics);
            e.Graphics.DrawImage(bm, 0, 0, bm.Width, bm.Height);
        }

        
        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("e pressed : " + e.KeyChar);
            switch(e.KeyChar)
            {
                case '1':
                    transformObject = TransformObject.Cube;
                    Console.WriteLine("Transform object : " + transformObject);
                    break;
                case '0':
                    transformObject = TransformObject.Camera;
                    Console.WriteLine("Transform object : " + transformObject);
                    break;
                case 'f':
                    {
                        if (renderingMode == RenderingMode.Line)
                        {
                            Console.WriteLine("Switch to Fill rendering mode");
                            renderingMode = RenderingMode.Fill;
                        }
                        else
                        {
                            Console.WriteLine("Switch to Line rendering mode");
                            renderingMode = RenderingMode.Line;
                        }
                        Console.WriteLine("Rendering mode = " + renderingMode);
                    break;
                    }
                case (char)Keys.Space:
                    Console.WriteLine("Pause");
                    if (timer1.Enabled)
                        timer1.Stop();
                    else timer1.Start();
                    break;
                default:
                    Console.WriteLine("unrecognized command");
                    break;
            }

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Enter key pressed");
            }
        }

        /*[STAThread]
        public static void Main(string[] args)
        {
            Application.Run(new BufferingExample());
        }*/
    }
}