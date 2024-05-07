using UnityEngine;

public class StartPosition : MonoBehaviour, IResetable
{
    [SerializeField] Vector3 initialPos = Vector3.zero;
    [SerializeField] Quaternion initialRotation = Quaternion.identity;

    private void OnValidate()
    {
        if (initialPos == Vector3.zero) { initialPos = gameObject.transform.position; }
        if (initialRotation == Quaternion.identity) { initialRotation = gameObject.transform.rotation; }
    }

    //public (Vector3 position, Quaternion rotation) GetInitialState()
    //{
    //    return (initialPos, initialRotation);
    //}

    public void ResetState()
    {
        transform.position = initialPos;
        transform.rotation = initialRotation;
    }
}
