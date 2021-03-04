using GameLauncher.App.Classes.LauncherCore.FileReadWrite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GameLauncher.App.Classes.LauncherCore.Validator
{
    class IsValid
    {
        /* Emails */
        public static bool Email(string email)
        {
            if (String.IsNullOrEmpty(email)) return false;

            String EmailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
               + "@"
               + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

            return Regex.IsMatch(email, EmailPattern);
        }

        public static string EmailMask(string email)
        {
            string Pattern = @"(?<=[\w]{1})[\w-\._\+%]*(?=[\w]{1}@)";

            return Regex.Replace(email, Pattern, m => new string('*', m.Length));
        }
    }
}
