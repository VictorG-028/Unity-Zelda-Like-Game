using UnityEngine;
using UnityEngine.AI;

public class ElevatorBehavior : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Reativou nav mesh do player");
        GameObject player = GameObject.Find("Player");
        NavMeshAgent playerNavMeshAgent = player.GetComponent<NavMeshAgent>();

        // Deleta o caminho que o agente tenta ir
        playerNavMeshAgent.isStopped = true;
        playerNavMeshAgent.ResetPath();
        playerNavMeshAgent.isStopped = false;

        playerNavMeshAgent.updatePosition = true;

        GameObject origin = GameObject.Find("OriginEmpty");
        player.transform.SetParent(origin.transform);
        //player.transform.SetParent(null);
    }
}
