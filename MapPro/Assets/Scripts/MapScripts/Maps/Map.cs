
using UnityEngine.Tilemaps;

public interface Map
{
    public TileBase GetTile(int x, int y);
    public void UpdateMap();
}
