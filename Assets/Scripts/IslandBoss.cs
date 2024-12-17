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
        anm.SetBool("Walk", !agent.isStopped && !isDead && !isAttacking);
    }

    protected  override IEnumerator  AttackPlayer()
    {
        if (isAttacking) yield break;
        isAttacking = true;
        agent.isStopped = true;
        RotateTowardsPlayer();
        anm.SetTrigger("Attack");
<<<<<<< Updated upstream
=======
        GameManager.Instance.Player.TakeDamage();
>>>>>>> Stashed changes
        yield return new WaitForSeconds(attackRate);
        isAttacking = false;
        agent.isStopped = false;
    }



}
