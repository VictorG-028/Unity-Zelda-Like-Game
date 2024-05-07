using UnityEngine;

public class Claimable : MonoBehaviour
{
    [SerializeField] PlayerProperties playerProps = null;
    [SerializeField] SpriteRenderer spriteRenderer = null;
    [SerializeField] Transform pickables = null;
    [SerializeField] GameObject itemPrefab = null;
    [SerializeField] Sprite[] sprites = null;
    [SerializeField] string[] dropNames = null;
    [SerializeField] float[] dropChances = null;
    [SerializeField] Sprite openedChestSprite = null;

    // Parameter

    // Control
    private bool isBroken = false;


    private void OnValidate()
    {
        if (!playerProps) { playerProps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerProperties>(); }
        if (!spriteRenderer) { spriteRenderer = GetComponent<SpriteRenderer>(); }
        if (!pickables) { pickables = GameObject.Find("Pickables").transform; }
        if (!itemPrefab) { itemPrefab = GameObject.Find("Icon Example"); }
        if (sprites == null || sprites.Length == 0) { sprites = Resources.LoadAll<Sprite>("Icons"); }
        if (dropNames == null || sprites.Length == 0)
        {
            dropNames = new string[sprites.Length];
            for (int i = 0; i < sprites.Length; i++)
            {
                dropNames[i] = sprites[i].name;
            }
            //dropItems = new string[] { "Empty", "Half Heart", "Rupee", "Bomb plus_1", "Arrow plus_3" };
        }
        if (dropChances == null || dropChances.Length == 0)
        {
            dropChances = new float[sprites.Length];
            for (int i = 0; i < sprites.Length; i++)
            {
                dropChances[i] = 1.0f / sprites.Length;
            }
            //dropItems = new string[] { 0.3f, 0.3f, 0.2f, 0.15f, 0.15f };
        }
        if (!openedChestSprite) { openedChestSprite = Resources.Load<Sprite>("open_treasure_chest"); }
    }


    private void OnTriggerEnter(Collider other)
    {
        //    Debug.Log($"Collider {other} entrou no colider de algum Breackable");
        if (!isBroken && playerProps.rupees < 5)
        {
            Debug.Log($"Necessário 5 rupees para abrir o baú.");
            return;
        }

        if (!isBroken && other.CompareTag("Attack"))
        {
            Debug.Log($"O jogador atacou breakable!");
            Claim();
        }
    }


    public void Claim()
    {
        isBroken = true;
        DropItem();
        playerProps.rupees -= 5;
        spriteRenderer.sprite = openedChestSprite;
    }


    private void DropItem()
    {
        float randomValue = Random.value;
        float cumulativeProbability = 0;

        for (int i = 0; i < dropChances.Length; i++)
        {
            cumulativeProbability += dropChances[i];

            // Se o valor aleatório estiver dentro da faixa de probabilidade, dropa o item correspondente
            if (randomValue <= cumulativeProbability)
            {
                Debug.Log($"Dropou: {dropNames[i]}");

                // Spawn new pickable item
                GameObject newIcon = Instantiate(itemPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                //newIcon.SetActive(true);
                newIcon.transform.SetParent(pickables, true);
                newIcon.name = dropNames[i];
                SpriteRenderer spriteRenderer = newIcon.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprites[i];
                Pickable pickable = newIcon.GetComponent<Pickable>();
                pickable.itemName = dropNames[i];

                break;
            }
        }
    }
}
