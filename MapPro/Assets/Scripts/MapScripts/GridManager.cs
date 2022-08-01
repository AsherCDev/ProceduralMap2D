using System;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    public GameObject groundGrid;

    private GridController groundController;

    private void Start()
    {
        groundController = new GridController(groundGrid);

        EventManager.instance.onChunkUpdate += groundController.UpdateChunks;
        EventManager.instance.onMapUpdate += groundController.UpdateMap;
    }
}
