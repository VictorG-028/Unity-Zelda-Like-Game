using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class OneDirectionLink : MonoBehaviour
{
    private NavMeshLink navMeshLink;
    private Transform player;
    public float activationAngle = 30f; // �ngulo de toler�ncia para ativa��o do link

    void Start()
    {
        navMeshLink = GetComponent<NavMeshLink>();
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        bool isNear = Vector3.Distance(transform.position, player.position) < navMeshLink.width;
        //float highestPoint = navMeshLink.startPoint.y > navMeshLink.endPoint.y ? navMeshLink.startPoint.y : navMeshLink.endPoint.y;
        //bool isTallEnough = player.position.y >= highestPoint;
        //Debug.Log($"start {navMeshLink.startPoint.y} | end {navMeshLink.endPoint.y}");
        if (isNear)
        {
            // Calcula a dire��o do jogador em rela��o ao NavMeshLink
            Vector3 playerForward = player.forward;
            playerForward.y = 0f; // Mant�m a dire��o apenas no plano horizontal (ignora a altura)

            Vector3 xAxisDirection = Vector3.right;

            float angleToXAxis = Vector3.Angle(playerForward, xAxisDirection);


            // Se o jogador estiver dentro do �ngulo de toler�ncia, ativa o NavMeshLink
            if (angleToXAxis <= activationAngle)
            {
                navMeshLink.enabled = true;
            }
            else
            {
                navMeshLink.enabled = false;
            }
        }
        else
        {
            // Desativa o NavMeshLink se o jogador estiver fora do alcance
            navMeshLink.enabled = false;
        }
    }
}
