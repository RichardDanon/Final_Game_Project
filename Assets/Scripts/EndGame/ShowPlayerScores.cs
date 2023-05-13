using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerScores : NetworkBehaviour
{
    [SerializeField]
    private int numOfLevels = 4;

    public RowScores rowScores;

    private int highestScore = 0;

    private Dictionary<ulong, Dictionary<string, int>> playersAllValues = new();

    private int totalPlayers = 0;

    void Start()
    {
        //When the scoreboard is loaded send to the server your individual score
        SendScoresToServerRpc(GlobalVariables.MyDictionaryToJson(GlobalVariables.playerScores), NetworkManager.Singleton.LocalClientId);

    }

    void Update()
    {
        //update the scoreboard if the number of connected players changed
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
        //this sends to the server your individual score, so it saves it locally with that player id
        playersAllValues.Add(id, JsonConvert.DeserializeObject<Dictionary<string, int>>(scores.ToString()));
        UpdatePlayerScores();
    }
    [ClientRpc]
    void SendScoresToClientRpc(FixedString512Bytes allScores)
    {
        //the server send all the player scores to each client so they can save it locally
        playersAllValues = JsonConvert.DeserializeObject<Dictionary<ulong, Dictionary<string, int>>>(allScores.ToString());
        Display();
    }




    private void UpdatePlayerScores()
    {
        //send to clients the new scores
        Display();
        SendScoresToClientRpc(GlobalVariables.MyDictionaryToJsonToJson(playersAllValues));

    }


    private void Display()
    {
        //reset the scoreboard
        foreach (Transform t in transform)
        {
            if (!t.gameObject.CompareTag("Header"))
                Destroy(t.gameObject);
        }


        //display all the values for each level
        foreach (KeyValuePair<ulong, Dictionary<string, int>> player in playersAllValues)
        {
            RowScores rowSpawned = Instantiate(rowScores, transform).GetComponent<RowScores>();

            int total = 0;
            foreach (int x in player.Value.Values)
            {
                if (player.Value.ContainsKey("Tutorial"))
                {
                    player.Value.Remove("Tutorial");
                }
                //make sure the player played all levels
                if (player.Value.Count == numOfLevels)
                {

                    total += x;
                }
                else
                {
                    total = 0;
                    break;
                }
            }

            //determine highest score
            if ((total < highestScore || highestScore == 0) && player.Value.Count == numOfLevels)
            {
                highestScore = total;
            }


            if (total != 0)
                rowSpawned.totalScore.text = total.ToString();

            //assign who won
            if (total == highestScore)
                rowSpawned.playerNum.text = "Winner " + ((int)player.Key + 1).ToString();
            else
                rowSpawned.playerNum.text = ((int)player.Key + 1).ToString();

            rowSpawned.lvl_01_score.text = !player.Value.ContainsKey("Level_01") ? "0" : player.Value["Level_01"].ToString();
            rowSpawned.lvl_02_score.text = !player.Value.ContainsKey("Level_02") ? "0" : player.Value["Level_02"].ToString();
            rowSpawned.lvl_03_score.text = !player.Value.ContainsKey("Level_03") ? "0" : player.Value["Level_03"].ToString();
            rowSpawned.lvl_04_score.text = !player.Value.ContainsKey("Level_04") ? "0" : player.Value["Level_04"].ToString();
            rowSpawned.lvl_05_score.text = !player.Value.ContainsKey("Level_05") ? "0" : player.Value["Level_05"].ToString();


            foreach (Transform tr in rowSpawned.transform)
            {
                tr.gameObject.GetComponentInChildren<Text>().color = playerNetwork.colors[(int)player.Key];
            }


        }
    }
}
