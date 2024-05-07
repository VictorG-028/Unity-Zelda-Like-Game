using UnityEngine;

public class ListenerScript : MonoBehaviour
{
    [SerializeField] SpawnIcon spawnIcon;

    public GameObject listenedObject = null;

    private void OnValidate()
    {
        if (!spawnIcon) { spawnIcon = GetComponent<SpawnIcon>(); }    
        //if (!listenedObject) { listenedObject = GameObject.Find(); }
    }

    private void OnEnable()
    {
        // Inscreve-se no evento quando este script é ativado
        listenedObject.GetComponent<TriggerScript>().TriggerActivated += OnTriggerActivated;
    }

    private void OnDisable()
    {
        // Cancela a inscrição no evento quando este script é desativado
        listenedObject.GetComponent<TriggerScript>().TriggerActivated -= OnTriggerActivated;
    }

    // Método para lidar com o evento de trigger ativado
    private void OnTriggerActivated()
    {
        spawnIcon.UpdateSpawn();
    }
}
