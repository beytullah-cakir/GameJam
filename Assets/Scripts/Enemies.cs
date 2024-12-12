using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    Transform player; // Oyuncunun Transform'u
    public float detectionRange = 40f; // Alg�lama menzili
    public float attackRange = 2f; // Sald�r� menzili
    public float moveSpeed = 7f; // Hareket h�z�
    private NavMeshAgent agent;
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    

    
   


void Start()
    {
        
        agent.speed = moveSpeed; // H�z� ayarla

        player = GameObject.FindWithTag("PLayer").GetComponent<Transform>();
       
    }

    protected virtual void Update()
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


    //d��man random gezicek
    //d��amn�n health bas�
    //d��man�n �lme durumu --- �l�nce yok et---Destroy(game..)
    //d��man sald�r� esnas�nda ate� etsin
    //lbirent tasar�m� 2 tane yeter

}
