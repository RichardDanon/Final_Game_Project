using Unity.Netcode;
using UnityEngine;

public class hitBall : NetworkBehaviour
{
    [SerializeField]
    private bool isMoving;

    private Rigidbody2D rb2d;




    void Awake()
    {

    }

    private void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }



    private void FixedUpdate()
    {
        if (rb2d.velocity.magnitude < 0.05f)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }


        if (!isMoving)
        {
            if (IsOwner)
            {
                if (Input.GetKey(KeyCode.A))
                    rb2d.AddForce(Vector3.left * 20);
                if (Input.GetKey(KeyCode.D))
                    rb2d.AddForce(Vector3.right * 20);
                if (Input.GetKey(KeyCode.W))
                    rb2d.AddForce(Vector3.up * 20);
                if (Input.GetKey(KeyCode.S))
                    rb2d.AddForce(Vector3.down * 20);


                Debug.Log("Not Moving");
            }



        }
    }
}
