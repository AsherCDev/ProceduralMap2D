
using UnityEngine;
using UnityEngine.Tilemaps;

public class FeatureMap
{
    private PerlinNoise _rockMap = new PerlinNoise(1.5f, 10, new Vector2(309, 3598));
    private PerlinNoise _pineTreeMap = new PerlinNoise(1.25f, 10, new Vector2(9786, 3598));

    public TileBase GetTile(int x, int y)
    {
        if (Dynamic.MapData.landMap.GetTile(x, y) == GameAssets.instance.defaultBiome.groundTiles[3])
        {
            if (_rockMap.GetValue(x, y) > 7.9 && _rockMap.GetValue(x, y) < 8) return GameAssets.instance.rocks[0];
            if (_rockMap.GetValue(x, y) > 7 && _rockMap.GetValue(x, y) < 7.1) return GameAssets.instance.rocks[1];
            if (_rockMap.GetValue(x, y) > 3.6 && _rockMap.GetValue(x, y) < 3.7) return GameAssets.instance.rocks[2];
        }
        else if (Dynamic.MapData.landMap.GetTile(x, y) == GameAssets.instance.biomes[3].groundTiles[3])
        {
            if (x % 3 == 0 && y % 3 == 0)
            {
                if (((x + y) / 2 % 2  == 0 && _pineTreeMap.GetValue(x,y) > 5) || _pineTreeMap.GetValue(x,y) < 4) return GameAssets.instance.pineTree;
            }

        }

        return null;
    }
}
