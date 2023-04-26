using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static int numOfHitsForLvl = 0;
    public static Dictionary<string, int> playerScores = new();



    public static string MyDictionaryToJson(Dictionary<string, int> dict)
    {
        var entries = dict.Select(d =>
            string.Format("\"{0}\": {1}", d.Key, string.Join(",", d.Value)));
        return "{" + string.Join(",", entries) + "}";
    }
    //var values = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);

    public static void ResetVariables()
    {
        numOfHitsForLvl = 0;
        playerScores.Clear();
    }
}
