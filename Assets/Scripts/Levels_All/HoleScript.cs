using Unity.Netcode;
using UnityEngine;


public class HoleScript : NetworkBehaviour
{
    readonly private NetworkVariable<int> numOfPlayersCompleted = new(0);

    [SerializeField]
    private string nextLevel = "Menu";

    private void Start()
    {
        numOfPlayersCompleted.Value = 0;
        if (IsServer)
            InvokeRepeating(nameof(ChangeLevel), 1, 1);
    }
    private void FixedUpdate()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < 0.20f)
        {
            if (collision.gameObject.GetComponent<playerNetwork>().IsLocalPlayer)
            {

                collision.gameObject.GetComponent<playerNetwork>().IsLevelCompleted = true;

                IsCompleteed_ServerRpc();




            }


        }
    }


    [ServerRpc(RequireOwnership = false)]
    public void IsCompleteed_ServerRpc()
    {
        Invoke("IsCompletedIncrement", 1f);
    }

    private void IsCompletedIncrement()
    {
        numOfPlayersCompleted.Value += 1;


    }

    private void ChangeLevel()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");


        if (numOfPlayersCompleted.Value == players.Length)
        {
            numOfPlayersCompleted.Value = 0;

            NetworkManager.Singleton.SceneManager.LoadScene(nextLevel, UnityEngine.SceneManagement.LoadSceneMode.Single);


        }
    }


}
