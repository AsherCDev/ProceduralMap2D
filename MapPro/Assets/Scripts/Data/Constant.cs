
using UnityEngine;

public static class Constant
{
    public static class Player
    {
        // Time it takes to complete any rotation
        public const float rotationTime = 0.25f;
    }

    public static class Map
    {
        public static Vector2 defaultSeed = new Vector2(10000, 10000);
        public const float perlinRange = 10;
        public static int renderDistance = 3;
    }
}
