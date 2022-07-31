
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapController : MonoBehaviour
{
    private Tilemap _tileMap;
    private LandMap _landMap;

    private List<Vector2Int> _loadedChunks = new List<Vector2Int>();

    private bool _reloading;

    private void Start()
    {
        _tileMap = GetComponent<Tilemap>();
        EventManager.instance.onChunkUpdate += UpdateChunks;
        _landMap = new LandMap();
        EventManager.instance.onMapUpdate += _landMap.UpdateMap;
        EventManager.instance.onMapUpdate += ApplyMapUpdate;
    }

    private void ApplyMapUpdate()
    {
        _reloading = true;
        UpdateChunks();
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
                if (!_loadedChunks.Contains(new Vector2Int(chunkX, chunkY)) || _reloading)
                {
                    SetChunk(chunkX, chunkY, true);
                    _loadedChunks.Add(new Vector2Int(chunkX, chunkY));
                }
            }
        }

        _reloading = false;

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
