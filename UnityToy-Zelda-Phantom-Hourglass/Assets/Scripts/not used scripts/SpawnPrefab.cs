using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public GameObject doorPrefab;
    public string prefabName = "";

    [Range(0.0f, 50.0f)]
    public float width;
    [Range(0.0f, 50.0f)]
    public float height;
    [Range(0.0f, 10.0f)]
    public float depth = 1;
    //private bool open = true;

    void Awake()
    {
        GameObject door = Instantiate(doorPrefab, new Vector3(0,0,0), Quaternion.identity);

        door.SetActive(true);
        door.transform.localScale = new Vector3(width, height, depth);
        door.transform.SetPositionAndRotation(transform.position, transform.rotation);
        door.name = prefabName;
        door.transform.SetParent(transform);
    }
}
