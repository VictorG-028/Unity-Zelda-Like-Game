using UnityEngine;

public class ToggleActive : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDeactivate;

    private void Start()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in objectsToDeactivate)
        {
            obj.SetActive(true);
        }
    }
}
