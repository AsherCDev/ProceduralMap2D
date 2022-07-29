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
}