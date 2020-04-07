using System.Linq;

namespace WorldGenerator2DClassLib
{
    public static class Utility
    {
        public static void MapBetweenZeroAndOne(ref float[] array)
        {
            float maxGradiant = array.Max();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] /= maxGradiant;
            }
        }
    }
}
