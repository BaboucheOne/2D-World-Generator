using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WorldGenerator2DClassLib
{
    public class WorldBuilder
    {
        public WorldOption Option { get; private set; }
        private readonly Dictionary<float, Color> worldColor = new Dictionary<float, Color>();
        private HashSet<NoiseFilterBase> filters = new HashSet<NoiseFilterBase>();

        public WorldBuilder()
        {
            SetOption(new WorldOption(600, 600, 0f, new Dictionary<float, Color>() { { 0f, Color.Black } }));
        }

        public WorldBuilder(WorldOption option)
        {
            SetOption(option);
        }

        public void SetOption(WorldOption option)
        {
            Option = option;
            SetWorldColor(option.WorldColor);

            foreach (NoiseFilterBase filter in filters)
            {
                filter.SetDimension(Option.Width, Option.Height);
            }
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

        public void RemoveAllFilters()
        {
            filters.Clear();
        }

        private void ComputeAllFilters()
        {
            TasksWorldBuilder.ComputeAll(ref filters);
        }

        public Bitmap GetWorldImage()
        {
            if (filters.Count == 0) return null;

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
