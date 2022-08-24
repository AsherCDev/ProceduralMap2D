using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GridManager : MonoBehaviour
{
    public Tilemap groundGrid;
    public Tilemap featureGrid;

    private GroundController groundController;
    private LandFeatureController featureController;

    private List<Vector2Int> _loadedChunks = new List<Vector2Int>();

    private void Start()
    {
        groundController = new GroundController(groundGrid);
        featureController = new LandFeatureController(featureGrid);
        EventManager.instance.onChunkUpdate += UpdateChunks;
        EventManager.instance.onMapUpdate += UpdateMaps;
    }

    private void UpdateChunks()
    {
        LoadChunksInRange();
        ClearChunksOutOfRange();
    }

    private void UpdateMaps()
    {
        ClearAllChunks();
        Dynamic.MapData.landMap.UpdateMap();
        UpdateChunks();
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
                    groundController.SetChunk(chunkX, chunkY, true);
                    featureController.SetChunk(chunkX, chunkY, true);

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
                groundController.SetChunk(_loadedChunks[i].x, _loadedChunks[i].y, false);
                featureController.SetChunk(_loadedChunks[i].x, _loadedChunks[i].y, false);
                _loadedChunks.Remove(_loadedChunks[i]);
            }
        }
    }
    
    private void ClearAllChunks()
    {
        for (int i = 0; i < _loadedChunks.Count; i++)
        {
            groundController.SetChunk(_loadedChunks[i].x, _loadedChunks[i].y, false);
            featureController.SetChunk(_loadedChunks[i].x, _loadedChunks[i].y, false);
        }

        _loadedChunks = new List<Vector2Int>();
    }
    
    
}
