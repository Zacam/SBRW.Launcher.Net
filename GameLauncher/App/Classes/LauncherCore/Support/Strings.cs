﻿using System.Text;

namespace GameLauncher.App.Classes.LauncherCore.Support
{
    class Strings
    {
        public static string Encode(string Value)
        {
            return Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(Value));
        }
    }
}
