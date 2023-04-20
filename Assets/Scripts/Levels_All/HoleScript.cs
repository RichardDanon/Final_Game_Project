using Unity.Netcode;
using UnityEngine;


public class HoleScript : NetworkBehaviour
{
    private NetworkVariable<int> numOfPlayersCompleted = new NetworkVariable<int>(0);

    [SerializeField]
    private string nextLevel = "Menu";

    private void Update()
    {
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < 0.25f)
        {
            collision.gameObject.GetComponent<playerNetwork>().IsLevelCompleted = true;
            IsCompleteed_ServerRPC();
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            if (numOfPlayersCompleted.Value == players.Length)
            {
                Debug.Log(numOfPlayersCompleted.Value);
                Debug.Log(players.Length);
                NetworkManager.Singleton.SceneManager.LoadScene(nextLevel, UnityEngine.SceneManagement.LoadSceneMode.Single);
            }
        }
    }


    [ServerRpc]
    public void IsCompleteed_ServerRPC()
    {
        Invoke("isCompletedIncrement", 1f);
    }

    private void isCompletedIncrement()
    {
        numOfPlayersCompleted.Value += 1;

    }


}
