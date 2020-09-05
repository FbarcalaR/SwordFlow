using UnityEngine;

public class PlayerAttackController: MonoBehaviour
{
    public float startTimeBtwAttack;
    public Transform attackPosition;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public float damage = 1f;
    public Rigidbody2D rb;

    private float timeBtwAttack;
    private Vector2 lastPosition;

    private void Start()
    {
        lastPosition = rb.position;
    }
    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    float velocity = (rb.position - lastPosition).magnitude / Time.deltaTime;
                    float calculatedDamage = damage * velocity;
                    enemiesToDamage[i].GetComponent<HealthController>().TakeDamage(calculatedDamage);
                }
            }


            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
        lastPosition = rb.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
