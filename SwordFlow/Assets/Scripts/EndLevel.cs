using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "model")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            var player = collision.GetComponentInParent<PlayerSingleton>().gameObject;
            player.transform.position = Vector3.zero;
            collision.transform.position = Vector3.zero;
        }
    }
}
