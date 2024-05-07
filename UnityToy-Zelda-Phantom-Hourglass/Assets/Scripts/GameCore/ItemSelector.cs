using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
    [SerializeField] Sprite[] itens = null;
    [SerializeField] string[] itemNames = null;
    [SerializeField] TextMeshProUGUI itemQuantity = null;
    [SerializeField] TextMeshProUGUI itemNameGUI = null;
    [SerializeField] Image itemImage = null;
    [SerializeField] PlayerProperties playerProps = null;

    // Control
    private int i = 0; // Selected item index
    private string itemName = "SlashEffect"; // Selected item name
    private string parsedItemName = "Basic Attack";

    private void OnValidate()
    {
        if (itens == null || itens.Length == 0) { itens = Resources.LoadAll<Sprite>("Icons"); }
        if (itemNames == null || itemNames.Length == 0)
        {
            itemNames = new string[itens.Length];
            for (int i = 0; i < itens.Length; i++)
            {
                itemNames[i] = itens[i].name;
            }
        }
        if (!itemQuantity) { itemQuantity = GameObject.Find("Item Quantity").GetComponent<TextMeshProUGUI>(); }
        if (!itemNameGUI) { itemNameGUI = GameObject.Find("Item Name").GetComponent<TextMeshProUGUI>(); }
        if (!itemImage) { itemImage = GameObject.Find("Current Item Image").GetComponent<Image>(); }
        if (!playerProps) { playerProps = GameObject.Find("Player").GetComponent< PlayerProperties>(); }
    }

    private void Start()
    {
        UpdateItemFrameUI();
    }

    private void Update()
    {
        if (playerProps.canSwitchHold && Input.GetKeyDown(KeyCode.Space))
        {
            i += 1;
            i %= itens.Length;
            itemName = itemNames[i];
            parsedItemName = itemPropertyMap[itemNames[i]];
            playerProps.usingName = parsedItemName;
            UpdateItemFrameUI();
        }
    }

    public void UpdateItemFrameUI()
    {
        itemImage.sprite = itens[i];
        itemQuantity.text = playerProps.GetPropAmmount(parsedItemName);
        itemNameGUI.text = parsedItemName;
    }

    private readonly Dictionary<string, string> itemPropertyMap = new Dictionary<string, string>()
    {
        { "SlashEffect", "Basic Attack" },
        { "Bomb_Unlighten", "Bombs" },
        { "Arrow", "Arrows" },
        { "Rupee", "Rupees" }
    };
}
