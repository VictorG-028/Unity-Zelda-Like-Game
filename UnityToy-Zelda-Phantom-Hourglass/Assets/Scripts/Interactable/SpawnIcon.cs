using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIcon : MonoBehaviour
{
    /* Class to manage constant spawn of a pickable object
     *
     */

    [SerializeField] SpriteRenderer spriteRenderer = null;
    [SerializeField] Sprite closed = null;
    [SerializeField] Sprite open = null;
    [SerializeField] PlayerProperties playerProps = null;
    [SerializeField] Transform pickables = null;
    [SerializeField] ListenerScript listenerScript = null;

    // Must be Initialized in editor
    [SerializeField] Sprite iconSprite = null;
    [SerializeField] GameObject iconPrefab = null;
    [SerializeField] string iconName = null;

    // Control
    [Range(2f, 10f)] public float timeToSpawn = 5.0f;
    private bool isClosed = true;
    //private string parsedIconName = "Bombs"; 

    private void OnValidate()
    {
        //foreach (Sprite sprite in Resources.LoadAll<Sprite>("Icon Spawner"))
        //{
        //    Debug.Log(sprite.name);
        //}
        //foreach (Sprite sprite in Resources.LoadAll<Sprite>("Icons"))
        //{
        //    Debug.Log(sprite.name);
        //}
        if (!spriteRenderer) { spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); }
        if (!closed) { closed = Resources.Load<Sprite>("Icon Spawner"); }
        if (!open) { open = Resources.LoadAll<Sprite>("Icon Spawner")[0]; }
        if (!playerProps) { playerProps = GameObject.Find("Player").GetComponent<PlayerProperties>(); }
        if (!pickables) { pickables = GameObject.Find("Pickables").transform; }
        if (!listenerScript) { 
            listenerScript = gameObject.GetComponent<ListenerScript>();
            listenerScript.enabled = false;
        }
        
        // Must be inicialized in editor
        if (!iconSprite) { iconSprite = Resources.LoadAll<Sprite>("Icons")[0]; }
        if (!iconPrefab) { iconPrefab = GameObject.Find("Icon Example"); }
        if (iconName == null || iconName.Length == 0) { iconName = iconSprite.name; }
    }

    private void Start()
    {
        if (isClosed)
        {
            UpdateSpawn();
        }
    }

    public void UpdateSpawn()
    {
        isClosed = true;
        spriteRenderer.sprite = closed;
        listenerScript.enabled = false;
        StartCoroutine(SpawnAfterDelay(timeToSpawn));
    }

    private IEnumerator SpawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isClosed = false;
        spriteRenderer.sprite = open;

        // Spawn new pickable item
        GameObject newIcon = Instantiate(iconPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        newIcon.transform.SetParent(pickables, true);
        newIcon.name = iconName;
        SpriteRenderer iconSpriteRenderer = newIcon.GetComponent<SpriteRenderer>();
        iconSpriteRenderer.sprite = iconSprite;
        Pickable pickable = newIcon.GetComponent<Pickable>();
        pickable.itemName = iconName;

        TriggerScript triggerScript = newIcon.AddComponent<TriggerScript>();
        triggerScript.enabled = true;

        listenerScript.listenedObject = newIcon;
        listenerScript.enabled = true;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        //playerProps.IncrementPropByName(parsedIconName);
    //        isClosed = true;
    //        spriteRenderer.sprite = closed;
    //        UpdateSpawn();
    //    }
    //}

    //private Dictionary<string, string> MapSpriteToProp = new()
    //{
    //    { "Half_Heart", "HP" },
    //    { "Bomb_plus_1", "Bombs" },
    //    { "Arrow_plus_3", "Arrows" },
    //    { "Rupee", "Rupees" }
    //};
}
