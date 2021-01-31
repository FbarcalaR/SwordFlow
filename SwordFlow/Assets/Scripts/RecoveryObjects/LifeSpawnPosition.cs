using UnityEngine;

public class LifeSpawnPosition : MonoBehaviour
{
    public GameObject lifeObject;
    void Start()
    {
        if (Random.Range(0f, 1f) < 0.33f)
        {
            var newEndPoint = Instantiate(lifeObject, transform.position, Quaternion.identity);
            newEndPoint.transform.parent = gameObject.transform;
        }
    }
}
