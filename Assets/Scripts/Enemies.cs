using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    Transform player; // Oyuncunun Transform'u
    public float detectionRange = 40f; // Algýlama menzili
    public float attackRange = 2f; // Saldýrý menzili
    public float moveSpeed = 7f; // Hareket hýzý
    private NavMeshAgent agent;
    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    

    
   


void Start()
    {
        
        agent.speed = moveSpeed; // Hýzý ayarla

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


    //düþman random gezicek
    //düþamnýn health basý
    //düþmanýn ölme durumu --- ölðnce yok et---Destroy(game..)
    //düþman saldýrý esnasýnda ateþ etsin
    //lbirent tasarýmý 2 tane yeter

}
