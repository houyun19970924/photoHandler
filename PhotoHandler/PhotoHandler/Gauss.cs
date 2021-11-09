using System;
using System.Drawing;

namespace PhotoHandler
{
    struct WeightedColor
    {
        public double R;
        public double G;
        public double B;
        public static WeightedColor FromRGB(double r, double g, double b)
        {
            WeightedColor we = new WeightedColor();
            we.R = r; we.G = g; we.B = b;
            return we;
        }
    }

    class Gauss
    {
        private double[,] GAUSS; 
        private const int FI = 2;

        private void IniGauss(int fi)
        {
            GAUSS = new double[fi * 2 + 1, fi * 2 + 1];

            for (int x = -fi; x <= fi; x++)
                for (int y = -fi; y <= fi; y++)
                {
                    int sqrtFi = fi * fi;
                    try
                    {
                        double ex = Math.Pow(Math.E, (-(x * x + y * y) / (2 * sqrtFi)));
                        double result = ex / (2 * Math.PI * sqrtFi);
                        GAUSS[x + fi, y + fi] = result;
                    }
                    catch (Exception) { }
                }
        }

        private WeightedColor[,,] GetWeightedColor(Bitmap source, int fi)
        {

            WeightedColor[,,] retVal = new WeightedColor[fi + 1 + fi, source.Width, source.Height];
            double r = 0, g = 0, b = 0;

            for (int i = 0; i < source.Width; i++)
                for (int j = 0; j < source.Height; j++)
                {
                    Color c = source.GetPixel(i, j);

                    for (int k = 0; k < fi + 1 + fi; k++)
                    {
                        double weight;
                        if (k < fi + 1)
                            weight = GAUSS[fi, fi + k];
                        else
                            weight = GAUSS[k, k];
                        r = c.R * weight;
                        g = c.G * weight;
                        b = c.B * weight;
                        r = r > 255 ? 255 : r;
                        g = g > 255 ? 255 : g;
                        b = b > 255 ? 255 : b;
                        retVal[k, i, j] = WeightedColor.FromRGB(r, g, b);
                    }
                }
            return retVal;
        }

        private Color GetFilteredColor(Bitmap source, int x, int y, int fi, WeightedColor[,,] colorMap)
        {
            int w = source.Width, h = source.Height;
            double r = 0, g = 0, b = 0;

            for (int u = x - fi; u <= x + fi; u++)
                for (int v = y - fi; v <= y + fi; v++)
                {
                    if (u >= 0 && u < w && v >= 0 && v < h)
                    {
                        int wx = Math.Abs(u - x), wy = Math.Abs(v - y);
                        int wVersion;
                        if (wx == wy && wx != 0)
                            wVersion = fi + wx;
                        else
                            wVersion = wx > wy ? wx : wy;
                        WeightedColor tmpC = colorMap[wVersion, u, v];
                        r += tmpC.R;
                        g += tmpC.G;
                        b += tmpC.B;
                    }
                }
            r = r > 255 ? 255 : r;
            g = g > 255 ? 255 : g;
            b = b > 255 ? 255 : b;
            return Color.FromArgb((int)r, (int)g, (int)b);
        }

        public Bitmap GaussianFilt(Bitmap source, int fi)
        {
            IniGauss(fi);
            Bitmap retVal = new Bitmap(source.Width, source.Height);
            WeightedColor[,,] colorMap = GetWeightedColor(source, fi);
            for (int i = 0; i < source.Width; i++)
                for (int j = 0; j < source.Height; j++)
                {
                    Color fColor = GetFilteredColor(source, i, j, fi, colorMap);
                    retVal.SetPixel(i, j, fColor);
                }
            return retVal;
        }
    }
}
