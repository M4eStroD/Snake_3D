using UnityEngine;

public class RandomPointOnMeshCollider : MonoBehaviour
{
    [SerializeField] private int _startCountFood;
    [SerializeField] private GameObject _prefabFood;

    [SerializeField] private MeshCollider _meshCollider;

    private void Start()
    {
        EventManager.OnFoodEat.AddListener(SpawnFood);

        for (int i = 0; i < _startCountFood; i++)
            SpawnFood();
    }

    public void SpawnFood()
    {
        Instantiate(_prefabFood, RandomPosition(), Quaternion.identity, transform);
    }

    private Vector3 RandomPosition()
    {
        int[] triangles = _meshCollider.sharedMesh.triangles;

        int triangleCount = triangles.Length / 3;
        int randomTriangleIndex = Random.Range(0, triangleCount);

        Vector3[] vertices = _meshCollider.sharedMesh.vertices;

        int vertexIndex1 = triangles[randomTriangleIndex * 3];
        int vertexIndex2 = triangles[randomTriangleIndex * 3 + 1];
        int vertexIndex3 = triangles[randomTriangleIndex * 3 + 2];

        Vector3 vertex1 = _meshCollider.transform.TransformPoint(vertices[vertexIndex1]);
        Vector3 vertex2 = _meshCollider.transform.TransformPoint(vertices[vertexIndex2]);
        Vector3 vertex3 = _meshCollider.transform.TransformPoint(vertices[vertexIndex3]);

        Vector3 barycentricCoordinates = GetRandomBarycentricCoordinates();

        Vector3 randomPointOnTriangle = vertex1 * barycentricCoordinates.x +
                                         vertex2 * barycentricCoordinates.y +
                                         vertex3 * barycentricCoordinates.z;

        return randomPointOnTriangle;
    }

    private Vector3 GetRandomBarycentricCoordinates()
    {
        float randomCoordinate1 = Random.Range(0f, 1f);
        float randomCoordinate2 = Random.Range(0f, 1f);
        float sum = randomCoordinate1 + randomCoordinate2;

        randomCoordinate1 /= sum;
        randomCoordinate2 /= sum;
        float randomCoordinate3 = 1 - randomCoordinate1 - randomCoordinate2;

        return new Vector3(randomCoordinate1, randomCoordinate2, randomCoordinate3);
    }
}