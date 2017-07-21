using Patagames.Ocr;
using System;
using System.Drawing;
using System.IO;

namespace PatagamesOcrUsage
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string path = @"C:\Users\songz\Desktop\Captcha";
            String[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                Bitmap bitmap = new Bitmap(Bitmap.FromFile(file));

                var sourceBitmap = RemoveBGandGrayColor(bitmap);
                BitmapUtil.ResizeImage(ref sourceBitmap, 300, 0, false);

                string text = GetTextOfNumber(bitmap);
                Console.WriteLine(text + " ---- " + Path.GetFileNameWithoutExtension(file));
            }

            Console.ReadLine();
        }

        /// <summary>
        /// 识别数字验证码（可能会抛出异常）
        /// </summary>
        /// <param name="bitmap">要求经过预处理（去干扰，灰度，补边等）</param>
        /// <returns></returns>
        public static string GetTextOfNumber(Bitmap bitmap)
        {
            string result;

            // 此处的using必须这样使用，否则会出现内存错误
            using (OcrApi api = OcrApi.Create())
            {
                api.Init();
                api.SetVariable("");
                result = api.GetTextFromImage(bitmap).Replace(" ", "").Trim();
            }
            return result;
        }

        private static Bitmap RemoveBGandGrayColor(Bitmap original)
        {
            //make an empty bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    newBitmap.SetPixel(i, j, GetSuiltColor(i, j, original.GetPixel(i, j)));
                }
            }

            return newBitmap;
        }

        /// <summary>
        /// 根据位置和颜色灰度来处理成黑白色
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="originalColor"></param>
        /// <returns></returns>
        private static Color GetSuiltColor(int i, int j, Color originalColor)
        {
            //上下左右边缘颜色置为白色
            if (i < 7 || i > 58 || j < 2 || j > 18) return Color.White;
            //背景颜色浅 置为白色
            int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59) + (originalColor.B * .11));
            if (grayScale > 120) return Color.White;
            //灰度颜色认为是干扰线 置为白色
            if ((Math.Abs(originalColor.R - originalColor.B) + Math.Abs(originalColor.B - originalColor.G) + Math.Abs(originalColor.R - originalColor.G)) < 45) return Color.White;
            return Color.Black;
        }
    }
}