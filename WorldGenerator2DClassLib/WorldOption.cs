using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WorldGenerator2DClassLib
{
    public class WorldOption
    {
        public int Width { get; private set; } = 256;
        public int Height { get; private set; } = 256;
        public float Thresold { get; private set; } = 0f;
        public Dictionary<float, Color> WorldColor { get; private set; } = new Dictionary<float, Color>();

        public WorldOption()
        {

        }

        public WorldOption(int width, int height, float thresold = 0f)
        {
            Width = width;
            Height = height;
            Thresold = thresold;
        }

        public WorldOption(int width, int height, float thresold, Dictionary<float, Color> worldColor)
        {
            if (width < 0 || height < 0)
            {
                throw new InvalidImageScaleException(width, height);
            }

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

    public class InvalidImageScaleException : Exception
    {
        public InvalidImageScaleException()
        {
        }

        public InvalidImageScaleException(int width, int height)
        : base(string.Format("Scale cannot be less than 0. widht {0} height {1}", width, height))
        {

        }
    }
}
