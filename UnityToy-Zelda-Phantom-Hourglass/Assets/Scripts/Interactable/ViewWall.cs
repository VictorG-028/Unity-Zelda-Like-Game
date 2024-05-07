using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using TMPro;


public class ViewWall : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] NavMeshAgent playerNavMeshAgent = null;
    [SerializeField] PlayerProperties playerProps = null;
    [SerializeField] GameObject mainCamera = null;

    // Debug
    //[SerializeField] TextMeshProUGUI debugText = null;

    // Parameters
    [SerializeField] float heightOffset = 1.5f;

    // Control
    private CinemachineVirtualCamera newCamera = null;
    private bool focus = false;

    void OnValidate()
    {
        if (!player) { player = GameObject.Find("Player"); }
        if (!playerNavMeshAgent) { playerNavMeshAgent = player.GetComponent<NavMeshAgent>(); }
        if (!playerProps) { playerProps = player.GetComponent<PlayerProperties>(); }
        if (!mainCamera) { mainCamera = GameObject.Find("Main Camera"); }
        
        //if (!debugText) { debugText = GameObject.Find("Debug Text TMP").GetComponent<TextMeshProUGUI>(); }
    }

    void Update()
    {
        if (focus && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)))
        {
            focus = false;
            playerProps.canPause = true;
            StartCoroutine(EnableSwitchHoldItemDelay(2.0f));
            SwitchToMainCamera();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            focus = true;
            playerProps.canPause = false;
            playerProps.canSwitchHold = false;
            CreateNewCamera();
        }
    }

    void CreateNewCamera()
    {
        // Cria uma nova câmera na posição do ponto empty
        newCamera = new GameObject("Temp Camera").AddComponent<CinemachineVirtualCamera>();
        newCamera.transform.SetPositionAndRotation(
            transform.position + -transform.forward * heightOffset, 
            Quaternion.identity
         );

        // Desativa a câmera principal
        //mainCamera.SetActive(false);
        newCamera.Priority = 11;
        //debugText.text = $"AQUI {newCamera.GetComponent<CinemachineVirtualCamera>().isActiveAndEnabled}";

        // Desativa movimentação do jogador
        playerNavMeshAgent.updatePosition = false;
        playerProps.canMove = false;
    }

    void SwitchToMainCamera()
    {
        mainCamera.SetActive(true);
        newCamera.Priority = 0;
        StartCoroutine(DestroyAfterDelay(3.0f));
        //Destroy(newCamera.gameObject);

        // Resativa movimentação do jogador
        playerNavMeshAgent.updatePosition = true;
        playerProps.canMove = true;
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(newCamera.gameObject);
    }

    private IEnumerator EnableSwitchHoldItemDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerProps.canSwitchHold = true;
    }
}
