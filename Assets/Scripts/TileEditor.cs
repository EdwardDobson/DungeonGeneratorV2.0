using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;

public class TileEditor : EditorWindow
{
    static int _tileIndex;
    static TileDG _tileToChange;
    static AllTiles _allTiles;
    static List<string> _tileTextureNames = new List<string>();
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
                if (_allTiles.Tiles.Count > 0)
                {
                    EditorGUILayout.LabelField("Tile");
                    _tileIndex = EditorGUILayout.Popup(_tileIndex, _tileTextureNames.ToArray());
                    _tileToChange = _allTiles.Tiles[_tileIndex];
                    _allTiles.Tiles[_tileIndex].Health = EditorGUILayout.IntField(_tileToChange.Health);
                    EditorGUILayout.LabelField("Tile Index: " + _tileToChange.TileID);
                    if (GUILayout.Button("Save Changes"))
                    {
                        _allTiles.Tiles[_tileIndex].Health = _tileToChange.Health;
                        _allTiles.Tiles[_tileIndex].TileName = _tileToChange.TileName;
                        SaveLoadTileData.SaveData(_allTiles);
                        Debug.Log("Modifed tile: " + _tileToChange.TileName);
                    }
                    if (GUILayout.Button("Remove Tile"))
                    {
                        _allTiles.Tiles.RemoveAt(_tileIndex);
                        for (int i = 0; i < _allTiles.Tiles.Count; i++)
                        {
                            _allTiles.Tiles[i].TileID = i;
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
    static void ReloadData()
    {
        _tileToChange = new TileDG();
        _tileTextureNames = new List<string>();
        if (File.Exists(Application.dataPath + "/Resources/TileDatas.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.dataPath + "/Resources/TileDatas.dat", FileMode.Open);
            _allTiles = (AllTiles)bf.Deserialize(file);
            file.Close();
            if (_allTiles != null)
            {
                for (int i = 0; i < _allTiles.Tiles.Count; i++)
                {
                    if (!_tileTextureNames.Contains(_allTiles.Tiles[i].TileName))
                        _tileTextureNames.Add(_allTiles.Tiles[i].TileName);
                }
            }
        }
    }
}
