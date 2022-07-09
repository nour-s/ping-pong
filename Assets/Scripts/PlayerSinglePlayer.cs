using System;
using UnityEngine;

public class PlayerSinglePlayer : MonoBehaviour
{
    public int speed = 15;

    public bool isComputer = false;

    public bool isPlayer1 = true;

    // Update is called once per frame
    void Update()
    {
        if (isComputer)
        {
            return;
        }

        var axis = isPlayer1 ? "Vertical" : "Vertical2";

        if (Input.GetAxis(axis) != 0)
        {
            var value = Input.GetAxisRaw(axis) * speed * Time.deltaTime;
            transform.position += (Vector3.up * value);
        }
    }
}