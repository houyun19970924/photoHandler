using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoHandler
{
    class PhotoManger
    {
        //设置色相
        public Bitmap setColorFilter(int index, Bitmap tempmap)
        {
            int Height = tempmap.Height;
            int Width = tempmap.Width;
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            Bitmap MyBitmap = tempmap;

            BitmapData oldData = MyBitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData newData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* pin = (byte*)(oldData.Scan0.ToPointer());
                byte* pout = (byte*)(newData.Scan0.ToPointer());
                for (int y = 0; y < oldData.Height; y++)
                {
                    for (int x = 0; x < oldData.Width; x++)
                    {
                        for (int i = 0; i < 3; i++)
                            if (i == index)
                                pout[i] = pin[i];
                            else
                                pout[i] = 0;
                        pin = pin + 3;
                        pout = pout + 3;
                    }
                    pin += oldData.Stride - oldData.Width * 3;
                    pout += newData.Stride - newData.Width * 3;
                }

                bitmap.UnlockBits(newData);
                MyBitmap.UnlockBits(oldData);
                return bitmap;
            }
        }

        //翻转图片
        public Bitmap RevPicH(Bitmap tempmap)
        {
            int Height = tempmap.Height;
            int Width = tempmap.Width;
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            Bitmap MyBitmap = tempmap;

            BitmapData oldData = MyBitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData newData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* pin = (byte*)(oldData.Scan0.ToPointer());
                byte* pout = (byte*)(newData.Scan0.ToPointer()) + (Height - 1) * newData.Stride;
                for (int y = 0; y < oldData.Height; y++)
                {
                    for (int x = 0; x < oldData.Width; x++)
                    {
                        for (int i = 0; i < 3; i++)
                            pout[i] = pin[i];
                        pin = pin + 3;
                        pout = pout + 3;
                    }
                    pin += oldData.Stride - oldData.Width * 3;
                    pout -= (newData.Stride + newData.Width * 3);
                }

                bitmap.UnlockBits(newData);
                MyBitmap.UnlockBits(oldData);
                return bitmap;
            }
        }

        //翻转图片
        public Bitmap RevPicW(Bitmap tempmap)
        {
            int Height = tempmap.Height;
            int Width = tempmap.Width;
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            Bitmap MyBitmap = tempmap;

            BitmapData oldData = MyBitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData newData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* pin = (byte*)(oldData.Scan0.ToPointer());
                byte* pout = (byte*)(newData.Scan0.ToPointer());
                for (int y = 0; y < oldData.Height; y++)
                {
                    pout += (Width - 1) * 3;
                    for (int x = 0; x < oldData.Width; x++)
                    {
                        for (int i = 0; i < 3; i++)
                            pout[i] = pin[i];
                        pin = pin + 3;
                        pout = pout - 3;
                    }
                    pin += oldData.Stride - oldData.Width * 3;
                    pout += newData.Stride + 3;
                }

                bitmap.UnlockBits(newData);
                MyBitmap.UnlockBits(oldData);
                return bitmap;
            }
        }

        //顺时针旋转图片
        public Bitmap rotPicClo(Bitmap tempmap)
        {
            int Height = tempmap.Height;
            int Width = tempmap.Width;
            Bitmap bitmap = new Bitmap(Height, Width, PixelFormat.Format32bppArgb);
            Bitmap MyBitmap = tempmap;

            BitmapData oldData = tempmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData newData = bitmap.LockBits(new Rectangle(0, 0, Height, Width), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* pin = (byte*)(oldData.Scan0.ToPointer());
                byte* pout = (byte*)(newData.Scan0.ToPointer());
                for (int y = 0; y < oldData.Height; y++)
                {
                    pout += (newData.Width - 1 - y) * 4;
                    for (int x = 0; x < oldData.Width; x++)
                    {
                        for (int i = 0; i < 4; i++)
                            pout[i] = pin[i];
                        pin = pin + 4;
                        pout = pout + newData.Stride;
                    }
                    pout = (byte *)(newData.Scan0.ToPointer());
                }
                bitmap.UnlockBits(newData);
                MyBitmap.UnlockBits(oldData);
                return bitmap;
            }
        }

        //逆时针旋转图片
        public Bitmap rotPicUnClo(Bitmap tempmap)
        {
            int Height = tempmap.Height;
            int Width = tempmap.Width;
            Bitmap bitmap = new Bitmap(Height, Width, PixelFormat.Format32bppArgb);
            Bitmap MyBitmap = tempmap;

            BitmapData oldData = MyBitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData newData = bitmap.LockBits(new Rectangle(0, 0, Height, Width), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* pin = (byte*)(oldData.Scan0.ToPointer());
                byte* pout = (byte*)(newData.Scan0.ToPointer());
                for (int y = 0; y < oldData.Height; y++)
                {
                    pout += y * 4  + (newData.Height - 1) * newData.Stride;
                    for (int x = 0; x < oldData.Width; x++)
                    {
                        for (int i = 0; i < 4; i++)
                            pout[i] = pin[i];
                        pin = pin + 4;
                        pout = pout - newData.Stride;
                    }
                    pout = (byte*)(newData.Scan0.ToPointer());
                }
                bitmap.UnlockBits(newData);
                MyBitmap.UnlockBits(oldData);
                return bitmap;
            }
        }

        //灰度指针法实现
        public Bitmap grayHandler(Bitmap tempmap)
        {
            int Height = tempmap.Height;
            int Width = tempmap.Width;
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            Bitmap MyBitmap = tempmap;

            BitmapData oldData = MyBitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData newData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* pin = (byte*)(oldData.Scan0.ToPointer());
                byte* pout = (byte*)(newData.Scan0.ToPointer());
                for (int y = 0; y < oldData.Height; y++)
                {
                    for (int x = 0; x < oldData.Width; x++)
                    {
                        byte Result = (byte)(pin[0] * 0.1 + pin[1] * 0.2 + pin[2] * 0.7);//加权平均实现灰化
                        for (int i = 0; i < 3; i++)
                            pout[i] = (byte)Result;
                        pin = pin + 3;
                        pout = pout + 3;
                    }
                    pin += oldData.Stride - oldData.Width * 3;
                    pout += newData.Stride - newData.Width * 3;
                }

                bitmap.UnlockBits(newData);
                MyBitmap.UnlockBits(oldData);
                return bitmap;
            }
        }

        //设置真彩色值
        public Bitmap setRGBColor(int[] Index, Bitmap tempmap)
        {
            int Height = tempmap.Height;
            int Width = tempmap.Width;
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            Bitmap MyBitmap = tempmap;

            BitmapData oldData = MyBitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData newData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* pin = (byte*)(oldData.Scan0.ToPointer());
                byte* pout = (byte*)(newData.Scan0.ToPointer());
                for (int y = 0; y < oldData.Height; y++)
                {
                    for (int x = 0; x < oldData.Width; x++)
                    {
                        for(int i = 0; i < 3; i++)
                            pout[i] = (byte)Math.Max(0, Math.Min(pin[i] + Index[i], 255));
                        pin = pin + 3;
                        pout = pout + 3;
                    }
                    pin += oldData.Stride - oldData.Width * 3;
                    pout += newData.Stride - newData.Width * 3;
                }

                bitmap.UnlockBits(newData);
                MyBitmap.UnlockBits(oldData);
                return bitmap;
            }
        }

        //设置亮度值
        public Bitmap setBrightness(int Index, Bitmap tempmap)
        {
            int Height = tempmap.Height;
            int Width = tempmap.Width;
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            Bitmap MyBitmap = tempmap;

            BitmapData oldData = MyBitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData newData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* pin = (byte*)(oldData.Scan0.ToPointer());
                byte* pout = (byte*)(newData.Scan0.ToPointer());
                for (int y = 0; y < oldData.Height; y++)
                {
                    for (int x = 0; x < oldData.Width; x++)
                    {
                        for (int i = 0; i < 3; i++)
                            pout[i] = (byte)Math.Max(0, Math.Min(pin[i] + Index, 255));
                        pin = pin + 3;
                        pout = pout + 3;
                    }
                    pin += oldData.Stride - oldData.Width * 3;
                    pout += newData.Stride - newData.Width * 3;
                }

                bitmap.UnlockBits(newData);
                MyBitmap.UnlockBits(oldData);
                return bitmap;
            }
        }

        //设置对比度
        public Bitmap setContrast(int Index, Bitmap tempmap)
        {
            int Height = tempmap.Height;
            int Width = tempmap.Width;
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            Bitmap MyBitmap = tempmap;

            BitmapData oldData = MyBitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData newData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* pin = (byte*)(oldData.Scan0.ToPointer());
                byte* pout = (byte*)(newData.Scan0.ToPointer());
                for (int y = 0; y < oldData.Height; y++)
                {
                    for (int x = 0; x < oldData.Width; x++)
                    {
                        for (int i = 0; i < 3; i++)
                            pout[i] = (byte)Math.Max(0, Math.Min(((((int)pin[i] / 255.0 - 0.5) * ((Index + 100.0) / 100.0)) + 0.5) * 255, 255));
                        pin = pin + 3;
                        pout = pout + 3;
                    }
                    pin += oldData.Stride - oldData.Width * 3;
                    pout += newData.Stride - newData.Width * 3;
                }

                bitmap.UnlockBits(newData);
                MyBitmap.UnlockBits(oldData);
                return bitmap;
            }
        }
    }
}
