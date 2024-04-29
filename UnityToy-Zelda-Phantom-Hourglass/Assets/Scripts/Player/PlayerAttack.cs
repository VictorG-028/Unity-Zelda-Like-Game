using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent = null;
    [SerializeField] Animator animator = null;
    [SerializeField] PlayerProperties playerProperties = null;
    [SerializeField] Camera mainCamera = null;

    [SerializeField] ParticleSystem swordSlashEffect = null;
    [SerializeField] GameObject swordSlashGO = null;
    [SerializeField] ParticleSystem hitBlinkEffect = null;
    [SerializeField] GameObject hitBlinkGO = null;

    [Range(0.0f, 5.0f)]
    [SerializeField] float distanceToAttack = 1.0f;
    [Range(0.0f, 5.0f)]
    [SerializeField] float attackDuration = 1.51f;


    void OnValidate()
    {
        if (!agent) { agent = gameObject.GetComponent<NavMeshAgent>(); }
        if (!animator) { animator = gameObject.GetComponent<Animator>(); }
        if (!playerProperties) { playerProperties = gameObject.GetComponent<PlayerProperties>(); }
        if (!mainCamera) { mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>(); }
        if (!swordSlashEffect) { swordSlashEffect = GameObject.Find("Sword Slash Effect").GetComponent<ParticleSystem>(); }
        if (!swordSlashGO) { swordSlashGO = GameObject.Find("Sword Slash Effect"); }
        if (!hitBlinkEffect) { hitBlinkEffect = GameObject.Find("Hit Blink Effect").GetComponent<ParticleSystem>(); }
        if (!hitBlinkGO) { hitBlinkGO = GameObject.Find("Hit Blink Effect"); }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //transform.forward
            //playerProperties.localDirection
            MelleAttack();
        }
    }

    private void MelleAttack()
    {
        // Play Slash Visual Effect
        var rayFromCameraToMouse = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(rayFromCameraToMouse, out var hit, Mathf.Infinity);
        Vector3 lookPoint = hit.point;
        lookPoint.y = transform.position.y;

        Vector3 lookDirection = (lookPoint - transform.position).normalized;

        swordSlashGO.transform.forward = lookDirection;
        hitBlinkGO.transform.localPosition = -lookDirection;

        swordSlashEffect.Play();
        hitBlinkEffect.Play();

        // Create collider to check for damage
        GameObject colliderObject = new GameObject("AttackCollider");
        colliderObject.tag = "Attack";
        //colliderObject.transform.parent = transform;
        colliderObject.transform.forward = lookDirection; 
        colliderObject.transform.position = transform.position + lookDirection * distanceToAttack; // Ajuste a posição conforme necessário
        BoxCollider collider = colliderObject.AddComponent<BoxCollider>(); // Adiciona um Collider
        collider.size = new Vector3(2.0f, 0.5f, 0.6f); // Largura, Altura, Profundidade
        collider.isTrigger = true;
        collider.providesContacts = true;
        //colliderObject.AddComponent<Rigidbody>(); // Adiciona um Rigidbody, se necessário
        Destroy(colliderObject, attackDuration); // Destroi o Collider após o tempo de ataque
    }

    private void OnParticleTrigger()
    {
        Debug.Log("[PlayerAttack] Aqui foi chamado!");
    }
}
