using UnityEngine;

public class PlayerOrientationFromCamera : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;

    void LateUpdate()
    {
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;

        if (forward.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(forward);
        }
    }
}