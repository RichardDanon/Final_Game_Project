using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class StartGameScript : NetworkBehaviour
{

    private Button[] startBtn;

    private Button btn;

    private GameObject[] btnObject;

    private bool isDone = false;



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
                    btnObject = GameObject.FindObjectsOfType<GameObject>(true);


                    foreach (GameObject go in btnObject)
                    {
                        if (go.name.Equals("StartObject"))
                        {
                            go.SetActive(true);
                        }
                    }

                    btn.onClick.AddListener(() =>
                    {
                        NetworkManager.Singleton.SceneManager.LoadScene("Level_01", UnityEngine.SceneManagement.LoadSceneMode.Single);
                    });
                    isDone = true;
                    Debug.Log(IsServer);

                }
            }

        }

    }
    // Update is called once per frame
    void Update()
    {

        if (!isDone)
            startGame();
    }


}
