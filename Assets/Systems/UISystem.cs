using UnityEngine;
using TMPro;

/*
NAME:
UISystem

PURPOSE:
Displays game state information to the player.

OWNS:
- UI representation of GameState

DOES NOT OWN:
- Game state logic
- Game flow control
- Input handling

NOTES:
This system is a persistent observer of GameManager state.
It does not decide anything, only reflects state.
*/

public class UISystem : MonoBehaviour
{
    public static UISystem Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI gameStateText;

    private GameManager.GameState lastState;

    private void Awake()
    {
        // Singleton guard (prevents duplicates across scene loads)
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Critical: UI persists across scene transitions
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            lastState = GameManager.Instance.CurrentState;
            UpdateUI(lastState);
        }
    }

    private void Update()
    {
        if (GameManager.Instance == null)
            return;

        var currentState = GameManager.Instance.CurrentState;

        // Only update UI when state actually changes (clean + efficient)
        if (currentState == lastState)
            return;

        lastState = currentState;
        UpdateUI(currentState);
    }

    private void UpdateUI(GameManager.GameState state)
    {
        if (gameStateText == null)
            return;

        gameStateText.text = $"State: {state}";
    }
}