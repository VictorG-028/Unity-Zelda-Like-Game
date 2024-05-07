using UnityEngine;
using System;

public class TriggerScript : MonoBehaviour
{
    // Delegado para o evento
    public delegate void TriggerActivatedEventHandler();

    // Evento acionado quando o trigger é ativado
    public event TriggerActivatedEventHandler TriggerActivated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            // Verifica se há assinantes para o evento
            if (TriggerActivated != null)
            {
                // Dispara o evento
                TriggerActivated();
            }
        }
    }
}
