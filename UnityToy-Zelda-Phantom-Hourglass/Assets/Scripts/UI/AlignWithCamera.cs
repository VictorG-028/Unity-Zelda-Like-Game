using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignWithCamera : MonoBehaviour
{
    [SerializeField] Camera mainCamera = null;

    // Control
    private Vector3 vector = Vector3.zero;

    private void OnValidate()
    {
        if(!mainCamera) { mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>(); }
    }

    void LateUpdate()
    {
        vector = gameObject.transform.position - mainCamera.transform.position;
        vector.x = 0f;
        transform.forward = vector;
    }
}
