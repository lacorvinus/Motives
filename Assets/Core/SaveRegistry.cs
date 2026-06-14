using System.Collections.Generic;
using UnityEngine;

/*
NAME:
SaveRegistry

PURPOSE:
Tracks saveable systems.

OWNS:
- Registration of save participants

DOES NOT OWN:
- Save files
- Serialization
- Game logic
*/

public class SaveRegistry : MonoBehaviour
{
    public static SaveRegistry Instance { get; private set; }

    private readonly List<ISaveable> saveables = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Register(ISaveable saveable)
    {
        if (!saveables.Contains(saveable))
        {
            saveables.Add(saveable);
        }
    }

    public void Unregister(ISaveable saveable)
    {
        saveables.Remove(saveable);
    }

    public IReadOnlyList<ISaveable> GetSaveables()
    {
        return saveables;
    }
}