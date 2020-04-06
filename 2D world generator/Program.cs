using System.Drawing;
using System.Numerics;

namespace WorldGenerator2D
{
    //TODO: Separer world builder en plusieurs classe. Une landBuilder et une autre worldColorer.
    //TODO: Refactor et separer les taches de la temperature.
    //TODO: Implementer les biomes.
    //UNDONE: Verifier si une class static est vraiment bien pour faire des tasks.
    //TODO: Ajouter une maniere de verifer tout les scripts lua sont bon. Ca prend bcp de ressource RAM !
    //IDEA: Cree un systeme pour voir si un filtre a ete modifie.

    class Program
    {
        static void Main(string[] args)
        {
            WorldOption option = WorldOptionFactory.Load("Option.xml");
            WorldBuilder worldBuilder = new WorldBuilder(option);

            RadialGradient radialGradient = new RadialGradient(option.Width, option.Height);
            SimplexFractalGradient fractalGradient = new SimplexFractalGradient(option.Width, option.Height);

            LuaNoiseFilter luaNoiseFilter = new LuaNoiseFilter(option.Width, option.Height, "noiseLua.lua");

            radialGradient.SetGradientArgruments(new Vector2(option.Width / 2, option.Height / 2), 200f);
            fractalGradient.SetGradientArgruments(350f);

            worldBuilder.AddFilter(radialGradient);
            worldBuilder.AddFilter(fractalGradient);
            worldBuilder.AddFilter(luaNoiseFilter);

            Bitmap img = worldBuilder.GetWorldImage();
            img.Save(string.Format("result/worldBuilder.bmp"));
        }
    }
}
