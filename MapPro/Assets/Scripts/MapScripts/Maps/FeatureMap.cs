
using UnityEngine;
using UnityEngine.Tilemaps;

public class FeatureMap
{
    private PerlinNoise _rockMap = new PerlinNoise(1.5f, 10, new Vector2(309, 3598));

    public TileBase GetTile(int x, int y)
    {
        if (Dynamic.MapData.landMap.GetTile(x, y) == GameAssets.instance.defaultBiome.groundTiles[3])
        {
            if (_rockMap.GetValue(x, y) > 7.9 && _rockMap.GetValue(x, y) < 8) return GameAssets.instance.rocks[0];
            if (_rockMap.GetValue(x, y) > 7 && _rockMap.GetValue(x, y) < 7.1) return GameAssets.instance.rocks[1];
            if (_rockMap.GetValue(x, y) > 3.6 && _rockMap.GetValue(x, y) < 3.7) return GameAssets.instance.rocks[2];
        }

        return null;
    }
}
