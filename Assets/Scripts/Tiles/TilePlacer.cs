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
    Tile _placedTileBase;
    DungeonSave _dungeonSave;
    void Start()
    {
        _dungeonSave = GameObject.Find("Save").GetComponent<DungeonSave>();
        LoadTiles();
        if (_allTileDatas != null)
        {
            if (_allTileDatas.Tiles.Count > 0)
            {
                _placedTileBase = ScriptableObject.CreateInstance<Tile>();
                MakeSquare(100, 100);
            } 
        }
    }
    void Place(Vector3Int _location)
    {
   
        if (!_dungeonSave._invalidPositions.Contains(_location))
        {
            int tileID = _allTileDatas.Tiles[Random.Range(0, _allTileDatas.Tiles.Count)].TileID;
            TileDG copy = new TileDG
            {
                Health = _allTileDatas.Tiles[tileID].Health,
                TileID = _allTileDatas.Tiles[tileID].TileID,
                TileName = _allTileDatas.Tiles[tileID].TileName
            };
            if(!_placedTiles.ContainsKey(_location))
            _placedTiles.Add(_location, copy);
            _placedTileBase.sprite = _atlas.GetSprite(copy.TileName);
            _map.SetTile(_location, _placedTileBase);
        }
        for (int i = 0; i < _dungeonSave._addedTiles.Count; i++)
        {
            TileDG copy = new TileDG
            {
                Health = _allTileDatas.Tiles[_dungeonSave._addedTiles[i]._tileID].Health,
                TileID = _allTileDatas.Tiles[_dungeonSave._addedTiles[i]._tileID].TileID,
                TileName = _allTileDatas.Tiles[_dungeonSave._addedTiles[i]._tileID].TileName
            };
            Vector3Int newPos = new Vector3Int(_dungeonSave._addedTiles[i]._tileXPos, _dungeonSave._addedTiles[i]._tileYPos, 0);
            if (_placedTiles.ContainsKey(newPos))
            {
                _placedTiles.Remove(newPos);
            }
            _placedTiles.Add(newPos, copy);
            _placedTileBase.sprite = _atlas.GetSprite(copy.TileName);
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
