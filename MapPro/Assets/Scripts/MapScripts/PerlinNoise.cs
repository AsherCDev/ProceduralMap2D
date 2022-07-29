
using UnityEngine;

public class PerlinNoise
{
    private float _scale, _range;
    private Vector2 _seed;

    public PerlinNoise(float scale, float range, Vector2 seed)
    {
        _scale = scale;
        _range = range;
        _seed = seed;
    }

    public float GetValue(int x, int y)
    {
        float px = (_seed.x + x) / _scale;
        float py = (_seed.y + y) / _scale;
        return Mathf.PerlinNoise(px + 0.1f, py + -0.1f) * _range;
    }
}
