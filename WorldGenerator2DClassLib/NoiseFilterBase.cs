using System;
using System.Drawing;
using System.Numerics;

namespace WorldGenerator2DClassLib
{
    public abstract class NoiseFilterBase
    {
        public string Name = "Filter";
        private Vector2 dimension = new Vector2();
        protected FastNoise fastNoise = new FastNoise();
        protected float[] gradient;

        protected object[] gradientArguments;

        public NoiseFilterBase()
        {
        }

        public NoiseFilterBase(int width, int height)
        {
            SetDimension(width, height);
        }

        public void SetDimension(int width, int height)
        {
            dimension.X = width;
            dimension.Y = height;
            gradient = new float[width * height];
        }

        public void SetGradient(int index, float n)
        {
            gradient[index] = n;
        }

        public void SetSeed(int seed)
        {
            fastNoise.SetSeed(seed);
        }

        public void SetRandomSeed(int min, int max)
        {
            Random r = new Random();
            fastNoise.SetSeed(r.Next(min, max));
        }

        public void SetFrequency(float frequency)
        {
            fastNoise.SetFrequency(frequency);
        }

        public void SetFractalOctaves(int octaves)
        {
            fastNoise.SetFractalOctaves(octaves);
        }

        public void SetLacunarity(float octaves)
        {
            fastNoise.SetFractalLacunarity(octaves);
        }

        public void SetFractalGain(float gain)
        {
            fastNoise.SetFractalGain(gain);
        }

        public int Seed => fastNoise.GetSeed();

        public float[] GetGradient()
        {
            return gradient;
        }

        public bool IsLUAScript => this is LuaNoiseFilter;

        public int Width => (int)dimension.X;
        public int Height => (int)dimension.Y;
        public Vector2 Dimension => dimension;

        public void SetGradientArgruments(params object[] args)
        {
            gradientArguments = args;
        }

        public abstract float[] ComputeGradient();

        public virtual void Initialize()
        {
        }

        public Bitmap GenerateImage()
        {
            if (gradient == null) return null;

            Bitmap img = new Bitmap((int)Dimension.X, (int)Dimension.Y);
            for (int x = 0; x < Dimension.X; x++)
            {
                for (int y = 0; y < Dimension.Y; y++)
                {
                    float sample = gradient[y * (int)Dimension.X + x] * 255f;
                    if (sample < 0) sample = 0; //FIXME : c est nul.
                    img.SetPixel(x, y, Color.FromArgb((int)sample, (int)sample, (int)sample));
                }
            }

            return img;
        }
    }
}
