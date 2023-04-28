using UnityEngine;

public class SyncClientServer : MonoBehaviour
{
    //public static Dictionary<ulong, Dictionary<string, int>> playersAllValues = new();


    //public static List<Color> colors = new()
    //    {
    //         Color.red,
    //         Color.green,
    //         Color.yellow,
    //         Color.magenta,
    //         Color.blue,
    //         Color.cyan,
    //         Color.white,
    //         new Color(255F, 0F, 255F),
    //         new Color(0F, 255F, 255F),
    //         new Color(255F, 255F, 0F),
    //         new Color(128F, 0F, 128F),
    //         new Color(128F, 0F, 0F)
    //    };
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //[ServerRpc(RequireOwnership = false)]
    //public static void SendScoresToServerRpc(FixedString512Bytes scores, ulong id)
    //{
    //    Debug.Log("ServerRpc sent");
    //    playersAllValues.Add(id, JsonConvert.DeserializeObject<Dictionary<string, int>>(scores.ToString()));
    //}
    //[ClientRpc]
    //public static void SendScoresToClientRpc(FixedString512Bytes allScores)
    //{
    //    Debug.Log("ClientRpc sent");
    //    Debug.Log(allScores.ToString());

    //    playersAllValues = JsonConvert.DeserializeObject<Dictionary<ulong, Dictionary<string, int>>>(allScores.ToString());
    //}




    //[ClientRpc]
    //public static void SendColorsToClientRpc(Color[] modifiedColors, ulong id, GameObject go)
    //{
    //    List<Color> colors = modifiedColors.ToList();
    //    go.GetComponentInChildren<SpriteRenderer>().color = colors[(int)id];
    //}


    //[ServerRpc(RequireOwnership = false)]
    //public static void SentColorsToServerRpc(Color color, ulong id, GameObject go)
    //{

    //    List<Color> modifiedColors = colors;



    //    try
    //    {

    //        modifiedColors[(int)id] = color;
    //        SendColorsToClientRpc(modifiedColors.ToArray(), id, go);

    //    }
    //    catch
    //    {
    //        modifiedColors.Add(color);
    //        modifiedColors[(int)id] = color;
    //        SendColorsToClientRpc(modifiedColors.ToArray(), id, go);

    //    }

    //}
}
