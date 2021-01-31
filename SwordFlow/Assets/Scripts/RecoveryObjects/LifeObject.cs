using UnityEngine;

public class LifeObject : MonoBehaviour
{
    private float timer = 0.5f;

    private void Update()
    {
        timer -= Time.deltaTime;
        var position = gameObject.transform.position;
        if (timer >= 0)
        {
            gameObject.transform.position = new Vector3(position.x, position.y + 0.007f, position.z);
        }
        else if (timer >= -0.5)
        {
            gameObject.transform.position = new Vector3(position.x, position.y - 0.007f, position.z);
        }
        else
        {
            timer = 0.5f;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponentInParent<PlayerSingleton>();
        if (player != null && player.gameObject.name == "Player")
        {
            player.GetComponent<HealthController>().AddHealth(6);
            Destroy(gameObject);
        }
    }
}
