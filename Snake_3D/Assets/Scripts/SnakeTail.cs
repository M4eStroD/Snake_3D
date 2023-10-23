using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    [SerializeField] private int _startCountBody = 5;
    [SerializeField] private float _offset = 2;

    [SerializeField] private Transform _container;
    [SerializeField] private Transform _snakeHead;
    [SerializeField] private GameObject _prefabBody;

    private List<Transform> _snakeBody = new List<Transform>();
    private List<Vector3> _postition = new List<Vector3>();
    private List<Quaternion> _rotation = new List<Quaternion>();

    private void Start()
    {
        _postition.Add(_snakeHead.position);
        _rotation.Add(_snakeHead.rotation);

        for (int i = 0; i < _startCountBody; i++) AddBody();
    }

    private void FixedUpdate()
    {
        float distance = (_snakeHead.position - _postition[0]).magnitude;

        if (distance > _offset)
        {
            Vector3 direction = (_snakeHead.position - _postition[0]).normalized;

            _postition.Insert(0, _postition[0] + direction * _offset);
            _postition.RemoveAt(_postition.Count - 1);

            distance -= _offset;
        }

        for (int i = 0; i < _snakeBody.Count; i++)
        {
            _snakeBody[i].position = Vector3.Lerp(_postition[i + 1], _postition[i], distance / _offset);
            if (i == 0) _snakeBody[i].transform.LookAt(_snakeHead);
            else _snakeBody[i].transform.LookAt(_snakeBody[i - 1]);
        }
    }

    public void AddBody()
    {
        Transform body = Instantiate(_prefabBody.transform, _postition[_postition.Count - 1], Quaternion.identity, _container.transform);
        _snakeBody.Add(body);
        _postition.Add(body.position);
        _rotation.Add(body.rotation);
    }




























    //[SerializeField] private int _startTailCount = 5;
    //[SerializeField] private float _offset = 5;

    //[SerializeField] private GameObject _prefabTail;

    //[SerializeField] private Transform _planet;
    //[SerializeField] private Rigidbody _rigidBodyPlanet;

    //private List<GameObject> _tails = new List<GameObject>();
    //private List<Vector3> _position = new List<Vector3>();
    //private List<Vector3> _rotation = new List<Vector3>();

    //private void Start()
    //{
    //    StartCoroutine(AddNewTail(_startTailCount));
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.Space))
    //        StartCoroutine(AddNewTail(1));
    //}

    //private IEnumerator AddNewTail(int count)
    //{
    //    for (int i = 0; i < count; i++)
    //    {
    //        Vector3 newTailPos;
    //        if (_tails.Count == 0)
    //        {
    //            newTailPos = transform.position;
    //            newTailPos.z = _offset - 6;
    //        }
    //        else
    //        {
    //            newTailPos = _tails[_tails.Count - 1].transform.position;
    //            newTailPos.z -= _offset;
    //        }            

    //        yield return new WaitForSeconds(0.1f);

    //        _tails.Add(Instantiate(_prefabTail, newTailPos, Quaternion.identity));

    //        _tails[i].GetComponent<PlanetGravity>().AddComponent(_planet, _rigidBodyPlanet);

    //        if (_tails.Count == 1)
    //            _tails[i].GetComponent<MovementBody>().AddTargetObject(gameObject);
    //        else
    //            _tails[i].GetComponent<MovementBody>().AddTargetObject(_tails[i]);
    //    }
    //}
}
