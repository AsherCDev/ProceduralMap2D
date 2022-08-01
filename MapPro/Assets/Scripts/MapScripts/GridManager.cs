using System;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    public GameObject groundGrid;

    private LandMap _landMap;
    private GridController groundController;

    private void Start()
    {
        _landMap = new LandMap();
        groundController = new GridController(_landMap, groundGrid);
    }
}
