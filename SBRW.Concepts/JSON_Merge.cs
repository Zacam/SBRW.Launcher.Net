using Newtonsoft.Json;
using SBRW.Concepts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SBRW.Concepts
{
    public class Json_List_Events
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("trackname")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Track { get; set; }
    }

    internal class JSON_Merge
    {
        
        public static void Merge()
        {
            MergeJsonFiles("Old.json", "New.json", "Merged.json");
        }

        public static void MergeJsonFiles(string filePath1, string filePath2, string outputFilePath)
        {
            // Read and deserialize the JSON files
            var json1 = File.ReadAllText(filePath1);
            var json2 = File.ReadAllText(filePath2);

            var list1 = JsonConvert.DeserializeObject<List<Json_List_Events>>(json1);
            var list2 = JsonConvert.DeserializeObject<List<Json_List_Events>>(json2);

            // Merge the lists, excluding duplicates
            var mergedList = list1.Union(list2, new JsonListEventsComparer()).ToList();

            mergedList = mergedList.OrderBy(e => ConvertIdToInt(e.Id)).ToList();

            // Serialize the merged list back to JSON
            var mergedJson = JsonConvert.SerializeObject(mergedList, Formatting.Indented);

            // Write the merged JSON to the output file
            File.WriteAllText(outputFilePath, mergedJson);
        }

        private static int ConvertIdToInt(string id)
        {
            if (int.TryParse(id, out int result))
            {
                return result;
            }
            // If the ID is not a valid integer, treat it as 0 or handle as needed
            return 0;
        }
    }
}

public class JsonListEventsComparer : IEqualityComparer<Json_List_Events>
{
    public bool Equals(Json_List_Events x, Json_List_Events y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;

        return x.Id == y.Id;
    }

    public int GetHashCode(Json_List_Events obj)
    {
        if (ReferenceEquals(obj, null)) return 0;

        return obj.Id == null ? 0 : obj.Id.GetHashCode();
    }
}
