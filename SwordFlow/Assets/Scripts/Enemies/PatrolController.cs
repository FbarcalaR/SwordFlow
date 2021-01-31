using UnityEngine;

public class PatrolController : MonoBehaviour
{
    public float speed;
    public float waitTime;
    public Transform[] moveSpots;
    public Animator animator;

    public Transform attackPosition;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public float damage = 1f;
    public float timeBtwAttack;

    [HideInInspector]
    public bool attack = false;

    private float startTimeBtwAttack;
    private float startWaitTime;
    private int randomSpot;
    private bool isWaiting;
    private bool facingRight = true;

    void Start()
    {
        startWaitTime = waitTime;
        startTimeBtwAttack = timeBtwAttack;
        randomSpot = 0;
        isWaiting = false;
    }

    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveSpots[randomSpot].position.x, moveSpots[randomSpot].position.y), speed * Time.deltaTime);

        if (!isWaiting)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveSpots[randomSpot].position.x, transform.position.y), speed * Time.deltaTime);
            isWaiting = Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.3f;
            animator.SetFloat("Speed", 1);
            FlipCheck();
        }
        else
        {
            waitTime -= Time.deltaTime;
            if(waitTime < 0)
            {
                waitTime = startWaitTime;
                randomSpot = randomSpot == 0 ? moveSpots.Length - 1 : randomSpot - 1;
                isWaiting = false;
            }
            animator.SetFloat("Speed", 0);
        }

        if (timeBtwAttack <= 0 && attack)
        {
            attack = false;
            animator.SetTrigger("IsAttacking");
            Attack();
            timeBtwAttack = startTimeBtwAttack;

        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void Attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            var shield = enemiesToDamage[i].GetComponentInChildren<Shield>();
            var finalDamage = damage;
            if (shield != null)
                finalDamage = enemiesToDamage[i].enabled ? finalDamage * shield.avoidedDamagePercentage : finalDamage;
            enemiesToDamage[i].GetComponentInParent<HealthController>().TakeDamage(finalDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

    private void FlipCheck()
    {
        if (transform.position.x - moveSpots[randomSpot].position.x > -0.1 && facingRight)
        {
            transform.Rotate(0, 180, 0);
            facingRight = !facingRight;
        }
        else if (transform.position.x - moveSpots[randomSpot].position.x < 0.1 && !facingRight)
        {
            transform.Rotate(0, 180, 0);
            facingRight = !facingRight;
        }
    }
}
