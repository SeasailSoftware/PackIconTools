﻿using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackIconTools.Utils
{
    internal class IconGenerator
    {

        /// <summary>
        /// 图片转换为ico文件
        /// </summary>
        /// <param name="origin">原图片路径</param>
        /// <param name="destination">输出ico文件路径</param>
        /// <param name="iconSize">输出ico图标尺寸，不可大于255x255</param>
        /// <returns>是否转换成功</returns>
        public static bool Save(Image source, string destination, Size iconSize)
        {
            //        if (iconSize.Width > 255 || iconSize.Height > 255)
            //        {
            //            return false;
            //        }
            //        Image image = new Bitmap(source, iconSize); //先读取已有的图片为bitmap，并缩放至设定大小
            //        MemoryStream bitMapStream = new MemoryStream(); //存原图的内存流
            //        MemoryStream iconStream = new MemoryStream(); //存图标的内存流
            //        image.Save(bitMapStream, ImageFormat.Png); //将原图读取为png格式并存入原图内存流
            //        BinaryWriter iconWriter = new BinaryWriter(iconStream); //新建二进制写入器以写入目标图标内存流
            //        /**
            //* 下面是根据原图信息，进行文件头写入
            //*/
            //        iconWriter.Write((short)0);
            //        iconWriter.Write((short)1);
            //        iconWriter.Write((short)1);
            //        iconWriter.Write((byte)image.Width);
            //        iconWriter.Write((byte)image.Height);
            //        iconWriter.Write((short)0);
            //        iconWriter.Write((short)0);
            //        iconWriter.Write((short)32);
            //        iconWriter.Write((int)bitMapStream.Length);
            //        iconWriter.Write(22);
            //        //写入图像体至目标图标内存流
            //        iconWriter.Write(bitMapStream.ToArray());
            //        //保存流，并将流指针定位至头部以Icon对象进行读取输出为文件
            //        iconWriter.Flush();
            //        iconWriter.Seek(0, SeekOrigin.Begin);
            //        Stream iconFileStream = new FileStream(destination, FileMode.Create);
            //        Icon icon = new Icon(iconStream);
            //        icon.Save(iconFileStream); //储存图像
            //        /**
            //* 下面开始释放资源
            //*/
            //        iconFileStream.Close();
            //        iconWriter.Close();
            //        iconStream.Close();
            //        bitMapStream.Close();
            //        icon.Dispose();
            //        image.Dispose();
            //        return File.Exists(destination);

            // Write to stream
            MagickReadSettings settings = new MagickReadSettings();
            // Tells the xc: reader the image to create should be 800x600
            settings.Width = 800;
            settings.Height = 600;

            using (MemoryStream memStream = new MemoryStream())
            {
                using (var ms = new MemoryStream())
                {
                    Image bitmap = new Bitmap(source, iconSize); //先读取已有的图片为bitmap，并缩放至设定大小
                    bitmap.Save(ms, ImageFormat.Png); //将原图读取为png格式并存入原图内存流
                    ms.Seek(0, SeekOrigin.Begin);
                    // Create image that is completely purple and 800x600
                    using (MagickImage image = new MagickImage(ms))
                    {
                        // Sets the output format to png
                        image.Format = GetFormat(destination);
                        // Write the image to the memorystream
                        image.Write(destination);
                        //Stream iconFileStream = new FileStream(destination, FileMode.Create);
                        //Icon icon = new Icon(memStream);
                        //icon.Save(iconFileStream); //储存图像
                    }
                }
            }

            return true;
        }

        private static MagickFormat GetFormat(string destination)
        {
            var temp = destination.Substring(destination.LastIndexOf(".")).ToUpper();
            switch (temp)
            {
                case ".ICO":
                    return MagickFormat.Icon;
                case ".PNG":
                    return MagickFormat.Png;
                case ".JPG":
                    return MagickFormat.Jpeg;
                case ".BMP":
                    return MagickFormat.Bmp;
                default:
                    return MagickFormat.Png;
            }

        }

        /// <summary>
        /// 图片转换为ico文件
        /// </summary>
        /// <param name="origin">原图片路径</param>
        /// <param name="destination">输出ico文件路径</param>
        /// <param name="iconSize">输出ico图标尺寸，不可大于255x255</param>
        /// <returns>是否转换成功</returns>
        public static bool ConvertImageToIcon(string destination, Size iconSize)
        {
            var origin = $@"C:\Users\Administrator\Desktop\图标_背景.jpg";
            if (iconSize.Width > 255 || iconSize.Height > 255)
            {
                return false;
            }
            Image image = new Bitmap(new Bitmap(origin), iconSize); //先读取已有的图片为bitmap，并缩放至设定大小
            MemoryStream bitMapStream = new MemoryStream(); //存原图的内存流
            MemoryStream iconStream = new MemoryStream(); //存图标的内存流
            image.Save(bitMapStream, ImageFormat.Png); //将原图读取为png格式并存入原图内存流
            BinaryWriter iconWriter = new BinaryWriter(iconStream); //新建二进制写入器以写入目标图标内存流
            /**
             * 下面是根据原图信息，进行文件头写入
             */
            iconWriter.Write((short)0);
            iconWriter.Write((short)1);
            iconWriter.Write((short)1);
            iconWriter.Write((byte)image.Width);
            iconWriter.Write((byte)image.Height);
            iconWriter.Write((short)0);
            iconWriter.Write((short)0);
            iconWriter.Write((short)32);
            iconWriter.Write((int)bitMapStream.Length);
            iconWriter.Write(22);
            //写入图像体至目标图标内存流
            iconWriter.Write(bitMapStream.ToArray());
            //保存流，并将流指针定位至头部以Icon对象进行读取输出为文件
            iconWriter.Flush();
            iconWriter.Seek(0, SeekOrigin.Begin);
            Stream iconFileStream = new FileStream(destination, FileMode.Create);
            Icon icon = new Icon(iconStream);
            icon.Save(iconFileStream); //储存图像
            /**
             * 下面开始释放资源
             */
            iconFileStream.Close();
            iconWriter.Close();
            iconStream.Close();
            bitMapStream.Close();
            icon.Dispose();
            image.Dispose();
            return File.Exists(destination);
        }
    }
}
