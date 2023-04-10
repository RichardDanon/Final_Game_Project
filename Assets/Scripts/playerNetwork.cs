using Unity.Netcode;
using UnityEngine;

public class playerNetwork : NetworkBehaviour
{

    //Variables
    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private NetworkVariable<float> speed = new NetworkVariable<float>(10f);




    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()

    {

        SpriteRenderer sprite = ball.GetComponent<SpriteRenderer>();
        sprite.color = Random.ColorHSV();

        rb2d = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void ChangeScene()
    {

    }
}
