using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainersController : MonoBehaviour
{
    [SerializeField] PlayerProperties playerProps = null;
    [SerializeField] Image[] containers = null;
    [SerializeField] Sprite emptyHeart = null;
    [SerializeField] Sprite halfHeart = null;
    [SerializeField] Sprite fullHeart = null;

    // Control


    private void OnValidate()
    {
        if (!playerProps) { playerProps = GameObject.Find("Player").GetComponent<PlayerProperties>(); }
        if (containers == null || containers.Length == 0) { containers = GameObject.FindGameObjectsWithTag("Heart Container")
                                                                        .Select(x => x.GetComponent<Image>()).ToArray(); }
        if (!emptyHeart) { emptyHeart = Resources.Load<Sprite>("Icons/Empty_Heart"); } // Assets/Sprites/Resources/Icons.png
        if (!halfHeart) { halfHeart = Resources.Load<Sprite>("Icons.Half_Heart"); } // TODO: descorbrir pq o path está errado
        if (!fullHeart) { fullHeart = Resources.Load<Sprite>("Icons\\Full_Heart"); }
    }

    private void Start()
    {
        UpdatePlayerUI();
    }

    public void UpdatePlayerUI()
    {
        int fullContainersAmmount = playerProps.HP / 2; // implicit convert float to int removes the 0.5 (floor operation)
        int halfFullContainersAmmount = playerProps.HP % 2;
        //int emptyContainersAmmount = containers.Length - fullContainersAmmount - halfFullContainersAmmount;

        for (int i = 0; i < fullContainersAmmount; i++)
        { containers[i].sprite = fullHeart; }

        for (int i = fullContainersAmmount; i < fullContainersAmmount + halfFullContainersAmmount; i++)
        { containers[i].sprite = halfHeart; }

        for (int i = fullContainersAmmount + halfFullContainersAmmount; i < containers.Length; i++)
        { containers[i].sprite = emptyHeart; }
    }
}
