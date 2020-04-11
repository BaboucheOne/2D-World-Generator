using System.IO;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.CoreLib;

namespace WorldGenerator2DClassLib
{
    public sealed class LuaNoiseFilter : NoiseFilterBase
    {
        private Script luaScript;
        public string FilePath { get; private set; }  = string.Empty;
        private const string fileExtension = ".lua";

        private string luaModuleFastnoiseName = "fastNoise";
        private string luaModuleMathName = "math";

        public string LastLUAError { get; private set; } = string.Empty;

        private const string luaMethodInit = "Init";
        private const string luaMethodCompute = "ComputeFilter";

        public LuaNoiseFilter() : base()
        {
            Name = "Lua gradient";
            InitLua();
        }

        public LuaNoiseFilter(int width, int height) : base(width, height)
        {
            Name = "Lua gradient";
            InitLua();
        }

        public LuaNoiseFilter(string luaFilePath) : base()
        {
            Name = "Lua gradient";
            FilePath = luaFilePath;
            InitLua();
        }

        public LuaNoiseFilter(int width, int height, string luaFilePath) : base(width, height)
        {
            Name = "Lua gradient";
            FilePath = luaFilePath;
            InitLua();
        }

        private void InitLua()
        {
            luaScript = new Script();
            UserData.RegisterType<FastNoise>();
            UserData.RegisterType<MathModule>();
            DynValue noise = UserData.Create(new FastNoise());
            DynValue math = UserData.Create(new MathModule());
            luaScript.Globals.Set(luaModuleFastnoiseName, noise);
            luaScript.Globals.Set(luaModuleMathName, math);
        }

        public void SetFilePath(string filePath)
        {
            if(File.Exists(filePath))
            {
                if(CheckFileExtension(filePath))
                {
                    FilePath = filePath;
                }
            }
        }

        private bool CheckFileExtension(string luaFilePath)
        {
            return Path.GetExtension(luaFilePath) == fileExtension;
        }

        public bool IsLUAScriptValid()
        {
            try
            {
                string luaCode = File.ReadAllText(FilePath);
                luaScript.DoString(luaCode);

                return true;
            }
            catch (SyntaxErrorException ex)
            {
                LastLUAError = ex.DecoratedMessage;
                return false;
            }
        }

        public override float[] ComputeGradient()
        {
            string code = File.ReadAllText(FilePath);
            luaScript.DoString(code);

            luaScript.Call(luaScript.Globals[luaMethodInit]);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    DynValue res = luaScript.Call(luaScript.Globals[luaMethodCompute], x, y);
                    gradient[(y * Width) + x] = (float)res.Number;
                }
            }

            return gradient;
        }
    }
}

