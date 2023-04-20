using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : NetworkBehaviour
{
    //Variables
    [SerializeField]
    private Text ipText;
    [SerializeField]
    private Button hostBtn;
    [SerializeField]
    private Button clientBtn;
    [SerializeField]
    private Button clientBtnCancel;
    [SerializeField]
    private InputField ip;


    private bool isClientJoined = false;

    private void Awake()
    {
        hostBtn.onClick.AddListener(() =>
        {
            if (!isClientJoined)
            {
                string hostName = Dns.GetHostName();
                string myIP = Dns.GetHostEntry(hostName).AddressList[1].ToString();
                ipText.text = myIP;
                NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = myIP; ;

                NetworkManager.Singleton.StartHost();
            }
        });

        clientBtn.onClick.AddListener(() =>
        {
            if (ip.text != null && ip.text != "")
            {
                NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = ip.text.Trim();

                NetworkManager.Singleton.StartClient();

                isClientJoined = true;


            }

        });

        clientBtnCancel.onClick.AddListener(() =>
        {
            if (IsClient)
                NetworkManager.Singleton.GetComponent<UnityTransport>().DisconnectLocalClient();
            else
            {
                NetworkManager.Singleton.Shutdown();
                Invoke("CameraCenter", 0.1f);
            }
            isClientJoined = false;
            ipText.text = string.Empty;

        });
    }

    private void CameraCenter()
    {
        Camera.main.GetComponent<FollowPlayer>().setTarget(new Vector3(0, 0, 0));
    }
    private void Update()
    {
        if (IsLocalPlayer && !isClientJoined)
            isClientJoined = true;
    }
}
