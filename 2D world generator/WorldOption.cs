using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WorldGenerator2D
{
    public class WorldOption
    {
        public int Width = 256;
        public int Height = 256;
        public float Thresold = 0f;
        public Dictionary<float, Color> WorldColor = new Dictionary<float, Color>();

        public WorldOption()
        {

        }

        public WorldOption(int width, int height, float thresold, Dictionary<float, Color> worldColor)
        {
            Width = width;
            Height = height;
            Thresold = thresold;
            WorldColor = worldColor;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("width {0}\nheight {1}\nthresold {2}\n", Width, Height, Thresold));
            foreach (KeyValuePair<float, Color> item in WorldColor)
            {
                sb.Append(string.Format("level {0} {1}\n", item.Key, item.Value));
            }
            return sb.ToString();
        }
    }
}
