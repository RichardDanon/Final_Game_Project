using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpawn : NetworkBehaviour
{
    [SerializeField]
    private float x, y;


    void Start()
    {
        List<GameObject> players = GameObject.FindGameObjectsWithTag("Player").ToList();
        players.ForEach(player =>
        {
            player.GetComponent<playerNetwork>().IsLevelCompleted = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            player.GetComponent<hitBall>().enabled = true;
            player.transform.position = new Vector2(x, y);
        });
        GlobalVariables.numOfHitsForLvl = 0;

        GlobalVariables.playerScores.Add(SceneManager.GetActiveScene().name, GlobalVariables.numOfHitsForLvl);



    }
}
