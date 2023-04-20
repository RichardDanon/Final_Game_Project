using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : NetworkBehaviour
{

    [SerializeField]
    private GameObject canvas;
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

        }
    }

    public void Resume()
    {
        canvas.SetActive(false);

    }

    public void Disconnect()
    {
        if (IsClient)
            NetworkManager.Singleton.GetComponent<UnityTransport>().DisconnectLocalClient();
        else
        {
            NetworkManager.Singleton.Shutdown();
            SceneManager.LoadScene("Menu");
            Invoke("CameraCenter", 0.1f);
        }
    }

    private void CameraCenter()
    {
        Camera.main.GetComponent<FollowPlayer>().setTarget(new Vector3(0, 0, 0));
    }
}
