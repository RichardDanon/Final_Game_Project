using System.Collections.Generic;
using UnityEngine;

public class onSand : MonoBehaviour
{
    private List<Rigidbody2D> players;

    // Start is called before the first frame update

    private void Start()
    {
        players = new List<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
        if (players != null)
        {
            foreach (Rigidbody2D player in players)
            {
                if (player.velocity.magnitude != 0)
                {
                    float resistance = Mathf.Lerp(0f, 1f, player.velocity.magnitude / (player.velocity.magnitude / 1000f));
                    player.AddForce(-player.velocity * resistance);
                }
            }
        }
    }

}
