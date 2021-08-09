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
    static int _tileIndex;
    [MenuItem("Dungeon Generator Windows/Tile Creator",false,1)]
    static void Init()
    {
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
        Texture2D displaySprite = _tileTextures[_tileIndex].texture;
        _tileIndex = EditorGUILayout.Popup(_tileIndex, _tileTextureNames.ToArray());
        _tile.TileName = _tileTextureNames[_tileIndex];
        EditorGUILayout.LabelField(new GUIContent(displaySprite), GUILayout.MaxWidth(100f), GUILayout.MaxHeight(100f));
        EditorGUILayout.LabelField("Tile Health");
        _tile.Health = EditorGUILayout.IntField(_tile.Health);
        if (GUILayout.Button("Create New Tile"))
        {
            if (_allTiles != null)
            {
                if (_allTiles.Tiles.Count >= 1)
                    _tile.TileID = _allTiles.Tiles.Count;
                else
                    _tile.TileID = 0;
                _allTiles.Tiles.Add(_tile);
                SaveLoadTileData.SaveData(_allTiles);
                Debug.Log("Created new tile: " + _tile.TileName);
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
        if (File.Exists(Application.dataPath + "/Resources/TileDatas.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/Resources/TileDatas.dat", FileMode.Open);
            _allTiles = (AllTiles)bf.Deserialize(file);
            file.Close();
        }
    }
}
