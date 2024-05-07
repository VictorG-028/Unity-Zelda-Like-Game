using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IActivable, IResetable
{
    [SerializeField] Animator animator = null;

    private void OnValidate()
    {
        if(!animator) { animator = GetComponentInChildren<Animator>(true); }
    }

    public void Activate()
    {
        //animator.SetBool("IsOpen", true);
        animator.Play("Door_Open");
    }

    public void Deactivate()
    {
        StartCoroutine(CloseAfterDelay(1.0f));
    }

    private IEnumerator CloseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.Play("Door_Close", 0, 0f);
    }
    
    public void ResetState()
    {
        animator.Play("Door_Close", 0, 0f);
    }
}
