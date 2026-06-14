using System;
using UnityEngine;

/*
NAME:
GameManager

PURPOSE:
Controls game state and overall game flow.

OWNS:
- Game state
- State transitions
- High-level game flow control

DOES NOT OWN:
- Gameplay systems
- Player logic
- UI logic
- Input handling
*/

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum SceneType { Bootstrap, Menu, Game }
    public enum GameState { Boot, Menu, Playing, Paused }

    public GameState CurrentState { get; private set; }
    public SceneType CurrentScene { get; private set; }

    public bool IsTransitioning { get; private set; }

    public event Action<GameState> OnStateChanged;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SetState(GameState.Boot);
    }

    public void SetState(GameState state)
    {
        if (CurrentState == state)
            return;

        CurrentState = state;

        Debug.Log($"ROOK: State -> {state}");

        OnStateChanged?.Invoke(state);
    }

    public void SetTransitioning(bool value)
    {
        IsTransitioning = value;
    }

    public void SetScene(SceneType scene)
    {
        CurrentScene = scene;
        Debug.Log($"ROOK: Scene -> {scene}");
    }
}