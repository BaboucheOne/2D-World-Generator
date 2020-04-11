namespace WorldGenerator2DClassLib
{
    public class SimplexFractalGradient : NoiseFilterBase
    {
        public SimplexFractalGradient() : base()
        {
            Name = "Simplex Fractal";
        }

        public SimplexFractalGradient(int width, int height) : base(width, height)
        {
            Name = "Simplex Fractal";
        }

        public override float[] ComputeGradient()
        {
            float scale = (float)gradientArguments[0];

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int index = y * Width + x;
                    gradient[index] = fastNoise.GetSimplexFractal((float)x / Width * scale, 0, (float)y / Height * scale);
                }
            }

            return gradient;
        }
    }
}
