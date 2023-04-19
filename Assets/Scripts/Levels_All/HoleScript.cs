using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;


public class HoleScript : NetworkBehaviour
{
    private NetworkVariable<int> numOfPlayersCompleted = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [SerializeField]
    private string nextLevel = "Menu";

    private void Update()
    {
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < 0.05f)
        {
            collision.gameObject.GetComponent<playerNetwork>().IsLevelCompleted = true;
            numOfPlayersCompleted.Value += 1;
            List<GameObject> players = GameObject.FindGameObjectsWithTag("Player").ToList();



            if (numOfPlayersCompleted.Value == players.Count)
            {
                NetworkManager.Singleton.SceneManager.LoadScene(nextLevel, UnityEngine.SceneManagement.LoadSceneMode.Single);
            }

        }
    }
}
