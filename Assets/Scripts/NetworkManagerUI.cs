using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    //Variables
    [SerializeField]
    private Button hostBtn;
    [SerializeField]
    private Button clientBtn;
    [SerializeField]
    private InputField ip;


    bool connected = false;
    private void Awake()
    {
        hostBtn.onClick.AddListener(() =>
        {
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostEntry(hostName).AddressList[1].ToString();

            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = myIP; ;

            NetworkManager.Singleton.StartHost();
        });

        clientBtn.onClick.AddListener(() =>
        {
            if (ip.text != null || ip.text != "")
            {
                NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = ip.text;

                NetworkManager.Singleton.StartClient();



            }

        });
    }



}
