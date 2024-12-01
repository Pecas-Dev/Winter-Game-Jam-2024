using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [Header("Target to Follow")]
    [SerializeField] Transform targetTransform; 

    void LateUpdate()
    {
        if (targetTransform != null)
        {
            transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y, transform.position.z);
        }
    }
}
