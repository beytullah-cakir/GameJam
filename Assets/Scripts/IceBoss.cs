using System.Collections;
using UnityEngine;

public class IceBoss : Enemies
{
    public GameObject ice;
    public float iceVelocity;


    protected override void Start()
    {
        base.Start();
    }

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Update()
    {
        base.Update();
    }


    protected override IEnumerator AttackPlayer()
    {
        if (isAttacking) yield break;
        isAttacking = true;
        agent.isStopped = true;
        Vector3 aimDir = (player.position - transform.position).normalized;        
        Instantiate(ice, transform.position, Quaternion.identity);
        Rigidbody bulletRb = ice.GetComponent<Rigidbody>();
        bulletRb.linearVelocity = aimDir * iceVelocity;
        yield return new WaitForSeconds(attackRate);
        isAttacking = false;
        agent.isStopped = false;
    }
}
