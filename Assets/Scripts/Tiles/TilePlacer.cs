using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;

public class TilePlacer : MonoBehaviour
{
    public SpriteAtlas _atlas;
    public AllTiles _allTileDatas;
    public Dictionary<Vector3Int, TileDG> _placedTiles = new Dictionary<Vector3Int, TileDG>();
    public Tilemap _map;
    public bool _tilesLoaded;
    [SerializeField]
    Tile _placedTileBase;
    DungeonSave _dungeonSave;
    [SerializeField]
    List<string> _tileNames = new List<string>();
    void Start()
    {
        _dungeonSave = GameObject.Find("Save").GetComponent<DungeonSave>();
        LoadTiles();
        if (_allTileDatas != null)
        {
            if (_allTileDatas._tiles.Count > 0)
            {
                _placedTileBase = ScriptableObject.CreateInstance<Tile>();
                for (int i = 0; i < _allTileDatas._tiles.Count; i++)
                {
                    if (_allTileDatas._tiles[i]._tileName.Contains(":"))
                    {
                        _tileNames.Add(_allTileDatas._tiles[i]._tileName.Substring(0, _allTileDatas._tiles[i]._tileName.IndexOf(":")));
                    }
                    else
                    {
                        _tileNames.Add(_allTileDatas._tiles[i]._tileName);
                    }
                }
               MakeSquare(100, 100);
               PlaceAddedTiles();
            } 
        }
    }
    void Place(Vector3Int _location)
    {
             int _tileID = _allTileDatas._tiles[Random.Range(0, _allTileDatas._tiles.Count)]._tileID;
            TileDG copy = new TileDG
            {
                _health = _allTileDatas._tiles[_tileID]._health,
                _tileID = _allTileDatas._tiles[_tileID]._tileID,
                _tileName = _allTileDatas._tiles[_tileID]._tileName,
            };
                _placedTiles.Add(_location, copy);
                _placedTileBase.sprite = _atlas.GetSprite(_tileNames[_tileID]);
                _placedTileBase.color = _allTileDatas._tiles[_tileID].GetTileColour();
                _map.SetTile(_location, _placedTileBase);
                if(_dungeonSave._invalidPositions.Contains(_location))
                {
                    _placedTiles.Remove(_location);
                     _map.SetTile(_location, null);
                }
    }
    void PlaceAddedTiles()
    {
       for (int i = 0; i < _dungeonSave._dataToSave._addedTiles.Count; i++)
       {
        Vector3Int newPos = new Vector3Int(_dungeonSave._dataToSave._addedTiles[i]._tileXPos, _dungeonSave._dataToSave._addedTiles[i]._tileYPos, 0);
                    TileDG copy = new TileDG
                    {
                        _tileID = _dungeonSave._dataToSave._addedTiles[i]._tileID,
                        _health =  _dungeonSave._dataToSave._addedTiles[i]._health,
                        _tileName =  _dungeonSave._dataToSave._addedTiles[i]._tileName
                    };
                 //   _placedTiles.Remove(newPos);
                  _map.SetTile(newPos, null);
                   _placedTiles.Remove(newPos);
                _placedTiles.Add(newPos, copy);
                _placedTileBase.sprite = _atlas.GetSprite(_tileNames[copy._tileID]);
                      _placedTileBase.color = _allTileDatas._tiles[copy._tileID].GetTileColour();
                _map.SetTile(newPos, _placedTileBase);
       }
    }
    void MakeSquare(int xDimension, int yDimension)
    {
        for (int x = 0; x < xDimension; x++)
        {
            for (int y = 0; y < yDimension; y++)
            {
                Place(new Vector3Int(x, y, 0));
            }
        }
    }
    void LoadTiles()
    {
        if (File.Exists(Application.dataPath + "/Resources/TileDatas.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/Resources/TileDatas.dat", FileMode.Open);
            _allTileDatas = (AllTiles)bf.Deserialize(file);
            file.Close();
            _tilesLoaded = true;
        }
    }
}
