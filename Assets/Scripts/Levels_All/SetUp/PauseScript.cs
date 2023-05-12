using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : NetworkBehaviour
{

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    Slider volumeSlider;

    [SerializeField]
    Toggle mute;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.SetActive(!canvas.activeSelf);
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (player.GetComponent<playerNetwork>().IsLocalPlayer)
                {
                    player.GetComponent<hitBall>().enabled = !canvas.activeSelf;
                }
            }
        }


    }

    public void Resume()
    {
        canvas.SetActive(false);

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (player.GetComponent<playerNetwork>().IsLocalPlayer)
            {
                player.GetComponent<hitBall>().enabled = true;
            }
        }
    }

    public void ChangeVolume()
    {
     

        if (mute.isOn)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = volumeSlider.value;
        }
    }

    public void Disconnect()
    {
        GlobalVariables.ResetVariables();
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene("Menu");
        Invoke("CameraCenter", 0.1f);

    }

    private void CameraCenter()
    {
        Camera.main.GetComponent<FollowPlayer>().setTarget(new Vector3(0, 0, 0));
    }
}
