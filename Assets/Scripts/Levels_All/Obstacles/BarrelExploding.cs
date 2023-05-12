using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class BarrelExploding : NetworkBehaviour
{

    Animator anim;
    [SerializeField]
    private float explosionForce = 5f;
    [SerializeField]
    private float explosionRadius = 5f;

    private AudioSource audioSource;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if host then tell clients it exploded otherwise tell server to tell clients
        if (NetworkManager.Singleton.IsHost)
        {
            SendExplosionToClientRpc();
        }
        else
        {
            SendExplosionToServerRpc();
        }
    }

    [ClientRpc]
    private void SendExplosionToClientRpc()
    {
        //tell all clients that this exploded
        Explosion();
    }

    [ServerRpc(RequireOwnership = false)]
    private void SendExplosionToServerRpc()
    {
        //make it explode then tell all clients that this exploded
        Explosion();
        SendExplosionToClientRpc();
    }


    public void Explosion()
    {
        //make ball be pushed back and play animation
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

        audioSource.Play();

        Destroy(anim.gameObject, 0.5f);
        StartCoroutine(despawnObject());
    }

    public IEnumerator despawnObject()
    {
        yield return new WaitForSeconds(0.5f);

        NetworkObject.Despawn(gameObject);
    }

}

