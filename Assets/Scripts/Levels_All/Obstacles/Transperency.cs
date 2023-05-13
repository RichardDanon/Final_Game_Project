using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Transperency : MonoBehaviour
{
    Tilemap _graphicMap;
    bool isTriggered = false;

    void Start()
    {
        // Find the TransparentThings object and get the Tilemap component
        var go = GameObject.Find("TransparentThings");
        _graphicMap = go.GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggered = true;
            ChangeTransparency();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTriggered = false;
            ChangeTransparency();
        }
    }

    public void ChangeTransparency()
    {
        Color color = Color.white;

        if (isTriggered)
        {
            color.a = 0.3f; // Set the transparency to 0.3
        }
        else
        {
            color.a = 1f; // Set the transparency to 1 (fully opaque)
        }

        // Loop through all the tiles in the Tilemap and change their transparency
        foreach (var pos in _graphicMap.cellBounds.allPositionsWithin)
        {
            _graphicMap.SetTileFlags(pos, TileFlags.None);

            _graphicMap.SetColor(pos, color);
        }
    }
}
