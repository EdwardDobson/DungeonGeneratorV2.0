using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceTile : MonoBehaviour
{
    TilePlacer _placer;
    Tile _placedTileBase;
    DungeonSave _dungeonSave;
    void Start()
    {
        _placer = GameObject.Find("TilePlacer").GetComponent<TilePlacer>();
        _placedTileBase = ScriptableObject.CreateInstance<Tile>();
        _dungeonSave = GameObject.Find("Save").GetComponent<DungeonSave>();
    }
    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            Vector2 mouseposVec2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int mousePos = new Vector3Int((int)mouseposVec2.x, (int)mouseposVec2.y, 0);
            if(!_placer._placedTiles.ContainsKey(mousePos))
            {
                _placedTileBase.sprite = _placer._atlas.GetSprite(_placer._allTileDatas._tiles[0]._tileName);
                _placedTileBase.color = _placer._allTileDatas._tiles[0].GetTileColour();
                _placer._map.SetTile(mousePos, _placedTileBase);
                ModifedTile tileToAdd = new ModifedTile
                {
                    _tileID = _placer._allTileDatas._tiles[0]._tileID,
                    _tileXPos = mousePos.x,
                    _tileYPos = mousePos.y
                };
                if(!_dungeonSave._dataToSave._addedTiles.Contains(tileToAdd))
                _dungeonSave._dataToSave._addedTiles.Add(tileToAdd);
                _placer._placedTiles.Add(mousePos, _placer._allTileDatas._tiles[0]);
                if (_dungeonSave._dataToSave._tileXPos.Contains(mousePos.x) && _dungeonSave._dataToSave._tileYPos.Contains(mousePos.y))
                {
                    _dungeonSave._dataToSave._tileXPos.Remove(mousePos.x);
                    _dungeonSave._dataToSave._tileYPos.Remove(mousePos.y);
                }
            }
     
        }
    }
}
