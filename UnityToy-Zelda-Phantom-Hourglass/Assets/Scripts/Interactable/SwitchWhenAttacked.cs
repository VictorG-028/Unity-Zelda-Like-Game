using UnityEngine;
using System;

public class SwitchWhenAttacked : MonoBehaviour, IResetable
{
    [SerializeField] SpriteRenderer spriteRenderer = null; 
    [SerializeField] Sprite spriteOff = null;
    [SerializeField] Sprite spriteOn = null;

    // Parameters
    [SerializeField] GameObject[] objectsToActivate = null; // MUST BE INITIALIZED IN EDITOR
    
    // Control
    //private bool shouldAutoTurnOff = false; // TODO: implementar auto desligamento por tempo usando couroutine
    private bool on = false;
    private bool lockOnLogic = false;

    private void OnValidate()
    {
        if(!spriteRenderer) { spriteRenderer = GetComponent<SpriteRenderer>(); }
        if(!spriteOff) { spriteOff = spriteRenderer.sprite;  }
        if(!spriteOn) { spriteOn = spriteRenderer.sprite; }
    }

    private void OnTriggerEnter(Collider other)
    {
        //    Debug.Log($"Collider {other} entrou no colider do SwitchWhenAttacked");
        if (!lockOnLogic && other.CompareTag("Attack"))
        {
            Debug.Log($"O jogador atacou SwitchWhenAttacked!");
            on = true;
            lockOnLogic = true;
        }

        if (on)
        {
            //spriteRenderer.sprite = Resources.Load<Sprite>(spriteNameOn);
            spriteRenderer.sprite = spriteOn;
            ActivateObjects(true);
        }
    }
    private void ActivateObjects(bool isActive)
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.GetComponent<IActivable>().Activate();
        }
    }

    public void ResetState()
    {
        // Default values
        on = false;
        lockOnLogic = false;

        spriteRenderer.sprite = spriteOff;
        ActivateObjects(false);
    }
}
