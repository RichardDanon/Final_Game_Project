using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class playerNetwork : NetworkBehaviour
{

    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private FlexibleColorPicker colorPick;
    private List<Color> colors = new()
        {
             Color.red,
             Color.green,
             Color.yellow,
             Color.magenta,
             Color.blue,
             Color.cyan,
             Color.white,
             new Color(255F, 0F, 255F),
             new Color(0F, 255F, 255F),
             new Color(255F, 255F, 0F),
             new Color(128F, 0F, 128F),
             new Color(128F, 0F, 0F)
        };

    public bool IsLevelCompleted = false;


    private int playerBefore = 1;
    void Start()
    {

        List<Button> colorBtn = GameObject.FindObjectsOfType<Button>().ToList();

        colorPick = GameObject.FindObjectOfType<FlexibleColorPicker>();

        foreach (Button button in colorBtn)
        {
            if (button.CompareTag("ColorPicker"))
            {
                button.onClick.AddListener(() =>
                {
                    if (IsLocalPlayer)
                        SentColorsToServerRpc(colorPick.GetColor());
                });
            }
        }

        try
        {
            this.gameObject.GetComponentInChildren<SpriteRenderer>().color = colors[(int)OwnerClientId];
        }
        catch (ArgumentOutOfRangeException ex)
        {

        }

    }



    private void Update()
    {

        if (IsLocalPlayer && gameObject != null && Camera.main != null)
        {
            if (!IsLevelCompleted)
            {
                Camera.main.GetComponent<FollowPlayer>().setTarget(gameObject.transform.position);
            }
            else
            {
                Camera.main.GetComponent<FollowPlayer>().setTarget(new Vector3(0, 0, 0));
                this.gameObject.transform.position = new Vector3(100, 100, 0);
            }


        }
        if (colorPick != null)
        {

            if (IsLocalPlayer)
                SentColorsToServerRpc(colorPick.GetColor());
        }
    }

    [ClientRpc]
    void SendColorsToClientRpc(Color[] modifiedColors)
    {
        Debug.Log("Talking to client");

        List<Color> colors = modifiedColors.ToList();



        this.gameObject.GetComponentInChildren<SpriteRenderer>().color = colors[(int)OwnerClientId];
    }


    [ServerRpc(RequireOwnership = false)]
    void SentColorsToServerRpc(Color color)
    {
        Debug.Log("Talking to server");

        List<Color> modifiedColors = colors;

        try
        {
            modifiedColors[(int)OwnerClientId] = color;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            modifiedColors.Add(color);
            modifiedColors[(int)OwnerClientId] = color;
            SendColorsToClientRpc(modifiedColors.ToArray());

        }
        SendColorsToClientRpc(modifiedColors.ToArray());
    }


}
