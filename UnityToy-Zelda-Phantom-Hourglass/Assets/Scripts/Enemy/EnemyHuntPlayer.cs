using UnityEngine;
using UnityEngine.AI;

public class EnemyHuntPlayer : MonoBehaviour
{
    [SerializeField] EnemyProperties enemyprops = null;
    [SerializeField] NavMeshAgent agent = null;
    [SerializeField] Transform player = null;
    [SerializeField] Animator animator = null;
    public bool shouldHunt = true;

    private void OnValidate()
    {
        if (!enemyprops) { enemyprops = GetComponent<EnemyProperties>(); }
        if (!agent) { agent = GetComponent<NavMeshAgent>(); }
        if (!player) { player = GameObject.Find("Player").GetComponent<Transform>(); }
        if (!animator) { animator = gameObject.GetComponent<Animator>(); }
    }


    void Update()
    {   if (enemyprops.canMove && shouldHunt)
        {
            agent.SetDestination(player.position);

            var globalDirection = (agent.steeringTarget - transform.position).normalized;
            var localDirection = transform.InverseTransformDirection(globalDirection);

            animator.SetFloat("Horizontal", localDirection.x);
            animator.SetFloat("Vertical", -localDirection.z);
        }
        else
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            // Deleta o caminho que o agente tem guardado
            agent.isStopped = true;
            agent.ResetPath();
            agent.isStopped = false;
        }
    }
}
