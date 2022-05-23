using UnityEngine;

public class Hit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        GameManager._instance.PaddleHit(other);
    }
}
