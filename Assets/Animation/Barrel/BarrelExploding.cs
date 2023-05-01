using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BarrelExploding : MonoBehaviour
{

    Animator anim;
    GameObject barrel;
    List<GameObject> players;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        barrel = GetComponent<GameObject>();
        players = new List<GameObject>();
        anim.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        players.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        players.Remove(collision.gameObject);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.enabled = true;
            
        foreach (GameObject player in players)
        {
            Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
            playerRB.AddForceAtPosition(new Vector2(5, 0), transform.position, ForceMode2D.Impulse);
        }
        
        Destroy(anim.gameObject);
    }
}
