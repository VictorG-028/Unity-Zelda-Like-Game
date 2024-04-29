using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform player;
    private Vector3 cameraOffset;

    [Range(0.01f, 1.0f)]
    public float smoothness = 0.5f;

    void Start()
    {
        Vector3 relativeToPlayerPosition = player.transform.position;
        relativeToPlayerPosition.y += 10.0f;
        relativeToPlayerPosition.z += -8.0f;
        transform.position = relativeToPlayerPosition;
        transform.LookAt(player);

        cameraOffset = transform.position - player.transform.position;
    }

    void Update()
    {
        Vector3 newPos = player.position + cameraOffset;
        //Debug.Log(transform.position);
        transform.position = Vector3.Slerp(transform.position, newPos, smoothness);
    }
}
