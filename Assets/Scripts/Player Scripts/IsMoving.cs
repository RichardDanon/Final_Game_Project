using UnityEngine;

public class IsMoving : MonoBehaviour
{
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (rb2d.velocity.magnitude < 0.05f)
        {
            gameObject.GetComponent<Collider2D>().isTrigger = true;
        }
        else
        {
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
