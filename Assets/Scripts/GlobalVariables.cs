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
    }



    public static string MyDictionaryToJsonToJson(Dictionary<ulong, Dictionary<string, int>> dict)
    {

        return JsonConvert.SerializeObject(dict);

    }



    public static void ResetVariables()
    {
        numOfHitsForLvl = 0;
        playerScores.Clear();
    }
}
