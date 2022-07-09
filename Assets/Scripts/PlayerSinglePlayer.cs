using System;
using UnityEngine;

public class PlayerSinglePlayer : MonoBehaviour
{
    public int speed = 15;

    public bool isComputer = false;

    // Update is called once per frame
    void Update()
    {
        if (isComputer)
        {
            return;
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            var value = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
            transform.position += (Vector3.up * value);
        }
    }
}