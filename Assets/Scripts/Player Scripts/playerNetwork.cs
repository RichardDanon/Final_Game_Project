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

        if (IsServer)
        {
            SendColorsToClientRpc(colors.ToArray());
        }






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

            if (colorPick != null)
            {

                if (IsLocalPlayer)
                {
                    SentColorsToServerRpc(colorPick.GetColor());
                }
            }

            yield return new WaitForSeconds(0.25f);
        }

    }

    IEnumerator UpdateCamera()
    {
        while (true)
        {
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



        colors = modifiedColors.ToList();


        this.gameObject.GetComponentInChildren<SpriteRenderer>().color = colors[(int)OwnerClientId];
    }


    [ServerRpc(RequireOwnership = false)]
    void SentColorsToServerRpc(Color color)
    {

        List<Color> modifiedColors = colors;



        try
        {

            modifiedColors[(int)OwnerClientId] = color;
            Debug.Log(modifiedColors);
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
