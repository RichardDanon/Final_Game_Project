using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleScript : NetworkBehaviour
{
    readonly private NetworkVariable<int> numOfPlayersCompleted = new(0);

    [SerializeField]
    private string nextLevel = "Menu";

    private void Start()
    {
        numOfPlayersCompleted.Value = 0;

        //check if all players finished each second to reuce lag
        if (IsServer)
            InvokeRepeating(nameof(ChangeLevel), 1, 1);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //determine of player is slow enough to enter the hole
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < 0.20f)
        {
            if (collision.gameObject.GetComponent<playerNetwork>().IsLocalPlayer)
            {
                collision.gameObject.GetComponent<playerNetwork>().IsLevelCompleted = true;
                float resistance = Mathf.Lerp(0f, 1f, collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude / (collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude / 1000f));
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-collision.gameObject.GetComponent<Rigidbody2D>().velocity * resistance);

                // Play the "Falling" sound clip
                AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Falling"), transform.position);

                IsCompleteed_ServerRpc();
                GlobalVariables.playerScores[SceneManager.GetActiveScene().name] = GlobalVariables.numOfHitsForLvl;
            }
        }
    }


    [ServerRpc(RequireOwnership = false)]
    public void IsCompleteed_ServerRpc()
    {

        //tell the server the client finished the level
        Invoke("IsCompletedIncrement", 1f);
    }

    private void IsCompletedIncrement()
    {
        //variable shared across the network for how many finished
        numOfPlayersCompleted.Value += 1;


    }

    private void ChangeLevel()
    {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        //compare the number who finished to the number of player, and change level if everyone finished
        if (numOfPlayersCompleted.Value == players.Length)
        {
            numOfPlayersCompleted.Value = 0;

            NetworkManager.Singleton.SceneManager.LoadScene(nextLevel, UnityEngine.SceneManagement.LoadSceneMode.Single);


        }
    }


}
