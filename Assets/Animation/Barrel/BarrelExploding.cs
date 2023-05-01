using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExploding : MonoBehaviour
{

    Animator anim;
    bool isExploding;
    List<Rigidbody2D> players;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        isExploding = anim.GetBool("isExploding");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        players.Add(collision.attachedRigidbody);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        players.Remove(collision.attachedRigidbody);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.enabled = true;
        isExploding = true;

        if (isExploding)
        {
            foreach (Rigidbody2D player in players)
            {
                
            }
        }
    }
}
