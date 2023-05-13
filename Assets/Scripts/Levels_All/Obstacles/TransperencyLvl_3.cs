using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Transperency : MonoBehaviour
{

    [SerializeField]
    private Tilemap _graphicMap; // Reference to the Tilemap component

    void Start()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        for (var x = -28; x <= 28; x++)
        {
            for (var y = -16; y <= 16; y++)
            {
                ChangeTransparency(x, y);
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        for (var x = -28; x <= 28; x++)
        {
            for (var y = -16; y <= 15; y++)
            {
                RevertTransparency(x, y);
            }
        }
    }

    public void ChangeTransparency(int x, int y)
    {
        var pos = new Vector3Int(x, y, 0);
        var color = new Color(1, 1, 1, 0.3f);
        _graphicMap.SetTileFlags(pos, TileFlags.None);
        _graphicMap.SetColor(pos, color);
    }

    public void RevertTransparency(int x, int y)
    {
        var pos = new Vector3Int(x, y, 0);
        var color = new Color(1, 1, 1, 1f);
        _graphicMap.SetTileFlags(pos, TileFlags.None);
        _graphicMap.SetColor(pos, color);
    }

}
