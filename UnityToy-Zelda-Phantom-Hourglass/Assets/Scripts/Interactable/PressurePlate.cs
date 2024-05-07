using System.Collections;
using System.Linq;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] Renderer pressurePlateRenderer = null;

    // Behavior in editor
    [SerializeField] int currentWeight = 0;
    [SerializeField] int targetWeight = 1;
    [SerializeField] bool keepActivatedAfterTrigger = true;
    [SerializeField] bool interactOnlyWithPlayer = false;
    //[SerializeField] ActivableType activableType = ActivableType.NULL; // MUST BE INITIALIZED IN EDITOR
    [SerializeField] GameObject[] objectsToActivate = null; // MUST BE INITIALIZED IN EDITOR

    // Control
    private bool activated = false;


    private void OnValidate()
    {
        if (!pressurePlateRenderer) { pressurePlateRenderer = gameObject.GetComponent<Renderer>(); }
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

            foreach(GameObject obj in objectsToActivate)
            {
                obj.GetComponent<IActivable>().Activate();
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
            
            foreach (GameObject obj in objectsToActivate)
            {
                obj.GetComponent<IActivable>().Deactivate();
            }
        }
    }


    public void ResetState()
    {
        // Default values
        activated = false;
        //currentWeight = 0;
        pressurePlateRenderer.material.SetColor("_Color", Color.gray);

        foreach (GameObject obj in objectsToActivate)
        {
            obj.GetComponent<IResetable>().ResetState();
        }
    }
}
