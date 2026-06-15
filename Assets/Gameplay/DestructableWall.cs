using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    public void DestroyWall()
    {
        ObjectiveSystem.Instance?.RegisterWallDestroyed();

        Destroy(gameObject);
    }
}