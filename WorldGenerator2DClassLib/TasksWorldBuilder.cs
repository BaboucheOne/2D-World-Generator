using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldGenerator2DClassLib
{
    public static partial class TasksWorldBuilder
    {
        public static void ComputeAll(ref HashSet<NoiseFilterBase> filters)
        {
            List<Task<float[]>> tasks = new List<Task<float[]>>();

            foreach (NoiseFilterBase filter in filters)
            {
                Func<float[]> function = new Func<float[]>(() => filter.ComputeGradient());
                tasks.Add(Task.Run(function));
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
