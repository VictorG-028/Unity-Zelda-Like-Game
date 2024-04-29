using UnityEngine;
using UnityEngine.AI;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Renderer pressurePlateRenderer = null;
    [SerializeField] GameObject player = null;
    [SerializeField] PlayerProperties playerProperties = null;
    [SerializeField] NavMeshAgent playerNavMeshAgent = null;
    [SerializeField] Animator animator = null;

    // Control
    private bool isColliding = false;
    private bool locked = false;

    void OnValidate()
    {
        if (!pressurePlateRenderer) { pressurePlateRenderer = gameObject.GetComponent<Renderer>(); }
        if (!player) { player = GameObject.Find("Player"); }
        if (!playerProperties) { playerProperties = player.GetComponent<PlayerProperties>(); }
        if (!playerNavMeshAgent) { playerNavMeshAgent = player.GetComponent<NavMeshAgent>(); }
        if (!animator) { animator = GetComponent<Animator>(); }
    }

    void Update()
    {
        if (isColliding && Input.GetKeyDown(KeyCode.Space) && !locked)
        {
            Debug.Log("plataforma 5 triggered");
            playerNavMeshAgent.updatePosition = false; // https://discussions.unity.com/t/navmesh-y-axis/168297
            player.transform.SetParent(transform);
            animator.Play("Move_Around"); // https://www.youtube.com/watch?v=12ojksg7GiU
            locked = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            pressurePlateRenderer.material.SetColor("_Color", Color.blue);
            isColliding = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            pressurePlateRenderer.material.SetColor("_Color", Color.gray);
            isColliding = false;
            locked = false;
        }
    }
}
