using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerProperties playerProps = null;
    [SerializeField] NavMeshAgent agent = null;
    [SerializeField] Animator animator = null;
    [SerializeField] PlayerProperties playerProperties = null;
    [SerializeField] Camera mainCamera = null;

    [SerializeField] bool showDebugMovementLine = false;


    void OnValidate()
    {
        if (!playerProps) { playerProps = gameObject.GetComponent<PlayerProperties>(); }
        if (!agent) { agent = gameObject.GetComponent<NavMeshAgent>(); }
        if (!animator) { animator = gameObject.GetComponent<Animator>(); }
        if (!playerProperties) { playerProperties = gameObject.GetComponent<PlayerProperties>(); }
        if (!mainCamera) { mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>(); }
    }


    void Update()
    {
        if (playerProps.canMove && agent.hasPath)
        {
            var globalDirection = (agent.steeringTarget - transform.position).normalized;
            var localDirection = transform.InverseTransformDirection(globalDirection);
            playerProperties.LocalDirection = localDirection;

            animator.SetFloat("Horizontal", localDirection.x);
            animator.SetFloat("Vertical", -localDirection.z);

            transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);

            //if (Vector3.Distance(transform.position, agent.destination) < agent.radius)
            //{
            //    agent.ResetPath();
            //}
        } else
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
        }

        if (playerProps.canMove && Input.GetMouseButtonDown(0)) // When pressing mouse left button
        {
            // Checking if the raycast shot hits something that uses the navmesh system.
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            var isHit = Physics.Raycast(ray, out RaycastHit hit, 20); // Mathf.Infinity

            if (playerProperties.canMove && isHit)
            {
                agent.SetDestination(hit.point);
            }
        }
        //else if (Input.GetKeyDown(KeyCode.Space) && playerProperties.canJump)
        //{
        //    //characterController.Move(new Vector3(0, jumpForce, 0));
        //    Vector3 v = transform.position;
        //    v.y += 5;
        //    transform.position = v;
        //}


        //if (shouldRotate)
        //{
        //    shouldRotate = false;
        //    // ROTATION
        //    Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
        //    float rotationY = Mathf.SmoothDampAngle(
        //        transform.eulerAngles.y,
        //        rotationToLookAt.eulerAngles.y,
        //        ref rotateVelocity,
        //        rotateSpeedMovement * (Time.deltaTime * 5)
        //    );

        //    transform.eulerAngles = new Vector3(0, rotationY, 0);
        //}
    }

    private void OnDrawGizmos()
    {
        if (showDebugMovementLine && agent.hasPath)
        {
            for (var i = 0; i < agent.path.corners.Length - 1; i++)
            {
                Debug.DrawLine(agent.path.corners[i], agent.path.corners[i + 1], Color.blue);
            }
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
