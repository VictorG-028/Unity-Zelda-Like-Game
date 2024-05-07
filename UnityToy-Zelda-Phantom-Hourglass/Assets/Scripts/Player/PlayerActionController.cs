using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerActionController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent = null;
    [SerializeField] Animator animator = null;
    [SerializeField] PlayerProperties playerProps = null;
    [SerializeField] Camera mainCamera = null;
    [SerializeField] ParticleSystem swordSlashEffect = null;
    [SerializeField] GameObject swordSlashGO = null;
    [SerializeField] ParticleSystem hitBlinkEffect = null;
    [SerializeField] GameObject hitBlinkGO = null;
    [SerializeField] GameObject bombPrefab = null;
    [SerializeField] ItemSelector itemSelector = null;
    [SerializeField] Sprite arrowSprite = null;

    // Parameters
    [Range(0.0f, 5.0f)]
    [SerializeField] float melleAttackDistance = 1.0f;
    [Range(0.0f, 5.0f)]
    [SerializeField] float melleAttackDuration = 1.51f;
    [Range(0.0f, 5.0f)]
    [SerializeField] float bombAttackDistance = 1.0f;
    [Range(0.0f, 5.0f)]
    [SerializeField] float arrowAttackDistance = 1.0f;
    [Range(3.0f, 15.0f)]
    [SerializeField] float arrowAttackDuration = 15.0f;
    [Range(0.0f, 10.0f)]
    [SerializeField] float arrowVelocity = 5.0f;

    // Control
    private readonly float heightAboveHead = 1.5f;
    Vector3 headPosition = Vector3.zero;
    private GameObject objectAboveHead = null;
    private int bombStage = 0;
    private Animator bombAnimator = null;


    void OnValidate()
    {
        if (!agent) { agent = gameObject.GetComponent<NavMeshAgent>(); }
        if (!animator) { animator = gameObject.GetComponent<Animator>(); }
        if (!playerProps) { playerProps = gameObject.GetComponent<PlayerProperties>(); }
        if (!mainCamera) { mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>(); }
        if (!swordSlashEffect) { swordSlashEffect = GameObject.Find("Sword Slash Effect").GetComponent<ParticleSystem>(); }
        if (!swordSlashGO) { swordSlashGO = GameObject.Find("Sword Slash Effect"); }
        if (!hitBlinkEffect) { hitBlinkEffect = GameObject.Find("Hit Blink Effect").GetComponent<ParticleSystem>(); }
        if (!hitBlinkGO) { hitBlinkGO = GameObject.Find("Hit Blink Effect"); }
        if (!bombPrefab) { bombPrefab = Resources.Load<GameObject>("Bomb"); }
        //if (!bombAnimator) { bombAnimator = bombPrefab.GetComponent<Animator>(); }
        if (!itemSelector) { itemSelector = GameObject.Find("Canvas Manager").GetComponent<ItemSelector>(); }
        if (!arrowSprite) { arrowSprite = Resources.Load<Sprite>("Icons/Arrow"); } // TODO descobrir pq o path está errado
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            switch (ActionMap[playerProps.usingName])
            {
                case "MelleAttack":
                    if (playerProps.canAttack)
                    { 
                        MelleAttack();
                    }
                    break;
                case "ThrowBomb":
                    if (playerProps.canAttack && playerProps.bombs > 0)
                    {
                        switch (bombStage)
                        {
                            case 0:
                                HoldBomb();
                                break;
                            case 1:
                                ThrowBomb();
                                break;
                        }
                    }
                    break;
                case "ThrowArrow":
                    if (playerProps.canAttack && playerProps.arrows > 0)
                    {
                        ThrowArrow();
                    }
                    break;
                case "Buy":
                    Buy();
                    break;
            }
        }
    }

    private Dictionary<string, string> ActionMap = new()
    {
        { "Basic Attack", "MelleAttack" },
        { "Bombs", "ThrowBomb" },
        { "Arrows", "ThrowArrow" },
        { "Rupees", "Buy" }
    };

    private void MelleAttack()
    {
        // Play Slash Visual Effect
        Vector3 lookDirection = CalculateLookDirection();

        swordSlashGO.transform.forward = lookDirection;
        hitBlinkGO.transform.localPosition = -lookDirection;

        swordSlashEffect.Play();
        hitBlinkEffect.Play();

        // Create collider to check for damage
        GameObject colliderObject = new("AttackCollider") { tag = "Attack" };
        //colliderObject.transform.parent = transform;
        colliderObject.transform.forward = lookDirection; 
        colliderObject.transform.position = transform.position + lookDirection * melleAttackDistance;
        BoxCollider collider = colliderObject.AddComponent<BoxCollider>(); // Adiciona um Collider
        collider.size = new Vector3(2.0f, 0.5f, 0.6f); // Largura, Altura, Profundidade
        collider.isTrigger = true;
        collider.providesContacts = true;
        //colliderObject.AddComponent<Rigidbody>(); // Adiciona um Rigidbody, se necessário
        Destroy(colliderObject, melleAttackDuration); // Destroi o Collider após o tempo de ataque
    }

    private void HoldBomb()
    {
        playerProps.canSwitchHold = false;

        headPosition = transform.position + Vector3.up * heightAboveHead;
        objectAboveHead = Instantiate(bombPrefab, headPosition, Quaternion.identity);
        objectAboveHead.transform.SetParent(transform);
        objectAboveHead.name = "Bomb";

        bombStage = 1;
    }
    private void ThrowBomb()
    {
        playerProps.canAttack = false;

        Vector3 lookDirection = CalculateLookDirection();

        objectAboveHead.transform.SetParent(null);
        objectAboveHead.transform.forward = lookDirection;
        objectAboveHead.transform.position = transform.position + lookDirection * bombAttackDistance;
        bombAnimator = objectAboveHead.GetComponent<Animator>();
        bombAnimator.Play("Exploding", 0, 0f);

        bombStage = 0;
        playerProps.bombs -= 1;
        itemSelector.UpdateItemFrameUI();
        playerProps.canSwitchHold = true;
    }

    private void ThrowArrow()
    {
        Vector3 lookDirection = CalculateLookDirection();
        GameObject arrowObject = new("AttackCollider") { tag = "Attack" };
        arrowObject.transform.forward = lookDirection;
        arrowObject.transform.position = transform.position + lookDirection * arrowAttackDistance;

        // Cria um gameObject filho para guardar a rotação do sprite sem afetar o collider
        GameObject arrowChild = new("arrowChild");
        arrowChild.transform.position = arrowObject.transform.position;
        arrowChild.transform.parent = arrowObject.transform;

        // Adiciona um sprite renderer com um sprite
        SpriteRenderer spriteRenderer = arrowChild.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = arrowSprite;
        spriteRenderer.sprite = arrowSprite;
        arrowChild.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f); // Altera escala do sprite

        // Faz o sprite fica na rotação correta
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        angle = (angle + 360f) % 360f; // Garante que o ângulo esteja no intervalo de 0 a 360 graus
        angle += (angle > 90f && angle < 270f) ? 180f : 0f; // Gira 180 no casos entre 90 e 270 graus
        arrowChild.transform.localRotation = Quaternion.Euler(new Vector3(90.0f, -45-angle, 0));

        // Adiciona um colider e rigidyBody
        BoxCollider collider = arrowObject.AddComponent<BoxCollider>();
        collider.center = new Vector3(0, 0.2f, 0); // x, y, z
        collider.size = new Vector3(0.4f, 0.2f, 1.2f); // Largura, Altura, Profundidade
        collider.isTrigger = true;
        collider.providesContacts = true;
        Rigidbody rigidbody = arrowObject.AddComponent<Rigidbody>();
        rigidbody.useGravity = false;

        // Adiciona lógica do colider
        arrowObject.AddComponent<ArrowController>();

        playerProps.arrows -= 1;
        itemSelector.UpdateItemFrameUI();
        StartCoroutine(MoveArrow(arrowObject));
    }

    private void Buy()
    {
        // TODO - Fazer algo com as Ruppes coletadas
    }

    private Vector3 CalculateLookDirection()
    {
        var rayFromCameraToMouse = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(rayFromCameraToMouse, out var hit, Mathf.Infinity);
        Vector3 lookPoint = hit.point;
        lookPoint.y = transform.position.y;

        Vector3 lookDirection = (lookPoint - transform.position).normalized;
        return lookDirection;
    }

    private IEnumerator MoveArrow(GameObject arrowObj)
    {
        float timer = 0f;
        //Destroy(arrowObj, arrowAttackDuration); // Destroi o Collider após o tempo de ataque
        while (timer < arrowAttackDuration)
        {
            if (arrowObj == null)
                yield break; // Sai da coroutine se o objeto da flecha for destruído prematuramente

            arrowObj.transform.position += arrowVelocity * Time.deltaTime * arrowObj.transform.forward;
            timer += Time.deltaTime;
            yield return null; // Aguarda um frame
        }
        Destroy(arrowObj);
    }


    // Testando particle system. Isso é chamado quando um collider do sistema entra em outro collider.
    private void OnParticleTrigger()
    {
        Debug.Log("[PlayerAttack] Aqui foi chamado!");
    }
}
