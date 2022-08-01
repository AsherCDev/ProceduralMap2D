
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class GridController : MonoBehaviour
{
    private Tilemap _tileMap;
    
    private LandMap _landMap;
    private Map _currentMap;

    private List<Vector2Int> _loadedChunks = new List<Vector2Int>();
    
    private void Start()
    {
        _tileMap = GetComponent<Tilemap>();
        
        _landMap = new LandMap();
        _currentMap = _landMap;
        EventManager.instance.onChunkUpdate += UpdateChunks;
        EventManager.instance.onMapUpdate += _currentMap.UpdateMap;
        EventManager.instance.onMapUpdate += ReloadChunks;
    }

    private void ReloadChunks()
    {
        ClearAllChunks();
        UpdateChunks();
    }

    private void UpdateChunks()
    {
        LoadChunksInRange();
        ClearChunksOutOfRange();
    }

    private void LoadChunksInRange()
    {
        // Load chunks within render distance
        for (int x = -Constant.Map.renderDistance; x <= Constant.Map.renderDistance; x++)
        {
            for (int y = -Constant.Map.renderDistance; y <= Constant.Map.renderDistance; y++)
            {
                int chunkX = Dynamic.Player.chunkPosition.x + x;
                int chunkY = Dynamic.Player.chunkPosition.y + y;

                if (!_loadedChunks.Contains(new Vector2Int(chunkX, chunkY)))
                {
                    SetChunk(chunkX, chunkY, true);
                    _loadedChunks.Add(new Vector2Int(chunkX, chunkY));
                }
            }
        }
    }

    private void ClearChunksOutOfRange()
    {
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

    private void ClearAllChunks()
    {
        for (int i = 0; i < _loadedChunks.Count; i++)
        {
            SetChunk(_loadedChunks[i].x, _loadedChunks[i].y, false);
        }

        _loadedChunks = new List<Vector2Int>();
    }

    private void SetChunk(int x, int y, bool setMode)
    {
        // Sets each tile in a given chunk based on the map data
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                Vector3Int position = new Vector3Int(x * 16 + i, y * 16 + j, 0);
                if (setMode)
                {
                    TileBase tile = _currentMap.GetTile(position.x, position.y);
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
