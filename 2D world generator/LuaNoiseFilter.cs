using System.IO;
using MoonSharp.Interpreter;

namespace WorldGenerator2D
{
    public sealed class LuaNoiseFilter : NoiseFilterBase
    {
        private Script luaScript;
        private readonly string filePath;

        private string luaFunctionComputeGradient = "gradient";
        private string luaObjectFastnoiseName = "fastNoise";

        public string LastLUAError { get; private set; } = string.Empty;

        public LuaNoiseFilter()
        {
        }

        public LuaNoiseFilter(int width, int height, string luaFilePath) : base(width, height)
        {
            filePath = luaFilePath;

            UserData.RegisterType<FastNoise>();
            luaScript = new Script();
            DynValue noise = UserData.Create(new FastNoise());
            luaScript.Globals.Set(luaObjectFastnoiseName, noise);
        }

        public bool IsFileValid()
        {
            return File.Exists(filePath) && IsLUAScriptValid();
        }

        private bool IsLUAScriptValid()
        {
            try
            {
                string luaCode = File.ReadAllText(filePath);
                luaScript.DoString(luaCode);

                return true;
            }
            catch(ScriptRuntimeException ex)
            {
                LastLUAError = ex.DecoratedMessage;
                return false;
            }
        }

        public override float[] ComputeGradient()
        {
            string code = File.ReadAllText(filePath);
            luaScript.DoString(code);
            gradient = luaScript.Call(luaScript.Globals.Get(luaFunctionComputeGradient), 1000, 1000).ToObject<float[]>();

            return gradient;
        }
    }
}

