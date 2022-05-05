using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{

    public int maxHealth = 100;
    public int damageAmount = 25;
    [SerializeField] int currentHealth = 100;
    public int attackDistance;
    public int moveSpeed;
    public float timer;
    public GameObject agroZone;
    public GameObject triggerArea;

    public GameObject coinPrefab;
    [HideInInspector] public bool inRange;
    [HideInInspector] public Transform target;

    Animator anim;
    float distance;
    bool attackMode;
    bool coolDown;
    float intTimer;


    public Transform attackPoint;
    public LayerMask playerLayer;

    public float attackRange = 0.5f;


    void Awake()
    {
        intTimer = timer;
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // player detected
        if (inRange)
        {
            EnemyLogic();
        }

        if (inRange == false)
        {
            anim.SetBool("Walk", false);
            StopAttack();
        }
    }

    void CoolDown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && coolDown && attackMode)
        {
            coolDown = false;
            timer = intTimer;
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        else if (attackDistance >= distance && coolDown == false)
        {
            Attack();
        }

        if (coolDown)
        {
            CoolDown();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("Walk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;

        anim.SetBool("Walk", false);
        anim.SetBool("Attack", true);
    }

    void SendDamage()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach(Collider2D collider in hitColliders)
        {
            collider.GetComponent<PlayerController>().TakeDamage(damageAmount);
        }
    }

    void StopAttack()
    {
        coolDown = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    void TriggerCooldown()
    {
        coolDown = true;
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }
        transform.eulerAngles = rotation;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity) as GameObject;
        coin.GetComponent<Coin>().setValue(Random.Range(25, 50));
        anim.SetBool("IsDead", true);
        GetComponentInChildren<Collider2D>().enabled = false;
        GetComponentInChildren<Rigidbody2D>().isKinematic = true;
        GetComponentInChildren<EnemyAgro>().enabled = false;
        this.enabled = false;
        // Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
