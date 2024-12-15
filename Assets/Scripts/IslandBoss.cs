using System.Collections;
using Unity.Cinemachine.Samples;
using Unity.VisualScripting;
using UnityEngine;

public class IslandBoss : Enemies
{

    protected override void Awake()
    {
        base.Awake();
    }


    
    protected override void Update()
    {
        base.Update();
        anm.SetBool("Walk", agent.velocity.magnitude > 0.1f && !isDead);
    }

    protected  override IEnumerator  AttackPlayer()
    {
        base.AttackPlayer();
        anm.SetTrigger("Attack");

        yield return new WaitForSeconds(attackRate);
        isAttacking = false;
        agent.isStopped = false;
    }



}
