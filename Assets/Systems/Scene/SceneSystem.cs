using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ROOK SCENE SYSTEM
///
/// Responsibility:
/// - Load and switch Unity scenes
///
/// Does NOT:
/// - Manage game state (GameManager owns this)
/// - Store data
/// - Control gameplay
/// - Handle UI
/// </summary>

public class SceneSystem : MonoBehaviour
{
    public static SceneSystem Instance { get; private set; }

    private GameManager.SceneType pendingScene;
    private bool isLoading;

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

    public void LoadMenu()
    {
        Load("MenuScene", GameManager.SceneType.Menu);
    }

    public void LoadGame()
    {
        Load("GameScene", GameManager.SceneType.Game);
    }

    private void Load(string sceneName, GameManager.SceneType type)
    {
        if (isLoading) return;

        isLoading = true;

        GameManager.Instance.SetTransitioning(true);

        pendingScene = type;

        SceneManager.sceneLoaded -= OnLoaded;
        SceneManager.sceneLoaded += OnLoaded;

        Debug.Log($"ROOK: Loading -> {type}");

        SceneManager.LoadScene(sceneName);
    }

    private void OnLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnLoaded;

        isLoading = false;

        Debug.Log($"ROOK: Scene Loaded -> {pendingScene}");

        GameManager.Instance.SetScene(pendingScene);

        GameFlow.Instance.OnSceneReady(pendingScene);
    }
}