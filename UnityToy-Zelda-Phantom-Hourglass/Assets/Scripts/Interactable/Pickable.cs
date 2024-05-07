//using TMPro;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] PlayerProperties playerProps = null;
    [SerializeField] ItemSelector itemSelector = null;
    [SerializeField] HeartContainersController updateHeartContainers = null;

    // Debug
    //[SerializeField] TextMeshProUGUI debugText = null;

    // Control
    public string itemName = ""; // Inicialized when spawned

    private void OnValidate()
    {
        if(!player) { player = GameObject.Find("Player"); }
        if(!playerProps) { playerProps = player.GetComponent<PlayerProperties>(); }
        if(!itemSelector) { itemSelector = GameObject.Find("Canvas Manager").GetComponent<ItemSelector>(); }
        if (itemName.Length == 0) { itemName = gameObject.name; }
        if(!updateHeartContainers) { updateHeartContainers = GameObject.Find("Canvas Manager").GetComponent<HeartContainersController>(); }

        //if (!debugText) { debugText = GameObject.Find("Debug Text TMP").GetComponent<TextMeshProUGUI>();  }
    }

    private void OnTriggerEnter(Collider other)
    {
        //    Debug.Log($"Collider {other} entrou no colider de algum Pickable");
        if (other.CompareTag("Player"))
        {
            PickItem(itemName);
            Destroy(gameObject);
        }
    }

    private void PickItem(string name)
    {
        Debug.Log($"O jogador pegou item!");
        //debugText.text = $"O jogador pegou item {name}! {itemSelector}";
        switch (name)
        {
            case "Empty":
                break;
            case "Half_Heart":
                if (playerProps.HP < playerProps.maxHP)
                {
                    playerProps.HP += 1;
                    updateHeartContainers.UpdatePlayerUI();
                }
                break;
            case "Rupee":
                playerProps.rupees += 1;
                itemSelector.UpdateItemFrameUI();
                break;
            case "Bomb_plus_1":
                playerProps.bombs += 1;
                itemSelector.UpdateItemFrameUI();
                break;
            case "Arrow_plus_3":
                playerProps.arrows += 3;
                itemSelector.UpdateItemFrameUI();
                break;
            default:
                Debug.LogWarning("Item desconhecido: " + name);
                break;
        }
    }
}
