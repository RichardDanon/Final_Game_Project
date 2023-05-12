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


    private void Awake()
    {
        //set the host btn to create a multiplayer session that is joinable
        hostBtn.onClick.AddListener(() =>
        {
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostEntry(hostName).AddressList[1].ToString();
            ipText.text = myIP;
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = myIP; ;
            startBtn.SetActive(true);

            NetworkManager.Singleton.StartHost();

        });

        //get the inputed IP and join that session if a hosted game is there
        clientBtn.onClick.AddListener(() =>
        {
            if (ip.text != null && ip.text != "")
            {
                NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = ip.text.Trim();

                NetworkManager.Singleton.StartClient();


            }

        });

        //Cancel the search for that IP if needed
        clientBtnCancel.onClick.AddListener(() =>
        {

            NetworkManager.Singleton.Shutdown();
            Invoke("CameraCenter", 0.1f);
            startBtn.SetActive(false);
            ipText.text = string.Empty;
            GlobalVariables.ResetVariables();

        });
    }

    private void CameraCenter()
    {
        //put camera to 0,0,0
        Camera.main.GetComponent<FollowPlayer>().setTarget(new Vector3(0, 0, 0));
    }
    private void Update()
    {
        //if connected to game then show colorpicker option
        if (NetworkManager.Singleton.IsConnectedClient || NetworkManager.Singleton.IsHost)
            colorPicker.gameObject.SetActive(true);
        else
            colorPicker.gameObject.SetActive(false);





        //fix weird bug where UI disapears for clients who joined
        if (this.gameObject.transform.localScale != Vector3.one)
        {
            this.gameObject.transform.localScale = Vector3.one;
            this.gameObject.transform.position = Vector3.zero;
        }
    }
}
