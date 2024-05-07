using UnityEngine;

public class UILookAtMainCamera : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(
            transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up
        );
    }

    public static void LookAtCameraOnce(GameObject obj)
    {
        obj.transform.LookAt(
            obj.transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up
        );
    }
}
