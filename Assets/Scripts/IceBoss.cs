using System.Collections;
using UnityEngine;

public class IceBoss : Enemies
{
    public GameObject ice;
    public float iceVelocity;
    public Transform playerPos;


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

        Vector3 aimDir = (playerPos.position - transform.position).normalized;
        GameObject bullet = Instantiate(ice, transform.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        bulletRb.linearVelocity = aimDir * iceVelocity;

        yield return new WaitForSeconds(attackRate);
        isAttacking = false;
        agent.isStopped = false;

    }
}
