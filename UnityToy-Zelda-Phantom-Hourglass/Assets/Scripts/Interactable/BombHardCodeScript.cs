using Unity.VisualScripting;
using UnityEngine;

// https://www.youtube.com/watch?v=krAOf_DgvRg

public class BombHardCodeScript : MonoBehaviour
{
    public GameObject objectAboveHead = null;
    private Animator animator = null;
    public GameObject player = null;
    public Transform playerT = null;
    private PlayerProperties playerProps = null;

    private readonly float heightAboveHead = 1.5f;
    public bool isStart = false;
    public bool isEnd = false;

    private void OnValidate()
    {
        if (!objectAboveHead) { objectAboveHead = GameObject.Find("Bomb"); }
        if (!animator) { animator = objectAboveHead.GetComponent<Animator>(); }
        if (!player) { player = GameObject.FindGameObjectWithTag("Player"); }
        if (!playerT) { playerT = player.transform; }
        if (!playerProps) { playerProps = player.GetOrAddComponent<PlayerProperties>(); }
    }

    void OnTriggerEnter(Collider other)
    {
        //print("on trigger enter chamado");
        if (other.gameObject.tag == "Player")
        {
            if (isStart)
            {
                Vector3 headPosition = playerT.position + Vector3.up * heightAboveHead;
                objectAboveHead.transform.position = headPosition;
                objectAboveHead.transform.SetParent(playerT);
                gameObject.SetActive(false);
                playerProps.bombs += 1;
            }
            else if (isEnd)
            {
                if (playerProps.bombs > 0)
                {
                    playerProps.bombs -= 1;
                    objectAboveHead.transform.position = transform.position + Vector3.up * 0.25f;
                    //renderer.material.color = Color.red;
                    animator.Play("Exploding", 0, 0f);
                    objectAboveHead.transform.SetParent(null);
                    gameObject.SetActive(false);
                }
            }
        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //    //print("on trigger exit chamado");
    //    //if (other.gameObject.name == "Player")
    //    //{
    //    //    Vector3 headPosition = player.transform.position + Vector3.up * heightOffset;
    //    //    objectAboveHead.transform.position = headPosition;
    //    //}
    //}
}
