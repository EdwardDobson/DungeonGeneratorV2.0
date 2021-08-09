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
    public string TileName;
    public int TileID;
    public TileType TileType;
    public int Health;
}
[Serializable]
public class AllTiles
{
    public List<TileDG> Tiles = new List<TileDG>();
}
