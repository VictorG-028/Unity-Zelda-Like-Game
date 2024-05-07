using UnityEngine;

public class Breackable : MonoBehaviour
{
    [SerializeField] Transform pickables = null;
    [SerializeField] GameObject itemPrefab = null;
    [SerializeField] Sprite[] sprites = null;
    [SerializeField] string[] dropNames = null;
    [SerializeField] float[] dropChances = null;

    // Parameter
    public bool shouldDrop = true;
    public bool onlyExplosive = false;

    // Control
    private bool isBroken = false;


    private void OnValidate()
    {
        //if (!pickables) { pickables = GameObject.Find("Pickables").transform; }
        //if (!itemPrefab) { itemPrefab = GameObject.Find("Icon Example"); }
        if (sprites == null || sprites.Length == 0) { sprites = Resources.LoadAll<Sprite>("Icons"); }
        if (dropNames == null || sprites.Length == 0) {
            dropNames = new string[sprites.Length];
            for (int i = 0; i < sprites.Length; i++)
            {
                dropNames[i] = sprites[i].name;
            }
            //dropItems = new string[] { "Empty", "Half Heart", "Rupee", "Bomb plus_1", "Arrow plus_3" };
        }
        if (dropChances == null || dropChances.Length == 0) {
            dropChances = new float[sprites.Length];
            for (int i = 0; i < sprites.Length; i++)
            {
                dropChances[i] = 1.0f / sprites.Length;
            }
            //dropItems = new string[] { 0.3f, 0.3f, 0.2f, 0.15f, 0.15f };
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //    Debug.Log($"Collider {other} entrou no colider de algum Breackable");
        if (!isBroken && onlyExplosive)
        {
            Debug.Log($"Breackable resiste e só pode ser quebrado com bomba.");
            return;
        }

        if (!isBroken && other.CompareTag("Attack"))
        {
            Debug.Log($"O jogador atacou breakable!");
            Break();
        }
    }


    public void Break()
    {
        isBroken = true;

        if (shouldDrop) { DropItem(); }
        
        gameObject.SetActive(false);
        Destroy(gameObject);
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
                GameObject newIcon = Instantiate(itemPrefab, transform.position + new Vector3(0, 0.5f,0), Quaternion.identity);
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
