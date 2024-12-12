using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    public Transform player; // Oyuncunun Transform'u
    public float detectionRange = 40f; // Alg�lama menzili
    public float attackRange = 2f; // Sald�r� menzili
    public float moveSpeed = 7f; // Hareket h�z�
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed; // H�z� ayarla

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
                    // Oyuncuya sald�r
                    agent.ResetPath(); // Hareketi durdur
                    AttackPlayer();
                }
                else
                {
                    // Oyuncuya do�ru hareket et
                    agent.SetDestination(player.position);
                }
            }
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Oyuncuya sald�r�l�yor!");
        // Sald�r� kodunu buraya ekleyin
        // �rne�in: player.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
    }
}
