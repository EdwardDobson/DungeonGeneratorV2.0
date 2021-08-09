using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDetailPuller : MonoBehaviour
{
    TilePlacer _tilePlacer;
    public bool _tileInfoDebug;
    void Start()
    {
        _tilePlacer = GameObject.Find("TilePlacer").GetComponent<TilePlacer>();
    }
    void Update()
    {
        if(_tileInfoDebug)
        {
            if (Input.GetMouseButton(1))
            {
                Vector2 mouseposVec2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int mousePos = new Vector3Int((int)mouseposVec2.x, (int)mouseposVec2.y, 0);
                if (_tilePlacer._placedTiles.ContainsKey(mousePos))
                {
                    Debug.Log(_tilePlacer._placedTiles[mousePos].TileID + " Tile Health: " + _tilePlacer._placedTiles[mousePos].Health);
                }
            }
        }
    }
}
