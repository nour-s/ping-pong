using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isPlayer1 = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            GameManager._instance.GoalHit(isPlayer1);
        }
    }
}