using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    [SerializeField]
    private float _smoothSpeed = 0.125f;

    private void FixedUpdate()
    {
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, new Vector3(_player.position.x, _player.position.y, transform.position.z),  _smoothSpeed);
        transform.position = smoothedPosition;  
    }
}
