using System.IO;
using System.Reflection;
using IniParser;
using IniParser.Model;
using System.Text;

namespace GameLauncherSimplified.App.Classes.LauncherCore.FileReadWrite
{
    class IniFile
    {
        public string Path;
        readonly string EXE = Assembly.GetExecutingAssembly().GetName().Name;
        public FileIniDataParser Parser;
        public IniData Data;

        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName.ToString();
            Parser = new FileIniDataParser();
            if (File.Exists(Path))
            {
                Data = Parser.ReadFile(Path);
            }
            else
            {
                if (!File.Exists(Path))
                {
                    File.Create(Path).Dispose();
                }

                Data = new IniData();
            }
        }

        public string Read(string Key)
        {
            return Data[EXE][Key];
        }

        public void Write(string Key, string Value)
        {
            try
            {
                if (new FileInfo(Path).IsReadOnly == true)
                {
                    Log.Warning("CORE: Settings.ini is ReadOnly");
                }
                else
                {
                    UTF8Encoding utf8 = new UTF8Encoding(false);
                    Data[EXE][Key] = Value;
                    Parser.WriteFile(Path, Data, utf8);
                }
            }
            catch { }
        }

        public void DeleteKey(string Key)
        {
            try
            {
                Data[EXE].RemoveKey(Key);
                Parser.WriteFile(Path, Data);
            }
            catch { }
        }

        public bool KeyExists(string Key)
        {
            return Data[EXE].ContainsKey(Key);
        }
    }
}
