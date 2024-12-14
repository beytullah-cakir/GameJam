using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{


    private NavMeshAgent agent;

    private Transform player;

    public LayerMask whatIsPlayer;

    public float health;

    public Vector3 walkPoint;

    bool walkPointSet;

    public float walkPointRange;

    public float attackRate;

    public float sightRange, attackRange;

    public bool playerInSightRange, playerInAttackRange;

    private Animator anm;

    private float currentSpeed;

    public float deathAnimationTime;

    private bool isDead,isAttacking;

    



    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }



    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        anm = GetComponent<Animator>();
        currentSpeed = agent.speed;

    }

    protected virtual void Update()
    {
        // Oyuncunun menzil içinde olup olmadýðýný kontrol et
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // Durum kontrolü
        if (!playerInSightRange && !playerInAttackRange && !isAttacking && !isDead)
        {
            Patroling();
        }
        else if (playerInSightRange && !playerInAttackRange && !isAttacking && !isDead)
        {
            ChasePlayer();
        }
        else if (playerInAttackRange && playerInSightRange && !isAttacking && !isDead)
        {
            StartCoroutine(AttackPlayer());
        }

        anm.SetBool("Walk", agent.speed > 0);
    }


    private IEnumerator AttackPlayer()
    {
        // Oyuncuya dön
        RotateTowardsPlayer();

        isAttacking = true;
        anm.SetTrigger("Attack");

        // Saldýrý süresince bekleme
        yield return new WaitForSeconds(attackRate);
        isAttacking = false;
        agent.speed = 0;
    }


    private void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Dönüþ hýzýný ayarlayýn
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

        agent.speed = currentSpeed;

        

    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        walkPointSet = true;


    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        agent.speed = currentSpeed;
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return; // Zaten ölmüþse iþlemi sonlandýr

        health -= damage;

        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        // NavMeshAgent'i devre dýþý býrak
        agent.enabled = false;

        // Animasyon oynat
        anm.SetTrigger("Die");

        // Rigidbody veya Collider ile fiziksel düþüþü saðla
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Rigidbody'nin fizik simülasyonuna dahil olmasýný saðlar
        }

        // Collider'larý devre dýþý býrak
        Collider[] colliders = GetComponents<Collider>();
        foreach (var col in colliders)
        {
            col.enabled = false; // Çarpýþmayý durdurur
        }

        // Belirli bir süre sonra düþmaný yok et
        Destroy(gameObject, deathAnimationTime);
    }







    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) TakeDamage(10);
    }






}
