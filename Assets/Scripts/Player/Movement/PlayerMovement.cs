using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class PlayerMovement : PlayerInput
{
    private Rigidbody playerRb;

    public float forwardAccel = 1, maxSpeed = 300, turnsStrength = 100, mulitipier = 1000, gravityForce = 10f, dragValue = .3f;

    private bool grounded;

    public LayerMask whatIsGround;
    public float groundRayLength = .5f;
    public Transform groundRayPoint;
    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (grounded)
            TurnPlayer(TurnAngle());
    }
    protected void TurnPlayer(float Angle)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles
    + (new Vector3(0, Angle * turnsStrength * Time.fixedDeltaTime, 0) / 2));
    }
    void FixedUpdate()
    {
        grounded = false;
        RaycastHit hit;

        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            grounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if (grounded)
        {
            playerRb.drag = dragValue;

            playerRb.AddRelativeForce(Vector3.forward * forwardAccel * mulitipier * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
        else
        {
            playerRb.drag = (dragValue / 3);

            playerRb.AddRelativeForce(Vector3.up * -gravityForce * mulitipier * Time.fixedDeltaTime, ForceMode.Force);
        }
    }
}
