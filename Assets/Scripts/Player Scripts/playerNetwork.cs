using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class playerNetwork : NetworkBehaviour
{

    [SerializeField]
    private GameObject ball;

    public bool IsLevelCompleted = false;
    void Start()
    {


        List<Color> colors = new()
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



        SpriteRenderer sprite = ball.GetComponent<SpriteRenderer>();

        sprite.color = colors[(int)OwnerClientId];


    }


    private void Update()
    {

        if (IsLocalPlayer && gameObject != null)
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


    }





}
