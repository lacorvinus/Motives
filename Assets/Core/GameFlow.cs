using UnityEngine;

/*
NAME:
GameFlow

PURPOSE:
Coordinates scene transitions and ensures safe handoff between
scene loading and game state activation.

Acts as the bridge between:
- SceneSystem (scene loading / Unity lifecycle)
- GameManager (game state)
- Input/UI systems (user intent)

OWNS:
- Transition flow control (when gameplay is allowed to start)
- Scene-to-state handoff timing
- Global transition lock state (prevents re-entrant scene loads)

DOES NOT OWN:
- Scene loading (SceneSystem owns this)
- Game rules or state definitions (GameManager owns this)
- Input handling (InputSystem owns this)
- UI logic (UISystem owns this)

NOTES:
GameFlow exists to eliminate circular dependencies between
scene loading and game state transitions.

It is the single authority for:
"Scene is ready → Gameplay is allowed to begin"
*/

public class GameFlow : MonoBehaviour
{
    public static GameFlow Instance { get; private set; }

    public bool IsReady { get; private set; }

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

    public void EnterMenu()
    {
        IsReady = false;

        GameManager.Instance.SetTransitioning(true);
        SceneSystem.Instance.LoadMenu();
    }

    public void StartGame()
    {
        IsReady = false;

        GameManager.Instance.SetTransitioning(true);
        SceneSystem.Instance.LoadGame();
    }

    public void OnSceneReady(GameManager.SceneType scene)
    {
        Debug.Log("ROOK: Scene READY");

        GameManager.Instance.SetTransitioning(false);
        GameManager.Instance.SetScene(scene);

        if (scene == GameManager.SceneType.Menu)
        {
            GameManager.Instance.SetState(GameManager.GameState.Menu);
        }

        if (scene == GameManager.SceneType.Game)
        {
            GameManager.Instance.SetState(GameManager.GameState.Playing);
            IsReady = true;
        }
    }
}