using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField]
    private float x, y;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (playerRb.velocity.magnitude < 0.75f)
        {
            collision.transform.position = new Vector2(x, y);
            playerRb.velocity = new Vector2(0, 0);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
