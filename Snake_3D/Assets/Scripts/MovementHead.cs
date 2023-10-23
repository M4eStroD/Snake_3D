using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class MovementHead : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotateSpeed = 10f;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FloatingJoystick _floatingJoystick;

    private Vector2 _rotation = Vector2.zero;

    private void Awake()
    {
        _rigidbody.freezeRotation = true;
        _rigidbody.useGravity = false;
        _rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        _rotation.y = transform.eulerAngles.y;
    }

    private void FixedUpdate()
    {
        if (_floatingJoystick.Horizontal > 0)
            transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);
        else if (_floatingJoystick.Horizontal < 0)
            transform.Rotate(Vector3.down * _rotateSpeed * Time.deltaTime);

        Vector3 direction = new Vector3(0, 0, 1);
        transform.Translate(direction * _speed * Time.deltaTime);

    }
}