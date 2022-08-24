
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class LandMap
{
    private PerlinNoise _heightMap = new PerlinNoise(Dynamic.MapData.scale, Constant.Map.perlinRange, Constant.Map.defaultSeed);
    private static PerlinNoise _heatMap = new PerlinNoise(250, Constant.Map.perlinRange,Constant.Map.defaultSeed);
    private static PerlinNoise _rainMap = new PerlinNoise(100, Constant.Map.perlinRange, Constant.Map.defaultSeed);
    private static PerlinNoise _edgeNoise = new PerlinNoise(10, 2, Constant.Map.defaultSeed);

    private int GetLayer(float height)
    {
        
        float water = Dynamic.MapData.waterLevel;
        if (height < water / 2) return 0;
        if (height < water) return 1;
        if (height < water + 0.3f) return 2;
        return 3;
    }

    public Biome GetBiome(int x, int y)
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
        _heightMap = new PerlinNoise(Dynamic.MapData.scale, Constant.Map.perlinRange, SeedGenerator.GetSeed(1));
        _heatMap = new PerlinNoise(5 * Dynamic.MapData.biomeScale, Constant.Map.perlinRange, SeedGenerator.GetSeed(2));
        _rainMap = new PerlinNoise(2 * Dynamic.MapData.biomeScale, Constant.Map.perlinRange, SeedGenerator.GetSeed(3));
        _edgeNoise = new PerlinNoise(Dynamic.MapData.sharpness, 2, SeedGenerator.GetSeed(4));
    }
}
