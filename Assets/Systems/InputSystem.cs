using UnityEngine;

/*
NAME:
InputSystem

PURPOSE:
Reads player input and sends high-level commands to GameManager.

OWNS:
- Player input handling
- Input-to-intent translation

DOES NOT OWN:
- Game rules
- Game state logic
- UI logic

NOTES:
InputSystem is the only script allowed to read Unity input APIs.
*/

public class InputSystem : MonoBehaviour
{
    public static InputSystem Instance { get; private set; }

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

    private void Update()
    {
        if (GameManager.Instance == null)
            return;

        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameFlow.Instance.StartGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.IsTransitioning)
                return;

            if (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
                GameManager.Instance.SetState(GameManager.GameState.Paused);
            else if (GameManager.Instance.CurrentState == GameManager.GameState.Paused)
                GameManager.Instance.SetState(GameManager.GameState.Playing);
        }
    }
}