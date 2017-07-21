using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace PatagamesOcrUsage
{
    public static class BitmapUtil
    {
        /// <summary>
        /// 图像尺寸修改
        /// </summary>
        /// <param name="image">原图</param>
        /// <param name="maxWidth">放大图片的最大宽度</param>
        /// <param name="cutStartX">切掉左侧的像素值（使得切割以后的对称）</param>
        /// <param name="needPadding">是否要加内边距（默认不加）</param>
        /// <returns></returns>
        public static void ResizeImage(ref Bitmap image, int maxWidth, int cutStartX, bool needPadding)
        {
            if (image.Width > maxWidth)
            {
                return;
            }

            double iSzie = 300.00 / image.Width;
            int newWidth = (int)(iSzie * image.Width);
            int newHeight = (int)(iSzie * image.Height);

            using (var destImage = new Bitmap(newWidth, newHeight))
            using (var graphics = Graphics.FromImage(destImage))
            using (var wrapMode = new ImageAttributes())
            {
                destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);

                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                var destRect = new Rectangle(0, 0, newWidth, newHeight);
                if (needPadding)
                {
                    // 先用原图左上角的样本像素填充底色
                    graphics.DrawImage(image, destRect, new Rectangle(0, 0, 1, 1), GraphicsUnit.Pixel);

                    // 把原图片绘入底图（内边距留10像素）
                    int padding = 10;
                    graphics.DrawImage(image,
                        new Rectangle(padding, padding, newWidth - padding * 2, newHeight - padding * 2),
                        cutStartX, 0, image.Width - cutStartX, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
                else
                {
                    graphics.DrawImage(image, destRect, cutStartX, 0, image.Width - cutStartX, image.Height,
                        GraphicsUnit.Pixel, wrapMode);
                }

                image = new Bitmap(destImage);
            }
        }

        /// <summary>
        /// 图片灰度处理
        /// </summary>
        /// <param name="original">原图片</param>
        /// <returns></returns>
        public static Bitmap MakeGrayscale(Bitmap original)
        {
            //make an empty bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    Color originalColor = original.GetPixel(i, j);

                    //create the grayscale version of the pixel
                    int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59)
                                           + (originalColor.B * .11));

                    //create the color object
                    Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, newColor);
                }
            }

            return newBitmap;
        }

        /// <summary>
        /// bitmap转byte[]
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static byte[] Bitmap2Bytes(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }

        /// <summary>
        /// 切割图片
        /// </summary>
        /// <param name="fromImage"></param>
        /// <param name="FromImagePointX"></param>
        /// <param name="FromImagePointY"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image CutImage(Image fromImage, int FromImagePointX, int FromImagePointY, int width, int height)
        {

            //创建新图位图   
            Bitmap bitmap = new Bitmap(width, height);
            //创建作图区域   
            Graphics graphic = Graphics.FromImage(bitmap);
            //截取原图相应区域写入作图区   
            graphic.DrawImage(fromImage, FromImagePointX, FromImagePointY, new Rectangle(0, 0, fromImage.Width, fromImage.Height), GraphicsUnit.Point);
            //从作图区生成新图   
            Image saveImage = Image.FromHbitmap(bitmap.GetHbitmap());
            //释放资源   
            bitmap.Dispose();
            graphic.Dispose();
            return saveImage;
        }
    }
}
