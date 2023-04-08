using Unity.Netcode;
using UnityEngine;

public class playerNetwork : NetworkBehaviour
{

    //22 minutes in the video, if we need more stuff


    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (IsOwner)
        {
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            rb2d.MovePosition(rb2d.position + (move * 10 * Time.deltaTime));
        }
    }
}
