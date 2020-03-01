using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Moteur3D
{
    class AffichageMain
    {
        public static void exec()
        {
            Console.WriteLine("Start Affichage");

            double winResX = 512;
            double winResY = 288;
            double fovX = 80 * Math.PI / 180 ;
            double fovY = 80 * Math.PI / 180;
            VectCartesien cameraPos = new VectCartesien(6, -4, 5);
            VectCartesien cameraCible = new VectCartesien(3, 1, -8);

            //VectCartesien cameraPos = new VectCartesien(0, 0, 0);
            //VectCartesien cameraCible = new VectCartesien(0, 5, -10);

            Rasterization rasterization = new Rasterization(cameraPos, cameraCible, winResX, winResY, fovX, fovY);

            VectCartesien pointTest = cameraCible + new VectCartesien(0, 1, 20);
            VectCartesien pScreen = rasterization.placePointSurEcran(pointTest);
            Console.WriteLine("pScreen1 = " + pScreen);


            VectCartesien pointTest2 = cameraCible + new VectCartesien(-4, -2, -10);
            VectCartesien pScreen2 = rasterization.placePointSurEcran(pointTest2);
            Console.WriteLine("pScreen2 = " + pScreen2);


            Matrix m = new Matrix(
                new VectCartesien(3, -2, 0),
                new VectCartesien(1, 4, 0),
                new VectCartesien(0, 0, 2));
            /*Console.WriteLine("Test inversibilité de m : " + m +" \n Inverse de m : " + m.inverse());
            Console.Write("Test déterminant: " + m.calculDeterminant());
            Console.Write("Test comatrice: " + m.comatrice());
            Console.Write("Test submatrix 0 0 : " + m.subMatrix(1, 2));*/



            Bitmap bitmap = new Bitmap((int)winResX, (int)winResY, PixelFormat.Format48bppRgb);

            /*  BitmapData bmd = bm.LockBits(new Rectangle(0, 0, 10, 10), System.Drawing.Imaging.ImageLockMode.ReadOnly, bm.PixelFormat);

              int PixelSize = 4;
              unsafe
              {
                  for (int y = 0; y < bmd.Height; y++)
                  {
                      byte* row = (byte*)bmd.Scan0 + (y * bmd.Stride);
                      for (int x = 0; x < bmd.Width; x++)
                      {
                          row[x * PixelSize] = 150;
                      }
                  }
              }
              */


            // Draw myBitmap to the screen.
            //e.Graphics.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);

            // Set each pixel in myBitmap to black.
            /*  for (int Xcount = 0; Xcount < bitmap.Width; Xcount++)
              {
                  for (int Ycount = 0; Ycount < bitmap.Height; Ycount++)
                  {
                      bitmap.SetPixel(Xcount, Ycount, Color.BlueViolet);
                  }
              }*/

            BufferingExample buffering = new BufferingExample();

            Application.Run(buffering);

            Console.WriteLine("Fin Affichage");
        }
    }
}
