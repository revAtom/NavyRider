using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    protected int turnAngle = 1;

    public void Update()
    {
        PlayerTurns();
    }

    void PlayerTurns()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            turnAngle = -turnAngle;
        }
    }
}