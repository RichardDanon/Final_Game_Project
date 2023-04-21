using System.Net;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class StartGameScript : NetworkBehaviour
{

    [SerializeField]
    private string firstLevel = "Level_01";

    private Button[] startBtn;

    private Button btn;

    private GameObject[] btnObject;

    private bool isDone = false;



    private void StartGame()
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
                        NetworkManager.Singleton.SceneManager.LoadScene(firstLevel, UnityEngine.SceneManagement.LoadSceneMode.Single);
                    });
                    isDone = true;




                }
            }
            else if (button.name == "CopyIp")
            {
                button.onClick.AddListener(() =>
                {
                    string hostName = Dns.GetHostName();

                    GUIUtility.systemCopyBuffer = Dns.GetHostEntry(hostName).AddressList[1].ToString();
                });
            }

        }

    }
    // Update is called once per frame
    void Update()
    {

        if (!isDone)
            StartGame();
    }


}
