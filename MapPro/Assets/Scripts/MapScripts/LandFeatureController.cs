
using UnityEngine;
using UnityEngine.Tilemaps;

public class LandFeatureController
{
    private Tilemap _tileMap;
    private FeatureMap _featureMap = new FeatureMap();

    public LandFeatureController(Tilemap tileMap)
    {
        _tileMap = tileMap;
    }
    
    public void SetChunk(int x, int y, bool setMode)
    {
        // Sets each tile in a given chunk based on the map data
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                Vector3Int position = new Vector3Int(x * 16 + i, y * 16 + j, 0);
                if (setMode)
                {
                    if (_featureMap.GetTile(position.x, position.y))
                    {
                        _tileMap.SetTile(position, _featureMap.GetTile(position.x, position.y));
                    }
                }
                else
                {
                    _tileMap.SetTile(position, null);
                }
            }
        }
    }
}
