using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class HoleScript : MonoBehaviour
{
    [SerializeField]
    private string nextLevel = "Menu";

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < 0.05f)
        {
            bool allPlayersCompleted = true;

            collision.GetComponent<playerNetwork>().IsLevelCompleted = true;

            List<GameObject> players = GameObject.FindGameObjectsWithTag("Player").ToList();
            players.ForEach(player =>
            {
                if (!player.GetComponent<playerNetwork>().IsLevelCompleted)
                {
                    allPlayersCompleted = false;
                    return;
                }
            });

            if (allPlayersCompleted)
            {
                NetworkManager.Singleton.SceneManager.LoadScene(nextLevel, UnityEngine.SceneManagement.LoadSceneMode.Single);
            }

        }
    }
}
