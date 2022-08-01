
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class LandMap : Map
{
    private PerlinNoise _heightMap = new PerlinNoise(Dynamic.MapData.scale, Constant.Map.range, Constant.Map.seed);
    private static PerlinNoise _heatMap = new PerlinNoise(250, 10, new Vector2(23628, 2479));
    private static PerlinNoise _rainMap = new PerlinNoise(150, 10, new Vector2(98428, 5479));
    private static PerlinNoise _edgeNoise = new PerlinNoise(12, 2, new Vector2(5000, 79278));

    private int GetLayer(float height)
    {
        
        float water = Dynamic.MapData.waterLevel;
        if (height < water / 2) return 0;
        if (height < water) return 1;
        if (height < water + 0.3f) return 2;
        return 3;
    }

    private Biome GetBiome(int x, int y)
    {
        float heat = _heatMap.GetValue(x, y) + _edgeNoise.GetValue(x, y) / 4;
        float rain = _rainMap.GetValue(x, y) + _edgeNoise.GetValue(x, y) / 4;

        for (int i = 0; i < GameAssets.instance.biomes.Count; i++)
        {
            if (heat < GameAssets.instance.biomes[i].heatRange.y && heat > GameAssets.instance.biomes[i].heatRange.x)
            {
                if (rain < GameAssets.instance.biomes[i].rainRange.y &&
                    rain > GameAssets.instance.biomes[i].rainRange.x)
                {
                    return GameAssets.instance.biomes[i];
                }
            }
        }
        
        return GameAssets.instance.defaultBiome;
    }
    
    public TileBase GetTile(int x, int y)
    {
        float height = _heightMap.GetValue(x, y) + _edgeNoise.GetValue(x, y) / 2;
        int layer = GetLayer(height);
        Biome biome = GetBiome(x, y);

        if (biome.groundTiles[layer] == null)
        {
            return GameAssets.instance.defaultBiome.groundTiles[layer];
        }
        
        return biome.groundTiles[layer];
    }

    public void UpdateMap()
    {
        _heightMap = new PerlinNoise(Dynamic.MapData.scale, Constant.Map.range, Constant.Map.seed);
    }
}
