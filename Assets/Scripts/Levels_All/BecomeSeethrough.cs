using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BecomeSeethrough : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        tilemap.color = new Color(1f, 1f, 1f, 0.5f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        tilemap.color = new Color(1f, 1f, 1f, 1f);
    }
}
