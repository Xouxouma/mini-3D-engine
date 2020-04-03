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
        Quaternion rotationCube;

        VectCartesien translationCubeUni;
        Quaternion rotationCubeUni;

        int agrandissement = 1;

        double rotationCube_x;
        double rotationCube_y;

        double rotationCubeUni_x;
        double rotationCubeUni_y;

        //VectCartesien cameraPos = new VectCartesien(6, -4, 5);
        VectCartesien cameraPos = new VectCartesien(4, 3, 3);
        //VectCartesien cameraCible = new VectCartesien(3, 1, -8);
        VectCartesien cameraCible = new VectCartesien(0, 0, 0);

        bool changeTranslation = false;
        bool changeRotation = false;
        int mouseX;
        int mouseY;

        enum TransformObject { Camera, Cube, CubeUni }
        TransformObject transformObject = TransformObject.Camera;

        Polygone cube;
        Polygone cubeColors;
        Polygone cubeUniColors;

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
        private DateTime time;

        public BufferingExample() : base()
        {
            this.Width = 512;
            this.Height = 288;
            // Configure the Form for this example.
            this.Text = "Visualisation algorithmes géométriques - Maxime Grosbois et Pauline Kowalzeyk";
            this.MouseDown += new MouseEventHandler(this.MouseDownHandler);
            this.MouseUp += new MouseEventHandler(this.MouseUpHandler);
            this.MouseMove += new MouseEventHandler(this.MouseMoveHandler);
            this.MouseWheel += new MouseEventHandler(this.MouseWheelHandler);
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

            rotationCube_x = 0;
            rotationCube_y = 0;
            rotationCubeUni_x = 0;
            rotationCubeUni_y = 0;
            translationCube = new VectCartesien(0, 0, 0);
            translationCubeUni = new VectCartesien(0, 0, -1.5);
            rotationCube = new Quaternion();
            rotationCubeUni = new Quaternion();
            cube = InitCube();
            cubeColors = initCubeColors();
            cubeUniColors = initCubeUniColors();
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
          
                rotationCube_x += dX * 5;
                rotationCube_y += dY * 5;
                VectCartesien unitVect = new VectCartesien(1, 0, 0);
                double rad = (rotationCube_x * (Math.PI / 180)) / 2;
               //rotationCube = new Quaternion(Math.Cos(rad), unitVect[0] * Math.Sin(rad), unitVect[1] * Math.Sin(rad), unitVect[2] * Math.Sin(rad));
                rotationCube = Quaternion.FromEuler(rotationCube_x, 0, rotationCube_y);

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

        private void MouseWheelHandler(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Zoom");
            agrandissement = (e.Delta/120)*2;
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

            Polygone cube = new Polygone(triangles);
            return cube;
        }

        private Polygone initCubeUniColors()
        {
            Triangle[] triangles = new Triangle[12];
            // Face avant
            triangles[0] = new Triangle(cube_colors[0], cube_colors[0], cube_colors[0]);
            triangles[1] = new Triangle(cube_colors[0], cube_colors[0], cube_colors[0]);
            // Face gauche
            triangles[2] = new Triangle(cube_colors[1], cube_colors[1], cube_colors[1]);
            triangles[3] = new Triangle(cube_colors[1], cube_colors[1], cube_colors[1]);
            // Face doite
            triangles[4] = new Triangle(cube_colors[2], cube_colors[2], cube_colors[2]);
            triangles[5] = new Triangle(cube_colors[2], cube_colors[2], cube_colors[2]);
            // Face arrière
            triangles[6] = new Triangle(cube_colors[3], cube_colors[3], cube_colors[3]);
            triangles[7] = new Triangle(cube_colors[3], cube_colors[3], cube_colors[3]);
            // Face haut
            triangles[8] = new Triangle(cube_colors[4], cube_colors[4], cube_colors[4]);
            triangles[9] = new Triangle(cube_colors[4], cube_colors[4], cube_colors[4]);
            // Face bas
            triangles[10] = new Triangle(cube_colors[5], cube_colors[5], cube_colors[5]);
            triangles[11] = new Triangle(cube_colors[5], cube_colors[5], cube_colors[5]);

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
                if (IsInWindow((int)x0, (int)y0))
                {
                    bm.SetPixel((int)x0, (int)y0, color);
                }
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

        private void DrawTriangleLine(Triangle triangleEcran)
        {
            VectCartesien[] ptsEcran = triangleEcran.getVertices();

            for (int i = 0; i < 3; i++)
            {
                if (IsInWindow((int)ptsEcran[i][0], (int)ptsEcran[i][1]))
                {
                    bm.SetPixel((int)ptsEcran[i][0], (int)ptsEcran[i][1], Color.Yellow);
                }
                DrawLine(ptsEcran[i], ptsEcran[(i + 1) % 3], Color.Green);
            }
        }

        private bool IsInWindow(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }

        private void DrawTriangleFill(Triangle triangleEcran3D, Triangle triangleColors = null)
        {
            //Color color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            Color color = Color.BlueViolet;

            AABB aabb = new AABB(triangleEcran3D.getVertices());
            VectCartesien min = aabb.getMin();
            VectCartesien max = aabb.getMax();
            int iMin = Convert.ToInt32(min[0]);
            int jMin = Convert.ToInt32(min[1]) + 1;
            int iMax = Convert.ToInt32(max[0]);
            int jMax = Convert.ToInt32(max[1]) + 1;

            for (int i = iMin; i < iMax; i++)
                for (int j = jMin; j < jMax; j++)
                {
                    if (IsInWindow(i, j))
                    {
                        VectCartesien ptFenetre = new VectCartesien(i, j);
                        VectCartesien ptBarycentrique = triangleEcran3D.ToBarycentrique2D(ptFenetre);
                        double z = triangleEcran3D.getZFromBarycentrique(ptBarycentrique);
                        if (triangleEcran3D.ptBarycentriqueIsIn(ptBarycentrique))
                        {
                            if (triangleColors != null)
                            {
                                VectCartesien vectColor = triangleColors.FromBarycentrique(ptBarycentrique);
                                color = vectColor.ToArgbColor();
                            }
                            DrawPixel(i, j, color, z);
                        }
                    }
                }
        }

        private void DrawTriangle(Triangle triangle, VectCartesien translation, Quaternion rotation, int agrandissement, Triangle triangleColors = null)
        {
            Triangle triangleEcran3D = renderingTransformation.placeTriangleSurEcran(triangle, translation, rotation, agrandissement);
            if (renderingMode == RenderingMode.Line)
                DrawTriangleLine(triangleEcran3D);
            else DrawTriangleFill(triangleEcran3D, triangleColors);
        }

        private void DrawPolygone(Polygone polygone, VectCartesien translation, Quaternion rotation, int agrandissement, Polygone polygoneColors = null)
        {
            Triangle[] triangles = polygone.GetTriangles();
            Triangle[] trianglesColors = polygoneColors.GetTriangles();
            int length = polygone.Length();
            for (int i = 0; i < length; i++)
            {
                DrawTriangle(triangles[i], translation, rotation, agrandissement, trianglesColors[i]);
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

            renderingTransformation = new RenderingTransformation(cameraPos, cameraCible, Width, Height, fovX, fovY);
            DrawPolygone(cube, translationCube, rotationCube, agrandissement, cubeColors);
            DrawPolygone(cube, translationCubeUni, rotationCubeUni, agrandissement, cubeUniColors);

            rotationCubeUni_x += 15;
            VectCartesien unitVect = new VectCartesien(1, 0, 0);
            double rad = (rotationCubeUni_x * (Math.PI / 180)) / 2;
            //rotationCubeUni = new Quaternion(Math.Cos(rad), unitVect[0] * Math.Sin(rad), unitVect[1] * Math.Sin(rad), unitVect[2] * Math.Sin(rad));
            rotationCubeUni = Quaternion.FromEuler(new AngleEuler(rad,0,0));
            agrandissement = 1;
        }

        int computeFps()
        {
            DateTime newTime = DateTime.Now;
            TimeSpan span = newTime - time;
            double sec = span.TotalSeconds;
            time = newTime;
            double fps = 1.0 / sec;
            //return (int) fps;
            return (int)span.TotalMilliseconds;
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            //Console.WriteLine("ONPAINT");
            this.Text = "Rendu graphique - " + computeFps() + "FPS";
            DrawToBuffer(e.Graphics);
            grafx.Render(e.Graphics);
            e.Graphics.DrawImage(bm, 0, 0, bm.Width, bm.Height);
        }

        
        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("e pressed : " + e.KeyChar);
            switch(e.KeyChar)
            {
                case '0':
                    transformObject = TransformObject.Camera;
                    Console.WriteLine("Transform object : " + transformObject);
                    break;
                case '1':
                    transformObject = TransformObject.Cube;
                    Console.WriteLine("Transform object : " + transformObject);
                    break;
                case '2':
                    transformObject = TransformObject.CubeUni;
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


        /*[STAThread]
        public static void Main(string[] args)
        {
            Application.Run(new BufferingExample());
        }*/
    }
}