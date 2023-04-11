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

        SpriteRenderer sprite = ball.GetComponent<SpriteRenderer>();
        sprite.color = Random.ColorHSV();


    }


}
