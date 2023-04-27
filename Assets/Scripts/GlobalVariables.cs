using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static int numOfHitsForLvl = 0;
    public static Dictionary<string, int> playerScores = new();



    public static string MyDictionaryToJson(Dictionary<string, int> dict)
    {
        return JsonConvert.SerializeObject(dict);

        //var entries = dict.Select(d =>
        //    string.Format("\"{0}\": {1}", d.Key, string.Join(",", d.Value)));
        //return "{" + string.Join(",", entries) + "}";
    }



    public static string MyDictionaryToJsonToJson(Dictionary<ulong, Dictionary<string, int>> dict)
    {

        return JsonConvert.SerializeObject(dict);
        //var entries = dict.Select(d =>
        //    string.Format(" {0} : {1}", d.Key, string.Join(",", MyDictionaryToJson(d.Value))));
        //return "{" + string.Join(",", entries) + "}";
    }



    public static void ResetVariables()
    {
        numOfHitsForLvl = 0;
        playerScores.Clear();
    }
}
