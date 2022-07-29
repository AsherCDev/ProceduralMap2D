
using UnityEngine;

public static class Dynamic
{
    public static class Player
    {
        public static Vector2Int chunkPosition;
        
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
}
