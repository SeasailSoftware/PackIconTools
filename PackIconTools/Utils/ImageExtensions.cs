using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PackIconTools.Utils
{
    public static class ImageExtensions
    {
        public static System.Drawing.Image ToBitmap(this System.Windows.Media.DrawingImage drawingImage, int width, int height)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(ToBitmapSource(drawingImage, width, height)));
                encoder.Save(ms);

                using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms))
                {
                    return ResizeImage(bmp, new System.Drawing.Size(width, height));
                }
            }
        }

        public static BitmapSource ToBitmapSource(DrawingImage source, int width, int height)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            drawingContext.DrawImage(source, new Rect(new Point(0, 0), new Size(width, height)));
            drawingContext.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);
            return bmp;

        }

        /// <summary>
        /// Resizes a square image
        /// </summary>
        /// <param name="OriginalImage">Image to resize</param>
        /// <param name="Size">Width and height of new image</param>
        /// <returns>A scaled version of the image</returns>
        internal static System.Drawing.Image ResizeImage(System.Drawing.Image OriginalImage, System.Drawing.Size Size)
        {
            System.Drawing.Image finalImage = new System.Drawing.Bitmap(Size.Width, Size.Height);

            System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(finalImage);

            graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(0, 0, Size.Width, Size.Height);

            graphic.DrawImage(OriginalImage, rectangle);

            return finalImage;
        }

    }
}
