using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action onChunkUpdate;

    public void UpdateChunks()
    {
        if (onChunkUpdate != null)
        {
            onChunkUpdate();
        }
    }

    public event Action onMapUpdate;

    public void UpdateMap()
    {
        if (onMapUpdate != null)
        {
            onMapUpdate();
        }
    }
}