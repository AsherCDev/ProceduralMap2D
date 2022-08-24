
using System;
using System.Collections.Generic;
using UnityEngine;

public static class Dynamic
{
    public static class Player
    {
        public static float speed;
        public static Vector2Int chunkPosition;
        public static Transform playerTransform;
        
        public static void UpdatePosition(Vector2 position)
        {
            // Calculates the players current chunk position
            Vector2Int newChunkPosition = new Vector2Int((int)position.x / 16, (int)position.y / 16);
            if (position.x < 0) newChunkPosition.x -= 1;
            if (position.y < 0) newChunkPosition.y -= 1;

            // Calls updateChunks event when the player enters a new chunk
            if (chunkPosition != newChunkPosition)
            {
                chunkPosition = newChunkPosition;
                EventManager.instance.UpdateChunks();
            }

        }
        
    }

    public static class MapData
    {
        public static LandMap landMap = new LandMap();

        public static String seed;
        
        public static float waterLevel;
        public static float scale;

        public static float biomeScale;

        public static float sharpness;
    }
}
