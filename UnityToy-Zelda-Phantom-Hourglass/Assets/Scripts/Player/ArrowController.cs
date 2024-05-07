using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Pickable")) //  || other.transform.parent.name == "Walls" || other.name == "Wall_cracked"
        {
            return;
        }
        Debug.Log($"Flecha colidiu com {other} {other.name} {other.tag}");
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("Pickable"))
        {
            return;
        }
        Debug.Log($"[OnCollisionEnter] Flecha colidiu com {collision} {collision.transform.name} {collision.transform.tag}");
        Destroy(gameObject);
    }
}
