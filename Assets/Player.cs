using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed = 15;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            var increment = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
            var newPosition = transform.position + Vector3.up * increment;
            transform.position = newPosition;
        }
    }
}