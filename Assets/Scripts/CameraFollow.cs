using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector2 offset;
    [Range(1,10)]
    [SerializeField] float smoothFactor = 8f;

    void LateUpdate()
    {
        Vector3 targetPosition = target.position + new Vector3(offset.x, offset.y, -10);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.deltaTime);
        transform.position = smoothPosition;
    }
}
