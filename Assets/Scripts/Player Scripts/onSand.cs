using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class onSand : MonoBehaviour
{
    private List<Rigidbody2D> players;

    // Start is called before the first frame update


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            players.Add(collision.attachedRigidbody);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            players.Remove(collision.attachedRigidbody);
        }
    }

    private void Update()
    {
        if (players.Count != 0)
        {
            foreach (Rigidbody2D player in players)
            {
                player.velocity = player.velocity / 2;
            }
        }
    }

}
