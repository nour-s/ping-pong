using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public int speed = 15;

    private int _player1Score = 0;

    private int _player2Score = 0;

    public TMP_Text _scoreText;

    public GameObject _ball;
    public AudioSource audioSource;
    public AudioClip[] hitSounds;
    public AudioClip[] loseSounds;
    public LineRenderer lineRenderer;


    void Start()
    {
        _instance = this;
        SetBall();
    }

    Vector2 ballPrevPos;

    void Update()
    {
        Vector2 curPos = _ball.transform.position;
        var rb = _ball.GetComponent<Rigidbody2D>();
        var vel = rb.velocity;

        // Debug.DrawRay(ballPrevPos.normalized * 10, curPos.normalized, Color.red);
        // Debug.DrawRay(curPos, vel.normalized * 5, Color.red);
        lineRenderer.SetPosition(0, curPos);
        lineRenderer.SetPosition(1, curPos + vel.normalized * 5);
        lineRenderer.material.mainTextureScale = new Vector2(1f / lineRenderer.startWidth, 1.0f);

        // Vector2 newPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.material.SetTextureOffset("_MainTex", new Vector2(Time.timeSinceLevelLoad * 4f, 0f));
        lineRenderer.material.SetTextureScale("_MainTex", new Vector2(curPos.magnitude, 1f));
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
        _ball.transform.position = Vector2.zero;
        var direction =
            new Vector2[] { Vector2.down, Vector2.up }[Random.Range(0, 2)] +
            new Vector2[] { Vector2.right, Vector2.left }[Random.Range(0, 2)];
        var rb = _ball.GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * speed;
        ballPrevPos = _ball.transform.position;
    }

    public void PaddleHit(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            audioSource.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
        }
    }
}
