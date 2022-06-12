using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    public Button btn_start_host;
    public Button btn_start_client;


    // Start is called before the first frame update
    void Start()
    {
        btn_start_host.onClick.AddListener(StartHost);
        btn_start_client.onClick.AddListener(StartClient);
    }

    private void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }

    private void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }
}
