using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

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


    void Start()
    {


        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        colorPick = GameObject.FindObjectOfType<FlexibleColorPicker>();
        if (IsLocalPlayer)
        {
            colorPick.SetColor(colors[(int)OwnerClientId]);
        }

        try
        {
            this.gameObject.GetComponentInChildren<SpriteRenderer>().color = colors[(int)OwnerClientId];
        }
        catch
        {
            this.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;

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
        List<Color> colors = modifiedColors.ToList();
        this.gameObject.GetComponentInChildren<SpriteRenderer>().color = colors[(int)OwnerClientId];
    }


    [ServerRpc(RequireOwnership = false)]
    void SentColorsToServerRpc(Color color)
    {

        List<Color> modifiedColors = colors;



        try
        {

            modifiedColors[(int)OwnerClientId] = color;
            SendColorsToClientRpc(modifiedColors.ToArray());

        }
        catch
        {
            modifiedColors.Add(color);
            modifiedColors[(int)OwnerClientId] = color;
            SendColorsToClientRpc(modifiedColors.ToArray());

        }

    }


}
