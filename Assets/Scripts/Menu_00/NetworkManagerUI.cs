using System.Net;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : NetworkBehaviour
{
    //Variables
    [SerializeField]
    private TMP_Text ipText;
    [SerializeField]
    private Button hostBtn;
    [SerializeField]
    private Button clientBtn;
    [SerializeField]
    private Button clientBtnCancel;
    [SerializeField]
    private TMP_InputField ip;
    [SerializeField]
    private GameObject startBtn;
    [SerializeField]
    private FlexibleColorPicker colorPicker;

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
                startBtn.SetActive(true);

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

            NetworkManager.Singleton.Shutdown();
            Invoke("CameraCenter", 0.1f);
            startBtn.SetActive(false);
            isClientJoined = false;
            ipText.text = string.Empty;
            GlobalVariables.ResetVariables();

        });
    }

    private void CameraCenter()
    {
        Camera.main.GetComponent<FollowPlayer>().setTarget(new Vector3(0, 0, 0));
    }
    private void Update()
    {

        if (NetworkManager.Singleton.IsConnectedClient || NetworkManager.Singleton.IsHost)
            colorPicker.gameObject.SetActive(true);
        else
            colorPicker.gameObject.SetActive(false);

        if (IsLocalPlayer && !isClientJoined)
        {
            isClientJoined = true;
        }

        if (this.gameObject.transform.localScale != Vector3.one)
        {
            this.gameObject.transform.localScale = Vector3.one;
            this.gameObject.transform.position = Vector3.zero;
        }
    }
}
