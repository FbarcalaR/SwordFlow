using UnityEngine;

public class EndSpawnPoint : MonoBehaviour
{
    public GameObject EndPoint;
    public void SpawnLevelEnd()
    {
        var newEndPoint = Instantiate(EndPoint, transform.position, Quaternion.identity);
        newEndPoint.transform.parent = gameObject.transform;
    }
}
