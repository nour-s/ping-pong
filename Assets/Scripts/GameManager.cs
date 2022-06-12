using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public static GameManager _instance;
    public int speed = 15;

    private int _player1Score = 0;

    private int _player2Score = 0;
    private GameObject ball;

    public TMP_Text _scoreText;

    public GameObject _ballPrefab;
    public AudioSource audioSource;
    public AudioClip[] hitSounds;
    public AudioClip[] loseSounds;
    public LineRenderer lineRenderer;

    public Vector3[] positions;

    // public NetworkManager networkManager;

    public bool isPlayer1Connected;

    private bool serverStarted;
    private bool clientConnected;


    void Start()
    {
        _instance = this;

        NetworkManager.OnServerStarted += OnServerStarted;
        NetworkManager.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.ConnectionApprovalCallback += ApproveConnection;
    }

    private void OnClientConnected(ulong obj)
    {
        clientConnected = true;
    }

    private void OnServerStarted()
    {
        serverStarted = true;
    }

    private void ApproveConnection(byte[] payload, ulong clientId, NetworkManager.ConnectionApprovedDelegate callback)
    {
        Debug.Log("Client connected: " + clientId);
        var position = isPlayer1Connected ? positions[1] : positions[0];
        isPlayer1Connected = true;

        callback(true, null, true, position, Quaternion.identity);
    }

    Vector2 ballPrevPos;

    void Update()
    {
        // if (!NetworkManager.Singleton.IsServer) return;

        // lineRenderer.SetPosition(0, newPosition);

        // float dot = Vector2.Dot(curPos.normalized, curPos.normalized * 5);
        // bool sameDirection = dot >= 0;
        // Debug.LogFormat("dot: {0} ", dot);

        // if (!sameDirection)
        // {
        //     ballPrevPos = curPos;
        // }
    }

    internal void GoalHit(bool isPlayer1)
    {
        _ = isPlayer1 ? _player1Score++ : _player2Score++;

        audioSource.PlayOneShot(loseSounds[Random.Range(0, loseSounds.Length)]);

        UpdateText();
        SetBall();
    }

    private void UpdateText()
    {
        _scoreText.text = _player1Score + " - " + _player2Score;
    }

    private void SetBall()
    {
        if (!NetworkManager.IsServer) return;

        var direction =
            new Vector2[] { Vector2.down, Vector2.up }[Random.Range(0, 2)] +
            new Vector2[] { Vector2.right, Vector2.left }[Random.Range(0, 2)];
        var rb = ball.GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * speed;
        ballPrevPos = ball.transform.position;
    }

    public void PaddleHit(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            audioSource.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
        }
    }

    public void StartGame()
    {
        if (!serverStarted)
        {
            Debug.LogError("No server");
            return;
        }

        if (!clientConnected)
        {
            Debug.LogError("No client");
            return;
        }

        ball = GameObject.Instantiate(_ballPrefab, Vector3.zero, Quaternion.identity);
        SetBall();
        ball.GetComponent<NetworkObject>().Spawn();
    }
}
