using System.Collections.Generic;
using System.Drawing;

namespace WorldGenerator2DClassLib
{
    public static class WorldGenerator
    {
        public static float[] GenerateWorldHeightmap(int width, int height, List<NoiseFilterBase> filters)
        {
            float[] world = new float[width * height];
            float[][] gradientFilters = new float[filters.Count][];

            for (int i = 0; i < filters.Count; i++)
            {
                gradientFilters[i] = filters[i].GetGradient();
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int index = y * width + x;

                    world[index] = gradientFilters[0][index];
                    for (int i = 1; i < gradientFilters.Length; i++)
                    {
                        world[index] *= gradientFilters[i][index];
                    }
                }
            }

            Utility.MapBetweenZeroAndOne(ref world);

            return world;
        }

        public static void ColorWorld(int x, int y, ref Bitmap image, float[] world, Dictionary<float, Color> worldColor)
        {
            float grey = world[y * image.Width + x];

            foreach (float key in worldColor.Keys)
            {
                if (grey <= key)
                {
                    image.SetPixel(x, y, worldColor[key]);
                    break;
                }
            }
        }

        public static float GetTemperature(int y, int worldHeight, int tempMult, float height, float lossTemp, float temperatureLossEachHeight, float equatorTemperature)
        {
            return (y / worldHeight * -tempMult) - ((height / lossTemp) * temperatureLossEachHeight) + equatorTemperature;
        }

        public static float[] GenerateWorldTemperature(int width, int height, int maxTemperature, ref float[] world)
        {
            float[] temperature = new float[width * height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int index = y * width + x;
                    temperature[index] = GetTemperature(y, height, 1, world[y * width + x] * 25, 0.2f, 0.2f, maxTemperature);
                }
            }

            return temperature;
        }

        public static void ColorTemperature(int x, int y, ref Bitmap image, float[] temperature)
        {
            List<Color> col = GetGradientColors(Color.Blue, Color.Red, 100);
            int index = y * image.Width + x;

            int temp = (int)temperature[index];
            if (temp <= 0)
            {
                image.SetPixel(x, y, Color.Blue);
            }
            else
            {
                image.SetPixel(x, y, col[temp]);
            }
        }


        public static List<Color> GetGradientColors(Color start, Color end, int steps)
        {
            return GetGradientColors(start, end, steps, 0, steps - 1);
        }

        public static List<Color> GetGradientColors(Color start, Color end, int steps, int firstStep, int lastStep)
        {
            var colorList = new List<Color>();
            if (steps <= 0 || firstStep < 0 || lastStep > steps - 1)
                return colorList;

            double aStep = (end.A - start.A) / steps;
            double rStep = (end.R - start.R) / steps;
            double gStep = (end.G - start.G) / steps;
            double bStep = (end.B - start.B) / steps;

            for (int i = firstStep; i < lastStep; i++)
            {
                var a = start.A + (int)(aStep * i);
                var r = start.R + (int)(rStep * i);
                var g = start.G + (int)(gStep * i);
                var b = start.B + (int)(bStep * i);
                colorList.Add(Color.FromArgb(a, r, g, b));
            }

            return colorList;
        }
    }
}
