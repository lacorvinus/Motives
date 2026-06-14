using UnityEngine;

/*
NAME:
TestActor

PURPOSE:
Minimal gameplay simulation object used to validate Rook runtime behavior.

OWNS:
- Simple movement logic
- Response to GameState

DOES NOT OWN:
- Input interpretation beyond raw keys
- Game flow
- Scene control
*/

public class TestActor : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private void Update()
    {
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing)
            return;

        if (GameFlow.Instance == null || !GameFlow.Instance.IsReady)
            return;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(x, y, 0f).normalized;

        transform.position += move * speed * Time.deltaTime;
    }
}