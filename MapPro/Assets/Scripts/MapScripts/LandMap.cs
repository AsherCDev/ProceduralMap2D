
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class LandMap
{
    private PerlinNoise _heightMap = new PerlinNoise(40, Constant.Map.range, Constant.Map.seed);

    private int GetLayer(float height)
    {
        float water = Dynamic.MapData.waterLevel;
        if (height < water / 2) return 0;
        if (height < water) return 1;
        if (height < water + 1) return 2;
        return 3;
    }
    
    public TileBase GetTile(int x, int y)
    {
        float height = _heightMap.GetValue(x, y);
        int layer = GetLayer(height);
        
        return GameAssets.instance.landTiles[layer];
    }

    public void UpdateMap()
    {
        _heightMap = new PerlinNoise(Dynamic.MapData.scale, Constant.Map.range, Constant.Map.seed);
    }
}
