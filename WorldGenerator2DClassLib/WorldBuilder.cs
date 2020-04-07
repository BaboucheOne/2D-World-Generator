using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WorldGenerator2DClassLib
{
    public class WorldBuilder
    {
        public WorldOption Option;
        private readonly Dictionary<float, Color> worldColor = new Dictionary<float, Color>();
        private HashSet<NoiseFilterBase> filters = new HashSet<NoiseFilterBase>();

        public WorldBuilder(WorldOption option)
        {
            Option = option;
            SetWorldColor(option.WorldColor);
        }

        public void SetWorldColor(Dictionary<float, Color> colors)
        {
            worldColor.Clear();
            AddThresoldToLevel(colors);
            colors = new Dictionary<float, Color>(worldColor);
            worldColor.Clear();
            OrderWorldColorByLevel(colors);
        }

        private void OrderWorldColorByLevel(Dictionary<float, Color> colors)
        {
            foreach (KeyValuePair<float, Color> item in colors.OrderBy(i => i.Key))
            {
                worldColor.Add(item.Key, item.Value);
            }
        }

        private void AddThresoldToLevel(Dictionary<float, Color> colors)
        {
            foreach (float level in colors.Keys)
            {
                worldColor.Add(Option.Thresold + level, colors[level]);
            }
        }

        public void AddFilter(NoiseFilterBase filter)
        {
            filters.Add(filter);
        }

        public void RemoveFilter(NoiseFilterBase filter)
        {
            filters.Remove(filter);
        }

        private void ComputeAllFilters()
        {
            TasksWorldBuilder.ComputeAll(ref filters);
        }

        public Bitmap GetWorldImage()
        {
            ComputeAllFilters();

            Bitmap worldImg = new Bitmap(Option.Width, Option.Height);

            float[] world = WorldGenerator.GenerateWorldHeightmap(Option.Width, Option.Height, filters.ToList());

            for (int x = 0; x < Option.Width; x++)
            {
                for (int y = 0; y < Option.Height; y++)
                {
                    WorldGenerator.ColorWorld(x, y, ref worldImg, world, worldColor);
                }
            }

            return worldImg;
        }
    }
}
