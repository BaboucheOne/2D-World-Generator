using System.Collections.Generic;

namespace WorldGenerator2D
{
    public class XMLOption
    {
        public int Width;
        public int Height;
        public float Thresold;
        public List<XMLLevelWorldColor> Worldcolors;

        public XMLOption()
        {

        }

        public XMLOption(int width, int height, float thresold, List<XMLLevelWorldColor> worldcolors)
        {
            Width = width;
            Height = height;
            Thresold = thresold;
            Worldcolors = worldcolors;
        }
    }
}
