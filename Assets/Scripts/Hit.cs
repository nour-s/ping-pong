using Unity.Netcode;
using UnityEngine;

public class Hit : NetworkBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((bool)!NetworkManager?.IsServer) return;

        GameManager._instance.PaddleHit(other);
    }
}
