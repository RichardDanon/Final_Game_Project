using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    private Rigidbody2D player;

    [SerializeField]
    private float speed = 150;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Rigidbody2D>();

        player.AddForce(player.velocity.normalized * speed, ForceMode2D.Force);
    }
}
