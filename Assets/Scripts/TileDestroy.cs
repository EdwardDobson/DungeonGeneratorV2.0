using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDestroy : MonoBehaviour
{
    TilePlacer _tilePlacer;
    DungeonSave _dungeonSave;
    public int _digDamage;
    void Start()
    {
        _tilePlacer = GameObject.Find("TilePlacer").GetComponent<TilePlacer>();
        _dungeonSave = GameObject.Find("Save").GetComponent<DungeonSave>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseposVec2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int mousePos = new Vector3Int((int)mouseposVec2.x, (int)mouseposVec2.y, 0);
            if (_tilePlacer._placedTiles.ContainsKey(mousePos))
            {
                TileDG copy = _tilePlacer._placedTiles[mousePos];
                copy._health -= _digDamage;
                if (copy._health <= 0)
                {
                    _dungeonSave._invalidPositions.Add(mousePos);
                    _tilePlacer._placedTiles.Remove(mousePos);
                    _tilePlacer._map.SetTile(mousePos, null);
                    for(int i = 0;i < _dungeonSave._dataToSave._addedTiles.Count; i++)
                    {
                        if(_dungeonSave._dataToSave._addedTiles[i]._tileXPos == mousePos.x && _dungeonSave._dataToSave._addedTiles[i]._tileYPos == mousePos.y)
                        {
                            _dungeonSave._dataToSave._addedTiles.RemoveAt(i);
                        }
                    }
                }
            }
        }
    }
}
