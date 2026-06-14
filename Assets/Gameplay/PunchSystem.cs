using UnityEngine;

public class PunchSystem : MonoBehaviour
{
    [SerializeField] private float punchRange = 1f;
    [SerializeField] private LayerMask wallLayer;

    private PlayerController player;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.CurrentState != GameManager.GameState.Playing)
            return;

        if (!Input.GetKeyDown(KeyCode.Space))
            return;

        TryPunch();
    }

    private void TryPunch()
    {
        Debug.Log("PUNCH: attempted");
        Vector2 origin = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            player.FacingDirection,
            punchRange,
            wallLayer
        );

        Debug.Log("PUNCH direction: " + player.FacingDirection);

        if (hit.collider == null)
            return;

        DestructibleWall wall = hit.collider.GetComponent<DestructibleWall>();

        if (wall != null)
        {
            wall.DestroyWall();
        }

        if (hit.collider == null)
        {
            Debug.Log("PUNCH: no hit");
            return;
        }
        Debug.Log("PUNCH hit: " + hit.collider.name);
    }
}