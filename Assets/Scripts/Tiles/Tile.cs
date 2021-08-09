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
    public int[] _colour = new int[4];
}
[Serializable]
public class AllTiles
{
    public List<TileDG> _tiles = new List<TileDG>();
}
