using UnityEngine;

/*
ROOK BOOTSTRAP

Purpose:
Initializes the game and hands control to GameManager.

Owns:
- Startup sequence only

Does Not Own:
- Scene flow
- Game state decisions
- Gameplay logic
- UI logic
*/

public class Bootstrap : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("ROOK: Bootstrap started.");

        GameFlow.Instance.EnterMenu();
    }
}