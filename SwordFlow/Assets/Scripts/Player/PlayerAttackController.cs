using UnityEngine;

public class PlayerAttackController: MonoBehaviour
{
    public float startTimeBtwAttack;
    public Transform attackPosition;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public float damage = 1f;
    public Rigidbody2D rb;
    public GameScore score;

    private float timeBtwAttack;
    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<HealthController>().TakeDamage(damage);
                    score.AddScore((int)damage);
                }
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
