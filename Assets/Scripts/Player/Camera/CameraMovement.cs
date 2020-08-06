using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    #region Variables
    [BoxGroup("Camera Follow")]
    [GUIColor(.7f, .3f, .35f)]
    [SceneObjectsOnly]
    public Transform target;

   
    [BoxGroup("Camera Follow")]
    [GUIColor(.7f, .3f, .35f)]
    [Range(.1f, 3)]
    public float smoothSpeed = .125f;

    [BoxGroup("Camera Follow")]
    [GUIColor(.7f, .3f, .35f)]
    public Vector3 offset;
    #endregion

    void Update()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smootherPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smootherPosition;

       transform.LookAt(target);
    }
}
