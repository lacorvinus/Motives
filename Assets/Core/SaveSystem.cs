using System.Collections.Generic;
using UnityEngine;

/*
NAME:
SaveSystem

PURPOSE:
Coordinates save and load operations.

OWNS:
- Save orchestration

DOES NOT OWN:
- Gameplay state
- Saveable registration
*/

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance { get; private set; }

    private Dictionary<string, object> saveData =
        new Dictionary<string, object>();

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

    public void Save()
    {
        saveData.Clear();

        foreach (var saveable in SaveRegistry.Instance.GetSaveables())
        {
            saveData[saveable.SaveID] =
                saveable.CaptureState();
        }

        Debug.Log("ROOK: Save Complete");
    }

    public void Load()
    {
        foreach (var saveable in SaveRegistry.Instance.GetSaveables())
        {
            if (saveData.TryGetValue(
                saveable.SaveID,
                out var state))
            {
                saveable.RestoreState(state);
            }
        }

        Debug.Log("ROOK: Load Complete");
    }
}