using UnityEngine;

public class WallCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        GameManager._instance.WallHit(other);
    }
}
