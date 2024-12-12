using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    public Transform player; // Oyuncunun Transform'u
    public float detectionRange = 40f; // Algýlama menzili
    public float attackRange = 2f; // Saldýrý menzili
    public float moveSpeed = 7f; // Hareket hýzý
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed; // Hýzý ayarla

        // Oyuncuyu bul
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                if (distanceToPlayer <= attackRange)
                {
                    // Oyuncuya saldýr
                    agent.ResetPath(); // Hareketi durdur
                    AttackPlayer();
                }
                else
                {
                    // Oyuncuya doðru hareket et
                    agent.SetDestination(player.position);
                }
            }
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Oyuncuya saldýrýlýyor!");
        // Saldýrý kodunu buraya ekleyin
        // Örneðin: player.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
    }
}
