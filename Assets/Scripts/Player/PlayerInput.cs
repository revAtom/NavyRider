using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    protected int turnAngle = 1;

    protected float TurnAngle()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A))
        {
            turnAngle = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turnAngle = 1;
        }
        else
        {
            turnAngle = 0;
        }
        return turnAngle;
    }
#endif
}