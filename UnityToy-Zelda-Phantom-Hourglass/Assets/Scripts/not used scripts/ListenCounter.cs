using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=2pCkInvkwZ0&t=91s
// https://www.youtube.com/watch?v=Y7pp2gzCzUI

public class ListenCounter : MonoBehaviour
{
    public GameObject pressurePlate = null;
    private PressurePlate_2 script = null;
    private Animator animator = null;

    void Awake()
    {
        pressurePlate = GameObject.Find("PressurePlate_2");
        script = pressurePlate.GetComponent<PressurePlate_2>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (script.weight == 2)
        {
            animator.Play("Door_Open", 0, 0f);
        }
    }
}
