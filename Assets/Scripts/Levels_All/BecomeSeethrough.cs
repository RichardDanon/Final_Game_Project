using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BecomeSeethrough : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    public float transparentValue = 0.5f;

    [SerializeField]
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        originalColor = tilemap.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Color color = tilemap.color;
        color.a = transparentValue;
        tilemap.color = color;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        tilemap.color = originalColor;
    }
}
