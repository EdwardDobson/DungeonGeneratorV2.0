using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;
public class TileCreator : EditorWindow
{
    static TileDG _tile;
    static AllTiles _allTiles;
    static SpriteAtlas _atlas;
    static Sprite[] _tileTextures;
    static List<string> _tileTextureNames = new List<string>();
    static List<string> _tileTypeNames = new List<string>();
    static int _tileIndex;
    static int _tileTypeIndex;
    static Color _newColor = new Color();
    [MenuItem("Dungeon Generator Windows/Tile Creator",false,1)]
    static void Init()
    {
        _newColor = new Color(255,255,255,255);
        LoadData();
        TileCreator window = (TileCreator)GetWindow(typeof(TileCreator));
        window.Show();
    }
    private void OnFocus()
    {
        LoadData();
    }
    private void OnGUI()
    {
        _tileIndex = EditorGUILayout.Popup(_tileIndex, _tileTextureNames.ToArray());
        if (_tileTextures[_tileIndex] != null)
        {
            EditorFunctions.DisplaySprite(_atlas, _tileTextureNames[_tileIndex]);
        }
        _tile._tileName = _tileTextureNames[_tileIndex];
        _tileTypeIndex = EditorGUILayout.Popup("Tile Type: ", _tileTypeIndex, _tileTypeNames.ToArray());
        _tile._tileType = (TileType)_tileTypeIndex;
        _tile._health = EditorGUILayout.IntField("Tile Health: ", EditorFunctions.ZeroValue(ref _tile._health));
        _tile._speed = EditorGUILayout.IntField("Tile Speed: ", EditorFunctions.ZeroValue(ref _tile._speed));
        _tile._rarity = EditorGUILayout.FloatField("Tile Rarity: ", EditorFunctions.ZeroValue(ref _tile._rarity));
        _newColor = EditorGUILayout.ColorField("Tile Color", _newColor);
        _tile.SetTileColour(_newColor);

        if (GUILayout.Button("Create New Tile"))
        {
            if (_allTiles != null)
            {
                if (_allTiles._tiles.Count >= 1)
                    _tile._tileID = _allTiles._tiles.Count;
                else
                    _tile._tileID = 0;
                for(int i = 0; i < _allTiles._tiles.Count; i++)
                {
                    if(_allTiles._tiles[i]._tileName.Contains(_tile._tileName))
                    {
                        _tile._tileName = _tile._tileName + ":" + _tile._tileID;
                    }
                }
                _allTiles._tiles.Add(_tile);
                EditorFunctions.SaveData(_allTiles);
                Debug.Log("Created new tile: " + _tile._tileName);
                _tile = new TileDG();
            }
        }
    }
    static void LoadData()
    {
        _tile = new TileDG();
        _allTiles = new AllTiles();
        _atlas = new SpriteAtlas();
        _atlas = Resources.Load<SpriteAtlas>("TileTextures/TileSprites");
        _tileTextures = new Sprite[_atlas.spriteCount];
        _atlas.GetSprites(_tileTextures);
        _tileTextureNames = new List<string>();
        for (int i = 0; i < _tileTextures.Length; i++)
        {
            if(!_tileTextureNames.Contains(_tileTextures[i].name))
            _tileTextureNames.Add(_tileTextures[i].name.Replace("(Clone)", ""));
        }
        foreach (TileType i in Enum.GetValues(typeof(TileType)))
        {
            _tileTypeNames.Add(i.ToString());
        }
        if (File.Exists(Application.dataPath + "/Resources/TileDatas.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/Resources/TileDatas.dat", FileMode.Open);
            _allTiles = (AllTiles)bf.Deserialize(file);
            file.Close();
        }
    }
}
