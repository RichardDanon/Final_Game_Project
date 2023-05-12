using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerNetwork : NetworkBehaviour
{

    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private FlexibleColorPicker colorPick;


    public static List<Color> colors = new()
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


        //if it's the server player, when a ball is created send the color list
        if (IsServer)
        {
            SendColorsToClientRpc(colors.ToArray());
        }





        //Set the ball color to the list of colors if it's more players than the list size then change to white
        try
        {
            this.gameObject.GetComponentInChildren<SpriteRenderer>().color = colors[(int)OwnerClientId];
        }
        catch
        {
            this.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;

        }


        StartCoroutine(UpdateCamera());


        StartCoroutine(UpdateColor());

    }



    private void Update()
    {



    }


    IEnumerator UpdateColor()
    {
        while (true)
        {
            //make sure this is in the menu
            if (colorPick != null)
            {
                //make it act only for the locallayer and not for "unowned" balls
                if (IsLocalPlayer)
                {
                    //sent to the server the color chosen by the player 
                    SentColorsToServerRpc(colorPick.GetColor());
                }
            }

            yield return new WaitForSeconds(0.25f);
        }

    }

    IEnumerator UpdateCamera()
    {
        //simply make the camera follow the player
        while (true)
        {
            //set the color of the balls
            if (SceneManager.GetActiveScene().name.Equals("Menu") && colorPick == null)
            {
                colorPick = GameObject.FindObjectOfType<FlexibleColorPicker>();
                if (colorPick != null && IsLocalPlayer)
                {
                    colorPick.SetColor(colors[(int)OwnerClientId]);
                }
            }
            if (IsLocalPlayer && gameObject != null && Camera.main != null)
            {
                if (!IsLevelCompleted)
                {
                    Camera.main.GetComponent<FollowPlayer>().setTarget(gameObject.transform.position);
                }
                else if (GameObject.FindGameObjectWithTag("Hole") != null && GameObject.FindGameObjectWithTag("Hole").transform.position != null)
                {
                    Camera.main.GetComponent<FollowPlayer>().setTarget(GameObject.FindGameObjectWithTag("Hole").transform.position);
                    this.gameObject.transform.position = new Vector3(100, 100, 0);
                }


            }
            yield return new WaitForSeconds(0);
        }
    }








    [ClientRpc]
    void SendColorsToClientRpc(Color[] modifiedColors)
    {


        //The server sends to the clients the updated list of colors
        colors = modifiedColors.ToList();


        this.gameObject.GetComponentInChildren<SpriteRenderer>().color = colors[(int)OwnerClientId];
    }


    [ServerRpc(RequireOwnership = false)]
    void SentColorsToServerRpc(Color color)
    {
        //the server adds the received colored from the clients to it's local list to then send the list back to the clients

        List<Color> modifiedColors = colors;


        //if the id already exists simply update that color otherwise create a new item in the list
        try
        {
            modifiedColors[(int)OwnerClientId] = color;
            SendColorsToClientRpc(modifiedColors.ToArray());
        }
        catch
        {
            modifiedColors.Add(color);
            SendColorsToClientRpc(modifiedColors.ToArray());
        }

    }

}
