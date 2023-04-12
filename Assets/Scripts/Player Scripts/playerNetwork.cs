using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class playerNetwork : NetworkBehaviour
{

    //Variables
    [SerializeField]
    private GameObject ball;









    // Start is called before the first frame update
    void Start()
    {
        List<Color> colors = new()
     {
         Color.red,
         Color.green,
         Color.yellow,
         Color.magenta,
         new Color(255F, 0F, 255F),
         new Color(0F, 255F, 255F),
         new Color(255F, 255F, 0F),
         new Color(128F, 0F, 128F),
         new Color(128F, 0F, 0F)
     };


        SpriteRenderer sprite = ball.GetComponent<SpriteRenderer>();

        sprite.color = colors[(int)OwnerClientId];



    }







}
