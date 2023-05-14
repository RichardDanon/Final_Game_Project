using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField]
    private float x, y;
    [SerializeField]
    GameObject waterPrefab;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (playerRb.velocity.magnitude < 0.75f)
        {
            GameObject splash = Instantiate(waterPrefab, collision.transform.position, collision.transform.rotation);
            StartCoroutine(WaterAnim(collision, playerRb, splash));
        }
    }

    IEnumerator WaterAnim(Collider2D collision, Rigidbody2D playerRb, GameObject splash)
    {
        yield return new WaitForSeconds(1f);

        collision.transform.position = new Vector2(x, y);
        playerRb.velocity = new Vector2(0, 0);
        Destroy(splash);
    }


}
