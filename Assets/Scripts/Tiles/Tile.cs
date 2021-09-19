using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TileType
{
    Wall,
    Floor
}
[Serializable]
public class TileDG
{
    public string _tileName;
    public int _tileID;
    public TileType _tileType;
    public int _health;
    public int _speed;
    public float[] _colour = new float[4];
    public float _rarity;

    public  Color GetTileColour()
    {
        Color newColor = new Color(_colour[0], _colour[1], _colour[2], _colour[3]);
        return newColor;
    }
    public void SetTileColour(Color colour)
    {
        _colour[0] = colour.r;
        _colour[1] = colour.g;
        _colour[2] = colour.b;
        _colour[3] = colour.a;
    }
}
[Serializable]
public class AllTiles
{
    public List<TileDG> _tiles = new List<TileDG>();
}
