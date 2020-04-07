using System;

namespace WorldGenerator2DClassLib
{
    public class SquareGradient : NoiseFilterBase
    {
        public SquareGradient(int width, int height) : base(width, height)
        {
            Name = "Square gradient";
        }

        public override float[] ComputeGradient()
        {
            CalculateSquareGradient(ref gradient);
            Utility.MapBetweenZeroAndOne(ref gradient);

            return gradient;
        }

        private void CalculateSquareGradient(ref float[] gradiant)
        {
            int width = Width;
            int height = Height;
            int halfWidth = Width / 2;
            int halfHeight = Height / 2;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    int x = i;
                    int y = j;

                    float colorValue;

                    x = x > halfWidth ? width - x : x;
                    y = y > halfHeight ? height - y : y;

                    int smaller = Math.Min(x, y);
                    colorValue = smaller / (float)halfWidth;

                    colorValue = 1 - colorValue;
                    colorValue *= colorValue * colorValue;

                    gradiant[j * Width + i] = colorValue;
                }
            }
        }
    }
}
