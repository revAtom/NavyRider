using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class PlayerMovement : PlayerInput
{
    #region Variables
    private Rigidbody playerRb;

    [TabGroup("Movement")]
    [GUIColor(.3f, 1, .6f)]
    [Range(0, 3000)]
    public int forwardAccel = 1, maxSpeed = 300, turnsStrength = 100, mulitipier = 1200;

    [TabGroup("Physics")]
    [GUIColor(1.6f, .4f, .6f)]
    [Range(.1f, 20)]
    public float gravityForce = 10f, dragValue = .3f;

    [TabGroup("Gravity")]
    [GUIColor(.75f, .1f, .6f)]
    [ShowInInspector]
    private bool grounded;

    [TabGroup("Gravity")]
    [GUIColor(.75f, .1f, .6f)]
    public LayerMask whatIsGround;

    [GUIColor(.75f, .1f, .6f)]
    [TabGroup("Gravity")]
    [Range(.1f, 3)]
    public float groundRayLength = .5f;

    [TabGroup("Gravity")]
    [GUIColor(.75f, .1f, .6f)]
    [SceneObjectsOnly]
    public Transform groundRayPoint;

    [TabGroup("Visualise")]
    [GUIColor(.8f, .3f, .3f)]
    [SceneObjectsOnly]
    public Animator playerVisualiseAnim;

    [TabGroup("Visualise")]
    [GUIColor(.8f, .3f, .3f)]
    [SceneObjectsOnly]
    public Camera playersCamera;

    [TabGroup("Modifiers")]
    [GUIColor(.74f, .75f, .6f)]
    [Range(500, 1500)]
    public int multiplierModifierLow = 500, multiplierModifierMedium = 1200;

    [TabGroup("Modifiers")]
    [GUIColor(.74f, .75f, .6f)]
    [ShowInInspector]
    private float cameraFiealOfViewNear = 45, cameraFiealOfViewFar = 50;
    #endregion
    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (grounded)
            TurnPlayer(TurnAngle());

        if (TurnAngle() != 0)
        {
            mulitipier = multiplierModifierLow;

            playerVisualiseAnim.SetBool("IsTurn", true);
        }
        else if (TurnAngle() == 0)
        {
            mulitipier = multiplierModifierMedium;

            playerVisualiseAnim.SetBool("IsTurn", false);
        }
    }
    #region Movement
    protected void TurnPlayer(float Angle)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles
    + (new Vector3(0, Angle * turnsStrength * Time.deltaTime, 0) / 1.5f));

        playerVisualiseAnim.SetFloat("angleOfTurn", Angle);
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
            playersCamera.fieldOfView = cameraFiealOfViewFar;

            playerRb.drag = dragValue;
        }
        else
        {
            playersCamera.fieldOfView = cameraFiealOfViewNear;

            playerRb.drag = (dragValue / 3);

            playerRb.AddRelativeForce(Vector3.up * -gravityForce * 1000 * Time.fixedDeltaTime, ForceMode.Force);
        }

        playerRb.AddRelativeForce(Vector3.forward * forwardAccel * mulitipier * Time.fixedDeltaTime, ForceMode.Force);
    }
    #endregion
}
