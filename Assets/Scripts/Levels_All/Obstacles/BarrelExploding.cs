using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BarrelExploding : MonoBehaviour
{

    Animator anim;
    [SerializeField]
    private float explosionForce = 5f;
    [SerializeField]
    private float explosionRadius = 5f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        anim.enabled = true;
            
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rigidbody = collider.GetComponent<Rigidbody2D>();
            if (rigidbody != null)
            {
                Vector2 direction = (collider.transform.position - transform.position).normalized;
                rigidbody.AddForce(direction * explosionForce, ForceMode2D.Impulse);
            }
        }
        
        Destroy(anim.gameObject, 0.5f);
    }
}
