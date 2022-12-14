using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets instance
    {
        get
        {
            if (_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }
    
    public Biome defaultBiome;
    public List<Biome> biomes;

    public List<TileBase> rocks;
    public TileBase pineTree;
}