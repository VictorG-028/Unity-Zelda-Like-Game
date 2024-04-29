using System.Linq;
using UnityEngine;

public class PressurePlate_2 : MonoBehaviour
{
    private bool isColliding = false;
    private Renderer pressurePlateRenderer = null;
    private Animator animator = null;
    public int weight = 0;
    public int neededWeight = 2;
    private bool once = true;

    private void OnValidate()
    {
        if (!pressurePlateRenderer) { pressurePlateRenderer = gameObject.GetComponent<Renderer>(); }
        if (!animator) { animator = GameObject.FindObjectsOfType<Animator>(true).Where(sr => !sr.gameObject.activeInHierarchy && sr.name == "Door_2").ToArray()[0]; }
    }
    
    void Update()
    {
        if (isColliding && weight == neededWeight && once)
        {
            print($"Ativou a placa de pressão com {weight} pesos");
            pressurePlateRenderer.material.SetColor("_Color", Color.blue);
            animator.Play("Door_Open", 0, 0f);
            once = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        print("Trigger enter chamado");
        if (other.gameObject.name == "Player")
        {
            //Debug.Log("Collided with player");
            weight += 1;
        } 
        else
        {
            weight += 1;
        }
        isColliding = true;
    }

    void OnTriggerExit(Collider other)
    {
        print("Trigger exit chamado");
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Collided with player");
            weight -= 1;
        }
        else
        {
            weight += 1;
        }
        isColliding = false;
    }
}
