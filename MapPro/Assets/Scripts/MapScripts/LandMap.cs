
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class LandMap
{
    private PerlinNoise _heightMap = new PerlinNoise(40, Constant.Map.range, Constant.Map.seed);

    private int GetLayer(float height)
    {
        for (int i = 0; i < Dynamic.MapData.layers.Count; i++)
        {
            if (height <= Dynamic.MapData.layers[i])
            {
                return i;
            }
        }

        return Dynamic.MapData.layers.Count;
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
