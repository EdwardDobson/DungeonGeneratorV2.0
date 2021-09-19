using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceTile : MonoBehaviour
{
    TilePlacer _placer;
    Tile _placedTileBase;
    DungeonSave _dungeonSave;
    [SerializeField]
    int _tileSelectedIndex;
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
                _placedTileBase.sprite = _placer._atlas.GetSprite(_placer._allTileDatas._tiles[_tileSelectedIndex]._tileName);
                _placedTileBase.color = _placer._allTileDatas._tiles[_tileSelectedIndex].GetTileColour();
                _placer._map.SetTile(mousePos, _placedTileBase);
                ModifedTile tileToAdd = new ModifedTile
                {
                    _tileID = _placer._allTileDatas._tiles[_tileSelectedIndex]._tileID,
                    _tileXPos = mousePos.x,
                    _tileYPos = mousePos.y,
                    _health = _placer._allTileDatas._tiles[_tileSelectedIndex]._health,
                    _tileName = _placer._allTileDatas._tiles[_tileSelectedIndex]._tileName,
                };
                if(!_dungeonSave._dataToSave._addedTiles.Contains(tileToAdd))
                _dungeonSave._dataToSave._addedTiles.Add(tileToAdd);
                _placer._placedTiles.Add(mousePos, _placer._allTileDatas._tiles[_tileSelectedIndex]);
                if(_dungeonSave._invalidPositions.Contains(mousePos))
                {
                    _dungeonSave._invalidPositions.Remove(mousePos);
                }
            }
        }
        ScrollTile();
    }
    void ScrollTile()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            _tileSelectedIndex++;
               if(_tileSelectedIndex > _placer._allTileDatas._tiles.Count - 1)
                {
                    _tileSelectedIndex = 0;
                }
              Debug.Log( _placer._allTileDatas._tiles[_tileSelectedIndex]._tileName);
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            _tileSelectedIndex--;
                if(_tileSelectedIndex < 0)
                {
                    _tileSelectedIndex = _placer._allTileDatas._tiles.Count - 1;
                }
                       Debug.Log( _placer._allTileDatas._tiles[_tileSelectedIndex]._tileName);
        }
    }
}
