using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private float attractionSpeed = 35f;

    private GameObject targetObject;

    private void Update()
    {
        if (targetObject)
        {
            float distance = Vector3.Distance(transform.position, targetObject.transform.position);

            if (distance <= 1)
            {
                targetObject.GetComponent<SnakeTail>().AddBody();
                EventManager.SendFoodEat();
                Destroy(gameObject);
            }

            Vector3 attractionDirection = (targetObject.transform.position - transform.position).normalized;
            float attractionForce = attractionSpeed * Time.deltaTime;

            transform.Translate(attractionDirection * attractionForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SnakeHead"))
            targetObject = other.gameObject;
    }
}
