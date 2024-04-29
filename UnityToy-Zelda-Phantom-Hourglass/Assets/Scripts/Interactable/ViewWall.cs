using UnityEngine;
using UnityEngine.AI;


public class ViewWall : MonoBehaviour
{
    private GameObject player = null;
    private NavMeshAgent playerNavMeshAgent = null;
    private PlayerProperties playerProps = null;
    private GameObject mainCamera = null;

    private Camera newCamera = null;
    private bool focus = false;

    // Behavior
    [SerializeField] float heightOffset = 1.0f;

    void OnValidate()
    {
        if (!player) { player = GameObject.Find("Player"); }
        if (!playerNavMeshAgent) { playerNavMeshAgent = player.GetComponent<NavMeshAgent>(); }
        if (!playerProps) { playerProps = player.GetComponent<PlayerProperties>(); }
        if (!mainCamera) { mainCamera = GameObject.Find("Main Camera"); }
    }

    void Update()
    {
        if (focus && Input.GetKeyDown(KeyCode.Escape))
        {
            focus = false;
            SwitchToMainCamera();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            focus = true;
            CreateNewCamera();
        }
    }

    void CreateNewCamera()
    {
        // Cria uma nova câmera na posição do ponto empty
        newCamera = new GameObject("Temp Camera").AddComponent<Camera>();
        newCamera.transform.position = transform.position + -transform.forward * heightOffset;
        newCamera.transform.rotation = Quaternion.identity;

        // Desativa a câmera principal
        mainCamera.SetActive(false);

        // Desativa movimentação do jogador
        playerNavMeshAgent.updatePosition = false;
        playerProps.canMove = false;
    }

    void SwitchToMainCamera()
    {
        mainCamera.SetActive(true);
        Destroy(newCamera.gameObject);

        // Resativa movimentação do jogador
        playerNavMeshAgent.updatePosition = true;
        playerProps.canMove = true;
    }
}
