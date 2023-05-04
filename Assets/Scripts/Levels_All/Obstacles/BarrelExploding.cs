using Unity.Netcode;
using UnityEngine;

public class BarrelExploding : NetworkBehaviour
{

    Animator anim;
    [SerializeField]
    private float explosionForce = 5f;
    [SerializeField]
    private float explosionRadius = 5f;


    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
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
        Explosion();
    }

    [ServerRpc(RequireOwnership = false)]
    private void SendExplosionToServerRpc()
    {
        Explosion();
        SendExplosionToClientRpc();
    }


    public void Explosion()
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
        NetworkObject.Despawn(gameObject);
    }

}

