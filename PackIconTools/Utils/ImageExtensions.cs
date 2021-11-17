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
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(ToBitmapSource(drawingImage, width, height)));
                encoder.Save(ms);

                using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ms))
                {
                    return new System.Drawing.Bitmap(bmp);
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

        public static string ImageSuffix(this System.Drawing.Imaging.ImageFormat format)
        {
            if (format == System.Drawing.Imaging.ImageFormat.Bmp)
                return "bmp";
            else if (format == System.Drawing.Imaging.ImageFormat.Jpeg)
                return "jpg";
            else if (format == System.Drawing.Imaging.ImageFormat.Icon)
                return "ico";
            else if (format == System.Drawing.Imaging.ImageFormat.Png)
                return "png";
            return format.ToString();
        }
    }
}
