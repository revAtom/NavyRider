using Sirenix.OdinInspector;
using UnityEngine;

public class Floater : MonoBehaviour
{
    #region Variables
    private Rigidbody floatingRb;

    [BoxGroup("Transform")]
    [GUIColor(.6f, .65f, .56f)]
    [SceneObjectsOnly]
    public Transform waterLevel;

    [BoxGroup("Transform")]
    [GUIColor(.6f, .65f, .56f)]
    [SceneObjectsOnly]
    public Transform shipBottomLevel;


    [BoxGroup("Floating Modifier")]
    [GUIColor(.74f, .5f, .74f)]
    [Range(.1f, 3)]
    public float depthBeforeSubmerged = 1f;

    [BoxGroup("Floating Modifier")]
    [GUIColor(.74f, .5f, .74f)]
    [Range(50f, 100)]
    public float displacementAmount = 3f;
    #endregion

    private void Awake()
    {
        floatingRb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (shipBottomLevel.position.y <=  200)
        {
            float displacementMultiplier = Mathf.Clamp01(-transform.position.y / depthBeforeSubmerged) * displacementAmount;

            floatingRb.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier * Time.fixedDeltaTime, 0), ForceMode.Acceleration);
        }
    }
}
