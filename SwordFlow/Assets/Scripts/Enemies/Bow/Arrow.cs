using SwordFlowScripts;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float Speed = 20f;
    public int damage = 2;
    public Rigidbody2D rb;
    public GameObject ImpactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthController healthComponent = collision.GetComponent<HealthController>();
        if(healthComponent != null)
        {
            healthComponent.TakeDamage(damage);
            if (ImpactEffect != null)  Instantiate(ImpactEffect, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
