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
    static Color _newColor = new Color();

    #region Tile Values
    static int _tileHealth = new int();
    static int _tileSpeed = new int();
    static float _tileRarity = new float();
    static string _tileName;
    static TileType _tileType;
    #endregion

    [MenuItem("Dungeon Generator Windows/Tile Editor", false, 2)]
    static void Init()
    {
        _tileToChange = new TileDG();
        ReloadData();
        TileEditor window = (TileEditor)GetWindow(typeof(TileEditor));
        window.Show();
        _tileToChange = _allTiles._tiles[_tileIndex];
        _newColor = _tileToChange.GetTileColour();
        _tileType = _allTiles._tiles[_tileIndex]._tileType;
        _tileHealth = _allTiles._tiles[_tileIndex]._health;
        _tileSpeed = _allTiles._tiles[_tileIndex]._speed;
        _tileRarity = _allTiles._tiles[_tileIndex]._rarity;
        _tileName = _allTiles._tiles[_tileIndex]._tileName;
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
                    EditorGUI.BeginChangeCheck();
                    _tileIndex = EditorGUILayout.Popup(_tileIndex, _tileTextureNames.ToArray());
                    if(EditorGUI.EndChangeCheck())
                    {
                        _newColor = _allTiles._tiles[_tileIndex].GetTileColour();
                        _tileType = _allTiles._tiles[_tileIndex]._tileType;
                        _tileHealth = _allTiles._tiles[_tileIndex]._health;
                        _tileSpeed = _allTiles._tiles[_tileIndex]._speed;
                        _tileRarity = _allTiles._tiles[_tileIndex]._rarity;
                        _tileName = _allTiles._tiles[_tileIndex]._tileName;
                    }
                    
                    if (_tileName.Contains(":"))
                    {
                        string newTileName = _tileName.Substring(0, _tileName.IndexOf(":"));
                        EditorFunctions.DisplaySprite(_atlas, newTileName);
                    }
                    else
                    {
                        EditorFunctions.DisplaySprite(_atlas, _tileName);
                    }

                    _tileTypeIndex = EditorGUILayout.Popup("Tile Type: ", _tileTypeIndex, _tileTypeNames.ToArray());
                    _tileType = (TileType)_tileTypeIndex;
                    _tileHealth = EditorGUILayout.IntField("Tile Health: ", EditorFunctions.ZeroValue(ref _tileHealth));
                   _tileSpeed = EditorGUILayout.IntField("Tile Speed: ", EditorFunctions.ZeroValue(ref _tileSpeed));
                    _tileRarity = EditorGUILayout.FloatField("Tile Rarity: ", EditorFunctions.ZeroValue(ref _tileRarity));

                    _newColor = EditorGUILayout.ColorField("Tile Color", _newColor);
      
                    EditorGUILayout.LabelField("Tile Index: " + _allTiles._tiles[_tileIndex]._tileID);

                    if (GUILayout.Button("Save Changes"))
                    {
                        _allTiles._tiles[_tileIndex]._health = _tileHealth;
                        _allTiles._tiles[_tileIndex]._speed = _tileSpeed;
                        _allTiles._tiles[_tileIndex]._rarity = _tileRarity;
                        _allTiles._tiles[_tileIndex]._tileType = _tileType;
                        _allTiles._tiles[_tileIndex].SetTileColour(_newColor);
                        EditorFunctions.SaveData(_allTiles);
                    }
                    if (GUILayout.Button("Remove Tile"))
                    {
                        _allTiles._tiles.RemoveAt(_tileIndex);
                        for (int i = 0; i < _allTiles._tiles.Count; i++)
                        {
                            _allTiles._tiles[i]._tileID = i;
                        }
                        EditorFunctions.SaveData(_allTiles);
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
    static void ReloadData()
    {
        _tileToChange = new TileDG();
        _tileTextureNames = new List<string>();
        _atlas = new SpriteAtlas();
        _atlas = Resources.Load<SpriteAtlas>("TileTextures/TileSprites");
    
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