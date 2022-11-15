using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public int speed = 15;

    private int _player1Score = 0;

    private int _player2Score = 0;
    private GameObject ball;

    public TMP_Text _scoreText;

    public GameObject _playerPrefab;
    public GameObject _ballPrefab;
    public AudioSource audioSource;
    public AudioClip[] hitSounds;
    public AudioClip[] loseSounds;
    public LineRenderer lineRenderer;

    public Vector3[] positions;

    void Start()
    {
        _instance = this;
        ball = Instantiate(_ballPrefab, Vector3.zero, Quaternion.identity);
        StartCoroutine(SetBall());
        PlacePlayers();
    }

    private void PlacePlayers()
    {
        var player1 = Instantiate(_playerPrefab, positions[0], Quaternion.identity);
        var player2 = Instantiate(_playerPrefab, positions[1], Quaternion.identity);
        player2.GetComponent<PlayerSinglePlayer>().isPlayer1 = false;
        // player2.GetComponent<PlayerSinglePlayer>().isComputer = true;

    }

    Vector2 ballPrevPos;

    void Update()
    {
        // DrawPathOfBall();

    }

    private void DrawPathOfBall()
    {
        Vector2 curPos = ball.transform.position;
        var rb = ball.GetComponent<Rigidbody2D>();
        var vel = rb.velocity;

        Debug.DrawRay(ballPrevPos.normalized * 10, curPos.normalized, Color.red);
        Debug.DrawRay(curPos, vel.normalized * 5, Color.red);
        lineRenderer.SetPosition(0, curPos);
        lineRenderer.SetPosition(1, curPos + vel.normalized * 5);
        lineRenderer.material.mainTextureScale = new Vector2(1f / lineRenderer.startWidth, 1.0f);

        Vector2 newPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.material.SetTextureOffset("_MainTex", new Vector2(Time.timeSinceLevelLoad * 4f, 0f));
        lineRenderer.material.SetTextureScale("_MainTex", new Vector2(curPos.magnitude, 1f));
        lineRenderer.SetPosition(0, newPosition);

        float dot = Vector2.Dot(curPos.normalized, curPos.normalized * 5);
        bool sameDirection = dot >= 0;
        Debug.LogFormat("dot: {0} ", dot);

        if (!sameDirection)
        {
            ballPrevPos = curPos;
        }
    }

    internal void GoalHit(bool isPlayer1)
    {
        _ = isPlayer1 ? _player1Score++ : _player2Score++;

        audioSource.PlayOneShot(loseSounds[Random.Range(0, loseSounds.Length)]);
        ball.SetActive(false);

        UpdateText();
        StartCoroutine(SetBall());
    }

    private void UpdateText()
    {
        _scoreText.text = _player1Score + " - " + _player2Score;
    }

    private IEnumerator SetBall()
    {
        yield return new WaitForSeconds(3f);
        ball.SetActive(true);
        ball.transform.position = Vector2.zero;
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
        ball = GameObject.Instantiate(_ballPrefab, Vector3.zero, Quaternion.identity);
        SetBall();
    }

    internal void WallHit(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            audioSource.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
        }
    }
}
