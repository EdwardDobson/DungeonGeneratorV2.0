using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
[Serializable]
public struct ModifedTile
{
    public int _tileID;
    public int _tileXPos;
    public int _tileYPos;
}
[Serializable]
public struct DataToSave
{
    public int _seed;
    public float[] _playerPos;
    public List<int> _tileXPos;
    public List<int> _tileYPos;
    public List<ModifedTile> _addedTiles;

}
public class DungeonSave : MonoBehaviour
{
    public DataToSave _dataToSave;
    public int _seed;
    public List<Vector3Int> _invalidPositions = new List<Vector3Int>();
    public List<ModifedTile> _addedTiles = new List<ModifedTile>();
    GameObject _player;

    void Awake()
    {
        _dataToSave = new DataToSave();
        _player = GameObject.Find("Player");
        _dataToSave._tileXPos = new List<int>();
        _dataToSave._tileYPos = new List<int>();
        _dataToSave._addedTiles = new List<ModifedTile>();
        LoadGame();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveGame();
        }
    }
    void LoadGame()
    {

        if (File.Exists(Application.dataPath + "/Resources/SaveGame.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/Resources/SaveGame.dat", FileMode.Open);
            _dataToSave._playerPos = new float[3];
            _dataToSave = (DataToSave)bf.Deserialize(file);
            file.Close();
            UnityEngine.Random.InitState(_dataToSave._seed);
            _player.transform.position = new Vector3(_dataToSave._playerPos[0], _dataToSave._playerPos[1], _dataToSave._playerPos[2]);
            for(int i = 0; i < _dataToSave._addedTiles.Count; i++)
            {
                _addedTiles.Add(_dataToSave._addedTiles[i]);
            }
     
            for (int i = 0; i< _dataToSave._tileXPos.Count; i++)
            {
                _invalidPositions.Add(new Vector3Int(_dataToSave._tileXPos[i], _dataToSave._tileYPos[i],0));
            }
        }
        else
        {
            UnityEngine.Random.InitState(UnityEngine.Random.Range(0, int.MaxValue));
        }
    }
    void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/Resources/SaveGame.dat");
        _dataToSave._seed = _seed;
        _dataToSave._playerPos = new float[3];
        _dataToSave._playerPos[0] = _player.transform.position.x;
        _dataToSave._playerPos[1] = _player.transform.position.y;
        _dataToSave._playerPos[2] = _player.transform.position.z;
        bf.Serialize(file, _dataToSave);
        file.Close();
        AssetDatabase.Refresh();
        Debug.Log("Saved Game");
    }
}
