using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class StartGameScript : NetworkBehaviour
{

    private Button[] startBtn;

    private Button btn;

    private bool isDone = false;

    void Start()
    {

    }


    private void startGame()
    {
        startBtn = GameObject.FindObjectsOfType<Button>(true);

        foreach (Button button in startBtn)
        {
            if (button.name == "StartGameBtn")
            {
                btn = button;
                if (IsServer)
                {
                    btn.enabled = true;
                    btn.onClick.AddListener(() =>
                    {
                        NetworkManager.Singleton.SceneManager.LoadScene("level_01", UnityEngine.SceneManagement.LoadSceneMode.Additive);
                    });
                }
            }

        }

    }
    // Update is called once per frame
    void Update()
    {
        /*
        if (!isDone)
        {
            Invoke("startGame", 2f);
            isDone = true;
        }
        */
        Debug.Log(IsServer);

    }


}
