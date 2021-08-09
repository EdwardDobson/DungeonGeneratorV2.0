using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;
using UnityEngine.U2D;

public class TileEditor : EditorWindow
{
    static int _tileIndex;
    static int _tileTypeIndex;
    static TileDG _tileToChange;
    static AllTiles _allTiles;
    static SpriteAtlas _atlas;
    static List<string> _tileTextureNames = new List<string>();
    static List<string> _tileTypeNames = new List<string>();
    [MenuItem("Dungeon Generator Windows/Tile Editor",false, 2)]
    static void Init()
    {
        _tileToChange = new TileDG();
        ReloadData();
        TileEditor window = (TileEditor)GetWindow(typeof(TileEditor));
        window.Show();
    }
    private void OnFocus()
    {
        ReloadData();
    }
    private void OnGUI()
    {
        if (File.Exists(Application.dataPath + "/Resources/TileDatas.dat"))
        {
            if (_allTiles != null)
            {
                if (_allTiles._tiles.Count > 0)
                {
                    EditorGUILayout.LabelField("Tile");
                    _tileIndex = EditorGUILayout.Popup(_tileIndex, _tileTextureNames.ToArray());
                    _tileToChange = _allTiles._tiles[_tileIndex];
                    Texture2D displaySprite = _atlas.GetSprite(_allTiles._tiles[_tileIndex]._tileName).texture;
                    EditorGUILayout.LabelField(new GUIContent(displaySprite), GUILayout.MaxWidth(100f), GUILayout.MaxHeight(100f));
                    _tileTypeIndex = EditorGUILayout.Popup("Tile Type: ", _tileTypeIndex, _tileTypeNames.ToArray());
                    _tileToChange._tileType = (TileType)_tileTypeIndex;
                    _tileToChange._health = EditorGUILayout.IntField("Tile Health: ", _tileToChange._health);
                    _tileToChange._speed = EditorGUILayout.IntField("Tile Speed: ", _tileToChange._speed);
                    ZeroValue(ref _tileToChange._speed);
                    ZeroValue(ref _tileToChange._health);
                    _tileToChange._colour[0] = EditorGUILayout.IntField("R: ", _tileToChange._colour[0]);
                    _tileToChange._colour[1] = EditorGUILayout.IntField("G: ", _tileToChange._colour[1]);
                    _tileToChange._colour[2] = EditorGUILayout.IntField("B: ", _tileToChange._colour[2]);
                    _tileToChange._colour[3] = EditorGUILayout.IntField("A: ", _tileToChange._colour[3]);
                    ZeroValue(ref _tileToChange._colour[0]);
                    ZeroValue(ref _tileToChange._colour[1]);
                    ZeroValue(ref _tileToChange._colour[2]);
                    ZeroValue(ref _tileToChange._colour[3]);
                    EditorGUILayout.LabelField("Tile Index: " + _tileToChange._tileID);
                    if (GUILayout.Button("Save Changes"))
                    {
                        _allTiles._tiles[_tileIndex]._health = _tileToChange._health;
                        _allTiles._tiles[_tileIndex]._tileName = _tileToChange._tileName;
                        _allTiles._tiles[_tileIndex]._speed = _tileToChange._speed;
                        _allTiles._tiles[_tileIndex]._tileType = _tileToChange._tileType;
                        _allTiles._tiles[_tileIndex]._colour[0] = _tileToChange._colour[0];
                        _allTiles._tiles[_tileIndex]._colour[1] = _tileToChange._colour[1];
                        _allTiles._tiles[_tileIndex]._colour[2] = _tileToChange._colour[2];
                        _allTiles._tiles[_tileIndex]._colour[3] = _tileToChange._colour[3];



                        SaveLoadTileData.SaveData(_allTiles);
                        Debug.Log("Modifed tile: " + _tileToChange._tileName);
                    }
                    if (GUILayout.Button("Remove Tile"))
                    {
                        _allTiles._tiles.RemoveAt(_tileIndex);
                        for (int i = 0; i < _allTiles._tiles.Count; i++)
                        {
                            _allTiles._tiles[i]._tileID = i;
                        }
                        SaveLoadTileData.SaveData(_allTiles);
                        ReloadData();
                    }
                }
            }
        }
        else
        {
            EditorGUILayout.LabelField("CUSTOM TILE DATA FILE DOES NOT EXIST");
        }
    }
    void ZeroValue(ref int _value)
    {
        if (_value <= 0)
        {
            _value = 0;
        }
    }
    static void ReloadData()
    {
        _tileToChange = new TileDG();
        _tileTextureNames = new List<string>();
        _atlas = new SpriteAtlas();
        _atlas = Resources.Load<SpriteAtlas>("TileTextures/TileSprites");
        _tileTextureNames = new List<string>();      

        if (File.Exists(Application.dataPath + "/Resources/TileDatas.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.dataPath + "/Resources/TileDatas.dat", FileMode.Open);
            _allTiles = (AllTiles)bf.Deserialize(file);
            file.Close();
            if (_allTiles != null)
            {
                for (int i = 0; i < _allTiles._tiles.Count; i++)
                {
                    if (!_tileTextureNames.Contains(_allTiles._tiles[i]._tileName))
                        _tileTextureNames.Add(_allTiles._tiles[i]._tileName);
                }
            }
            foreach (TileType i in Enum.GetValues(typeof(TileType)))
            {
                _tileTypeNames.Add(i.ToString());
            }
        }
    }
}
