using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    [SerializeField] private Transform _planet;
    [SerializeField] private Rigidbody _rigidbodyPlayer;
    [SerializeField] private Rigidbody _rigidbodyPlanet;

    private float _time = 1f;

    private void FixedUpdate()
    {
        if (_planet)
        {
            Vector3 toCenter = (_planet.position - transform.position).normalized;

            float distance = (_planet.position - transform.position).magnitude;
            float force = _rigidbodyPlayer.mass * _rigidbodyPlanet.mass / distance;

            _rigidbodyPlayer.AddForce(toCenter * force, ForceMode.Acceleration);

            Quaternion rotate = Quaternion.FromToRotation(transform.up, -toCenter);
            rotate = rotate * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, _time);
        }
    }
}