using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class EditorFunctions : EditorWindow
{
    /// <summary>
    /// Saves all of the required data to the Tile Datas file.
    /// </summary>
    /// <param name="AllTiles"></param>
    public static void SaveData(AllTiles AllTiles)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/Resources/TileDatas.dat");
        bf.Serialize(file, AllTiles);
        file.Close();
        AssetDatabase.Refresh();
    }
    /// <summary>
    /// Makes sure the value doesn't drop below zero
    /// </summary>
    /// <param name="_value"></param>
    /// <returns></returns>
    public static int ZeroValue(ref int _value)
    {
        if (_value <= 0)
        {
            return _value = 0;
        }
        else
        {
            return _value;
        }
    }
    /// <summary>
    /// Makes sure the value doesn't drop below zero
    /// </summary>
    /// <param name="_value"></param>
    /// <returns></returns>
    public static float  ZeroValue(ref float value)
    {
        if (value <= 0)
        {
            return value = 0;
        }
        else
        {
            return value;
        }
    }
    /// <summary>
    /// Displays the correct texture for the tile.
    /// </summary>
    /// <param name="atlas"></param>
    /// <param name="spriteName"></param>
    public static void DisplaySprite(SpriteAtlas atlas, string spriteName)
    {
        Texture2D displaySprite = atlas.GetSprite(spriteName).texture;
        EditorGUILayout.LabelField(new GUIContent(displaySprite), GUILayout.MaxWidth(100f), GUILayout.MaxHeight(100f));
       
    }
}
