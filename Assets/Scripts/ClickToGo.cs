using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class ClickToGo : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    Ray ray;
    RaycastHit hit;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void FixedUpdate()
    {
        animator.SetFloat("Blend",agent.velocity.magnitude);
        Debug.Log(agent.velocity.magnitude);
    }

    public void click(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.performed)
        {
            ray=Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit))
                agent.SetDestination(hit.point);
        }
    }


}
