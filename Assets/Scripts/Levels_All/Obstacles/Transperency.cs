using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Transperency : MonoBehaviour
{
    // Start is called before the first frame update
    Tilemap _graphicMap;
    bool _isTransparent = false;

    void Start()
    {
        var go = GameObject.Find("TransparentThings");
        _graphicMap = go.GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _isTransparent = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _isTransparent = false;
    }

    private void Update()
    {
        if (_isTransparent)
        {
            for (var x = -28; x <= -27; x++)
                for (var y = 15; y <= 6; y++)
                    ChangeTransparency(x, y, 0.3f);
        }
        else
        {
            for (var x = -28; x <= -27; x++)
                for (var y = 15; y <= 6; y++)
                    ChangeTransparency(x, y, 1f);
        }
    }

    public void ChangeTransparency(int x, int y, float alpha)
    {
        var pos = new Vector3Int(x, y);

        var color = new Color(1, 1, 1, alpha);

        _graphicMap.SetTileFlags(pos, TileFlags.None);

        _graphicMap.SetColor(pos, color);
    }
}
