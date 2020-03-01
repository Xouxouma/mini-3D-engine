using System;
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

            Rasterization rasterization = new Rasterization(cameraPos, cameraCible, winResX, winResY, fovX, fovY);

            VectCartesien pointTest = cameraCible + new VectCartesien(0, 1, 20);
            VectCartesien pScreen = rasterization.placePointSurEcran(pointTest);
            Console.WriteLine("buff pScreen1 = " + pScreen);


            VectCartesien pointTest2 = cameraCible + new VectCartesien(-4, -2, -10);
            VectCartesien pScreen2 = rasterization.placePointSurEcran(pointTest2);
            Console.WriteLine("buff pScreen2 = " + pScreen2);

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

            for (int Xcount = 0; Xcount < bm.Width; Xcount++)
            {
                for (int Ycount = 0; Ycount < bm.Height; Ycount++)
                {
                    bm.SetPixel(Xcount, Ycount, Color.Violet);
                }
            }
        }

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