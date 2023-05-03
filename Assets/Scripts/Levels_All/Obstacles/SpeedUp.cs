using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    Rigidbody2D player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Rigidbody2D>();

        player.AddForce(new Vector2(100f, 0f), ForceMode2D.Force);
    }
}
