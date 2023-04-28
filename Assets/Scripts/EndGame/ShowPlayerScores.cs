using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerScores : NetworkBehaviour
{

    public RowScores rowScores;

    private Dictionary<ulong, Dictionary<string, int>> playersAllValues = new();

    private int totalPlayers = 0;

    void Start()
    {
        Debug.Log("ServerRpc sent in start");

        SendScoresToServerRpc(GlobalVariables.MyDictionaryToJson(GlobalVariables.playerScores), NetworkManager.Singleton.LocalClientId);

        if (IsServer)
        {

        }

    }

    void Update()
    {
        if (totalPlayers <= GameObject.FindGameObjectsWithTag("Player").Count())
        {
            if (!playersAllValues.ContainsKey(NetworkManager.Singleton.LocalClientId))
            {
                playersAllValues.Add(NetworkManager.Singleton.LocalClientId, new Dictionary<string, int>(GlobalVariables.playerScores));
                totalPlayers = GameObject.FindGameObjectsWithTag("Player").Count();
                Invoke("UpdatePlayerScores", 0.5f);
            }

        }
    }

    [ServerRpc(RequireOwnership = false)]
    void SendScoresToServerRpc(FixedString512Bytes scores, ulong id)
    {
        Debug.Log("ServerRpc sent");
        playersAllValues.Add(id, JsonConvert.DeserializeObject<Dictionary<string, int>>(scores.ToString()));
        UpdatePlayerScores();
    }
    [ClientRpc]
    void SendScoresToClientRpc(FixedString512Bytes allScores)
    {
        Debug.Log("ClientRpc sent");
        Debug.Log(allScores.ToString());

        playersAllValues = JsonConvert.DeserializeObject<Dictionary<ulong, Dictionary<string, int>>>(allScores.ToString());
        Display();
    }




    private void UpdatePlayerScores()
    {

        Display();
        SendScoresToClientRpc(GlobalVariables.MyDictionaryToJsonToJson(playersAllValues));

    }


    private void Display()
    {
        foreach (Transform t in transform)
        {
            if (!t.gameObject.CompareTag("Header"))
                Destroy(t.gameObject);
        }

        foreach (KeyValuePair<ulong, Dictionary<string, int>> row in playersAllValues)
        {
            RowScores rowSpawned = Instantiate(rowScores, transform).GetComponent<RowScores>();
            rowSpawned.playerNum.text = ((int)row.Key + 1).ToString();

            rowSpawned.lvl_01_score.text = !row.Value.ContainsKey("Level_01") ? "0" : row.Value["Level_01"].ToString();
            rowSpawned.lvl_02_score.text = !row.Value.ContainsKey("Level_02") ? "0" : row.Value["Level_02"].ToString();
            rowSpawned.lvl_03_score.text = !row.Value.ContainsKey("Level_03") ? "0" : row.Value["Level_03"].ToString();
            rowSpawned.lvl_04_score.text = !row.Value.ContainsKey("Level_04") ? "0" : row.Value["Level_04"].ToString();

            int total = 0;
            foreach (int x in row.Value.Values)
            {
                total += x;
            }
            rowSpawned.totalScore.text = total.ToString();



            foreach (Transform tr in rowSpawned.transform)
            {

                tr.gameObject.GetComponentInChildren<Text>().color = playerNetwork.colors[(int)row.Key];

            }


        }
    }
}
