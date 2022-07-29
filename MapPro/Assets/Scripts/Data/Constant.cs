
using UnityEngine;

public static class Constant
{
    public static class Player
    {
        public const float moveSpeed = 4;
        // Time it takes to complete any rotation
        public const float rotationTime = 0.25f;
    }

    public static class Map
    {
        public static Vector2 seed = new Vector2(10000, 10000);
        public static int renderDistance = 1;
    }
}