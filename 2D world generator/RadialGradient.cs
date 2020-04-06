using System.Linq;
using System.Numerics;

namespace WorldGenerator2D
{
    public class RadialGradient : NoiseFilterBase
    {
        public RadialGradient(int width, int height) : base(width, height)
        {
            Name = "Radial gradient";
        }

        public override float[] ComputeGradient()
        {
            Vector2 position = (Vector2)gradientArguments[0];
            float radius = (float)gradientArguments[1];

            CalculateGradientDistance(position, gradient);
            RevertGradiant(ref gradient);
            ExpandGradiant(radius, ref gradient);
            Utility.MapBetweenZeroAndOne(ref gradient);

            return gradient;
        }

        private void CalculateGradientDistance(Vector2 position, float[] gradient)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    gradient[y * Width + x] = Vector2.Distance(new Vector2(x, y), position);
                }
            }
        }

        private void RevertGradiant(ref float[] gradiant)
        {
            float maxGradiant = gradiant.Max();
            for (int i = 0; i < gradiant.Length; i++)
            {
                gradiant[i] /= maxGradiant;
                gradiant[i] -= 0.5f;
                gradiant[i] *= -2;
            }
        }

        private void ExpandGradiant(float radius, ref float[] gradiant)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    int index = y * Width + x;

                    if (gradiant[index] > 0)
                    {
                        gradiant[index] *= radius;
                    }
                }
            }
        }
    }
}
