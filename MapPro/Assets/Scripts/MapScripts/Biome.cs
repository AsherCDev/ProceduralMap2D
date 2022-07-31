
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "MapSystem/Biome")]
public class Biome : ScriptableObject
{
    public List<TileBase> groundTiles;
    public Vector2 heatRange;
    public Vector2 rainRange;
}