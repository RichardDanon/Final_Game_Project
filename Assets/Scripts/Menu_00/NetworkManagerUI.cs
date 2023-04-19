using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
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


    private void Awake()
    {
        hostBtn.onClick.AddListener(() =>
        {
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostEntry(hostName).AddressList[1].ToString();

            ipText.text = myIP;
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = myIP; ;

            NetworkManager.Singleton.StartHost();
        });

        clientBtn.onClick.AddListener(() =>
        {
            if (ip.text != null && ip.text != "")
            {
                NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = ip.text.Trim();

                NetworkManager.Singleton.StartClient();



            }

        });

        clientBtnCancel.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().DisconnectLocalClient();
        });
    }



}
