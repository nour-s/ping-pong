using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerMultiplayer : NetworkBehaviour
{
    public int speed = 15;

    private NetworkVariable<float> x = new NetworkVariable<float>();

    // Update is called once per frame
    void Update()
    {
        // if (IsServer)
        // {
        ServerUpdate();
        // }

        if (IsClient && IsOwner)
            if (Input.GetAxis("Vertical") != 0)
            {
                UpdateClientOnServerRpc(Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
            }
    }

    [ServerRpc]
    void UpdateClientOnServerRpc(float newVal)
    {
        x.Value = newVal;
    }

    void ServerUpdate()
    {
        transform.position += (Vector3.up * x.Value);
    }
}