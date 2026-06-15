using UnityEngine;

public class ObjectiveSystem : MonoBehaviour
{
    public static ObjectiveSystem Instance { get; private set; }

    [SerializeField] private int target = 3;

    private int destroyed = 0;
    private bool complete = false;

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterWallDestroyed()
    {
        if (complete) return;

        destroyed++;

        Debug.Log($"MOTIVES: Wall destroyed ({destroyed}/{target})");

        if (destroyed >= target)
        {
            CompleteObjective();
        }
    }

    private void CompleteObjective()
    {
        complete = true;

        Debug.Log("MOTIVES: OBJECTIVE COMPLETE - DESTRUCTION +3");
    }
}