using System.Collections;
using System.Linq;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] Renderer pressurePlateRenderer = null;
    [SerializeField] Animator animator = null; // Lembrar de inicialiar no editor

    // Control
    private bool activated = false;

    // Behavior in editor
    [SerializeField] int currentWeight = 0;
    [SerializeField] int targetWeight = 1;
    [SerializeField] bool keepActivatedAfterTrigger = true;
    [SerializeField] bool interactOnlyWithPlayer = false;
    [SerializeField] ActivableType activableType = ActivableType.NULL; // MUST BE INITIALIZED IN EDITOR


    private void OnValidate()
    {
        if (!pressurePlateRenderer) { pressurePlateRenderer = gameObject.GetComponent<Renderer>(); }
        // TODO: encontrar um jeito de validar o animator para qualquer porta
        if (!animator) { animator = GameObject.FindObjectsOfType<Animator>(true).Where(sr => !sr.gameObject.activeInHierarchy && sr.gameObject.name == "Door_1").ToArray()[0]; }
    }

    void OnTriggerEnter(Collider other)
    {
        //print("Trigger enter chamado");
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Player has pressed the pressure plate");
            currentWeight += 1;
        } else if (!interactOnlyWithPlayer && other.gameObject.tag == "Movable")
        {
            Debug.Log("Movable object has pressed the pressure plate");
            currentWeight += 1;
        }

        if (!activated && currentWeight == targetWeight)
        {
            activated = true;
            pressurePlateRenderer.material.SetColor("_Color", Color.blue);

            if (activableType == ActivableType.Door)
            {
                animator.Play("Door_Open", 0, 0f);
                //foreach (Transform child in transform)
                //{
                //    child.GetComponent<Animator>().Play("Door_Open", 0, 0f);
                //}
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //print("Trigger exit chamado");
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Player has exit the pressure plate");
            currentWeight -= 1;
        }
        else if (!interactOnlyWithPlayer && other.gameObject.tag == "Movable")
        {
            Debug.Log("Movable object has exit the pressure plate");
            currentWeight -= 1;
        }

        if (activated && !keepActivatedAfterTrigger && currentWeight < targetWeight)
        {
            activated = false;
            pressurePlateRenderer.material.SetColor("_Color", Color.gray);
            
            if (activableType == ActivableType.Door)
            {
                StartCoroutine(CloseAfterDelay(1.0f));
                
            }
        }
    }

    private IEnumerator CloseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.Play("Door_Close", 0, 0f);
    }

    public void ResetState()
    {
        // Default values
        activated = false;
        //currentWeight = 0;
        pressurePlateRenderer.material.SetColor("_Color", Color.gray);

        if (activableType == ActivableType.Door)
        {
            animator.Play("Door_Close", 0, 0f);
        }
    }
}
