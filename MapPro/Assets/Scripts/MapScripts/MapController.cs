
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    private Tilemap _tileMap;
    private LandMap _landMap;

    private List<Vector2Int> _loadedChunks = new List<Vector2Int>();

    private void Start()
    {
        _tileMap = GetComponent<Tilemap>();
        EventManager.instance.onChunkUpdate += UpdateChunks;
        _landMap = new LandMap();
    }

    private void UpdateChunks()
    {
        // Load chunks within render distance
        for (int x = -Constant.Map.renderDistance; x <= Constant.Map.renderDistance; x++)
        {
            for (int y = -Constant.Map.renderDistance; y <= Constant.Map.renderDistance; y++)
            {
                int chunkX = Dynamic.Player.chunkPosition.x + x;
                int chunkY = Dynamic.Player.chunkPosition.y + y;
                SetChunk(chunkX, chunkY, true);
                _loadedChunks.Add(new Vector2Int(chunkX, chunkY));
            }
        }

        // Unload chunks outside of render distance
        for (int i = 0; i < _loadedChunks.Count; i++)
        {
            if (Mathf.Abs(_loadedChunks[i].x - Dynamic.Player.chunkPosition.x) > Constant.Map.renderDistance ||
                Mathf.Abs(_loadedChunks[i].y - Dynamic.Player.chunkPosition.y) > Constant.Map.renderDistance)
            {
                SetChunk(_loadedChunks[i].x, _loadedChunks[i].y, false);
                _loadedChunks.Remove(_loadedChunks[i]);
            }
        }
    }

    private void SetChunk(int x, int y, bool setMode)
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                Vector3Int position = new Vector3Int(x * 16 + i, y * 16 + j, 0);
                if (setMode)
                {
                    TileBase tile = _landMap.GetTile(position.x, position.y);
                    _tileMap.SetTile(position, tile);
                }
                else
                {
                    _tileMap.SetTile(position, null);
                }
            }
        }
    }
}
