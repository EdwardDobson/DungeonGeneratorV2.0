using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

public class ClearTileDataFile : EditorWindow
{
    static AllTiles AllTiles;
    [MenuItem("Dungeon Generator Windows/CLEAR ALL TILE DATAS",false,3)]
    static void Init()
    {
        AllTiles = new AllTiles();
        if (File.Exists(Application.dataPath + "/Resources/TileDatas.dat"))
        {
            AllTiles._tiles.Clear();
            FileUtil.DeleteFileOrDirectory(Application.dataPath + "/Resources/TileDatas.dat");
            FileUtil.DeleteFileOrDirectory(Application.dataPath + "/Resources/TileDatas.dat.meta");
            AssetDatabase.Refresh();
        }
    }
}
