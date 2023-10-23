using UnityEngine;

public class ObjectCamFollow : MonoBehaviour
{
    [SerializeField] private Transform _objectToFollow;

    private Vector3 _position;

    private void Start()
    {
        _position = transform.position - _objectToFollow.position;
    }

    private void LateUpdate()
    {
        transform.position = _objectToFollow.position + _position;
        transform.rotation = _objectToFollow.rotation;
    }
}
