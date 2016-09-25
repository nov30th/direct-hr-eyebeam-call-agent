using System;
using System.Drawing;

namespace DHRSoftphone.SoftphoneAgent_QZJ
{
    public class MeetingImagePic : IDisposable
    {
        protected Bitmap bmp;// = new Bitmap(thumbWidth, thumbHeight);
        private System.Drawing.Graphics gr;
        private const int thumbWidth = 14;
        private const int thumbHeight = 14;

        public MeetingImagePic()
        {
            bmp = new Bitmap(thumbWidth, thumbHeight);
        }

        public Bitmap MakePicByColor(Brush color, string words)
        {
            return MakePicByColor(color, words, (color == Brushes.White || color == Brushes.Yellow) ? Color.Black : Color.White);
        }

        public Bitmap MakePicByColor(Brush color, string words, Color wordsColor)
        {
            //System.Drawing.Image image;
            //int thumbHeight=(srcHeight/srcWidth)*thumbWidth;

            //从Bitmap创建一个System.Drawing.Graphics对象，用来绘制高质量的缩小图。
            gr = System.Drawing.Graphics.FromImage(bmp);
            gr.FillRectangle(color, 0, 0, thumbWidth, thumbHeight);

            //设置 System.Drawing.Graphics对象的SmoothingMode属性为HighQuality
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //下面这个也设成高质量
            gr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //下面这个设成High
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //把原始图像绘制成上面所设置宽高的缩小图
            System.Drawing.Rectangle rectDestination = new System.Drawing.Rectangle(0, 0, thumbWidth, thumbHeight);

            //gr.DrawImage(image, rectDestination, 0, 0, thumbWidth, thumbHeight, GraphicsUnit.Pixel);

            SolidBrush drawBrush = new SolidBrush(wordsColor);
            Font drawFont = new Font("Arial", 3, FontStyle.Regular, GraphicsUnit.Millimeter);
            gr.DrawString(words, drawFont, drawBrush, words.Length > 1 ? -1 : 2, 0);           //保存图像，大功告成！

            //==TEST

            //bmp.Save("C:\\a.bmp");
            return bmp;
        }

        protected virtual void Dispose(bool isLocal)
        {
            if (isLocal == false)
            {
                gr.Dispose();
                bmp.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}