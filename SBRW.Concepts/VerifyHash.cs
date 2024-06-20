using Newtonsoft.Json;
using SBRW.Launcher.Core.Extension.Hash_;
using SBRW.Launcher.Core.Extension.Logging_;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SBRW.Concepts
{
    public static class VerifyHash
    {
        public class Json_List_Game_Files
        {
            /// <summary>
            /// 
            /// </summary>
            public string Hash { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Path_Truncated { get; set; }
        }

        public static void format()
        {
            String[] getFilesToCheck = { };
            List<Json_List_Game_Files> Files = new List<Json_List_Game_Files>();

            if (File.Exists("checksums.dat"))
            {
                /* Read Local checksums.dat */
                getFilesToCheck = File.ReadAllLines("checksums.dat");

                var ScannedHashes = new string[getFilesToCheck.Length][];
                for (var i = 0; i < getFilesToCheck.Length; i++)
                {
                    ScannedHashes[i] = getFilesToCheck[i].Split(' ');
                }

                foreach (string[] file in ScannedHashes)
                {
                    Files.Add(new Json_List_Game_Files() { Hash = file[0].Trim(), Path_Truncated = file[1].Trim() });
                }

                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(Files);

                File.WriteAllText("New_Files.json", json);
            }
        }
    }
}
