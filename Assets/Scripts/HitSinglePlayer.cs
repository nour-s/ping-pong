using Unity.Netcode;
using UnityEngine;

public class HitSinglePlayer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        GameManager._instance.PaddleHit(other);
    }
}
