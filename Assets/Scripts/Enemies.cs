using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected Transform player;
    public LayerMask whatIsPlayer;

    public float health;
    protected bool walkPointSet;
    public float sightRange, attackRange;
    public  float attackRate;

    public bool playerInSightRange, playerInAttackRange;

    protected Animator anm;
    protected float currentSpeed;

    public float deathAnimationTime;

    protected bool isDead, isAttacking;
    Vector3 walkPoint;
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        anm = GetComponent<Animator>();
        currentSpeed = agent.speed;
    }

    protected virtual void Update()
    {

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);



        if (!playerInSightRange && !playerInAttackRange && !isDead)
            Patroling();

        if (playerInSightRange && !playerInAttackRange && !isDead)
            ChasePlayer();

        if (playerInAttackRange && !isDead)
        {
            RotateTowardsPlayer();
            StartCoroutine(AttackPlayer());
        }
            
    }



    protected void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 20f);
    }

    protected void Patroling()
    {
        
        if (!walkPointSet)
        {
            walkPoint = GameManager.Instance.RandomPoints().position;
            agent.SetDestination(walkPoint);
            walkPointSet = true;
        }

        if (walkPointSet && agent.remainingDistance < 0.1f)
        {
            walkPointSet = false;
        }

    }
    protected void ChasePlayer()
    {
        RotateTowardsPlayer();
        agent.SetDestination(player.position);
        agent.isStopped = false;
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    protected void TakeDamage(float damage)
    {
        if (isDead) return;

        health = Mathf.Max(health - damage, 0);

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    protected void Die()
    {
        isDead = true;
        anm.SetTrigger("Die");
        //agent.isStopped = true;
        agent.enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        Destroy(gameObject, deathAnimationTime);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) TakeDamage(10);
    }

    protected virtual IEnumerator AttackPlayer()
    {
        yield return null;
    }
}
