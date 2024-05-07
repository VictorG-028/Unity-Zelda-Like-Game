using UnityEngine;
using UnityEngine.AI;

public class ElevatorBehavior : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Reativou nav mesh do player");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject origin = GameObject.Find("OriginEmpty");
        GameObject platform = GameObject.Find("PressurePlate_5_elevator_start");

        NavMeshAgent playerNavMeshAgent = player.GetComponent<NavMeshAgent>();


        playerNavMeshAgent.enabled = true;
        Debug.Log(playerNavMeshAgent.destination);

        // Deleta o caminho que o agente tenta ir
        //playerNavMeshAgent.isStopped = true;
        //playerNavMeshAgent.ResetPath();
        //playerNavMeshAgent.isStopped = false;

        playerNavMeshAgent.updatePosition = true;

        Debug.Log(playerNavMeshAgent.destination);

        player.transform.SetParent(origin.transform);

        Destroy(platform);
    }
}
