using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Transperency : MonoBehaviour
{

    [SerializeField]
    private Tilemap _graphicMap; 
    // Reference to the Tilemap component
    [SerializeField]
    private int _minX = -10; // Minimum x coordinate for transparency change

    [SerializeField]
    private int _maxX = 10; // Maximum x coordinate for transparency change

    [SerializeField]
    private int _minY = -10; // Minimum y coordinate for transparency change

    [SerializeField]
    private int _maxY = 10; // Maxiximum y coordinate for transparency change
    void Start()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        for (var x = _minX; x <= _maxX; x++)
        {
            for (var y = _minY; y <= _maxY; y++)
            {
                ChangeTransparency(x, y);
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        for (var x = _minX; x <= _maxX; x++)
        {
            for (var y = _minY; y <= _maxY; y++)
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
