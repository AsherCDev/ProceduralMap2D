
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class GroundController
{
    private Tilemap _tileMap;


    private List<Vector2Int> _loadedChunks = new List<Vector2Int>();
    
    public GroundController(Tilemap tilemap)
    {
        _tileMap = tilemap;
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
                    TileBase tile = Dynamic.MapData.landMap.GetTile(position.x, position.y);
                    int rot = Random.Range(0, 3);
                    Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, 90f * rot), Vector3.one);
                    _tileMap.SetTile(position, tile);
                    _tileMap.SetTransformMatrix(position, matrix);
                }
                else
                {
                    _tileMap.SetTile(position, null);
                }
            }
        }
    }
}
