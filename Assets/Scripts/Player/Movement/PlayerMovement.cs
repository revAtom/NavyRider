using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class PlayerMovement : PlayerInput
{
    private Rigidbody playerRb;

    public float forwardAccel = 1, maxSpeed, turnsStrength = 20, mulitipier = 1000;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles
            + new Vector3(0, turnAngle * turnsStrength * Time.fixedDeltaTime, 0));



        playerRb.AddForce(Vector3.left * forwardAccel * mulitipier * Time.fixedDeltaTime, ForceMode.Acceleration);
    }
}
