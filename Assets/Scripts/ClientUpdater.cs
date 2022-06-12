using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Netcode;
using UnityEngine;

// Set the client ID on the client window
public class ClientUpdater : NetworkBehaviour
{
    public TMP_Text clientIdText;

    void Start()
    {
        NetworkManager.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnClientConnected(ulong id)
    {
        if (IsOwner)
            clientIdText.text = id.ToString();
        if (IsServer)
            Debug.Log(NetworkManager.ConnectedClientsIds.Select(Convert.ToString).Aggregate((a, b) => $"{a}, {b}"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
