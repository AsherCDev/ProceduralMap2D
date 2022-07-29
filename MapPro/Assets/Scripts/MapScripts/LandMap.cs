
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class LandMap
{
    private PerlinNoise _heightMap = new PerlinNoise(50, 10, Constant.Map.seed);
    private List<float> _layers = new List<float> {1f, 2f, 2.2f, 8f};

    private int GetLayer(float height)
    {
        for (int i = 0; i < _layers.Count; i++)
        {
            if (height <= _layers[i])
            {
                return i;
            }
        }

        return _layers.Count;
    }
    
    public TileBase GetTile(int x, int y)
    {
        float height = _heightMap.GetValue(x, y);
        int layer = GetLayer(height);
        
        return GameAssets.instance.landTiles[layer];
    }
}
