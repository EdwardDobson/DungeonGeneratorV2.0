using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class SaveLoadTileData : EditorWindow
{
    public static void SaveData(AllTiles AllTiles)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/Resources/TileDatas.dat");
        bf.Serialize(file, AllTiles);
        file.Close();
        AssetDatabase.Refresh();
    }
}
