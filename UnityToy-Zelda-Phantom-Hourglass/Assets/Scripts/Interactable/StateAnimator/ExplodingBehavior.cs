using UnityEngine;

public class ExplodingBehavior : StateMachineBehaviour
{
    [SerializeField] PlayerProperties playerProps = null;

    private void OnValidate()
    {
        // TODO: encontrar um jeito de inicializar playerProps. Lembrar: esse script é StateMachineBehaviour e está anexado na animação Exploding
        // Ver: https://forum.unity.com/threads/get-animator-controller-for-a-drawer-on-a-statemachinebehaviour-property.450031/
        //if(!playerProps) { playerProps = GameObject.Find("Player").GetComponent<PlayerProperties>(); }
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject bomb = GameObject.Find("Bomb");
        bool isNear = false;
        float distance = 3.0f;

        Transform pivot = null;

        foreach (GameObject breakable in GameObject.FindGameObjectsWithTag("Breakable"))
        {
            pivot = breakable.transform.Find("pivot");
            if (pivot && pivot.name == "pivot")
            {
                // If has pivot, use it.
                isNear = (pivot.position - bomb.transform.position).magnitude <= distance;
            } else
            {
                isNear = (breakable.transform.position - bomb.transform.position).magnitude <= distance;
            }
            if (isNear)
            {
                breakable.GetComponent<Breackable>().Break();
            }
        }

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            isNear = (enemy.transform.position - bomb.transform.position).magnitude <= distance;
            if (isNear)
            {
                enemy.GetComponent<EnemyProperties>().TakeDamage(2);
            }
        }

        //objectToDisable.SetActive(false);
        Destroy(bomb);

        playerProps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerProperties>(); // Temp fix to problem  in validate
        playerProps.canAttack = true;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
