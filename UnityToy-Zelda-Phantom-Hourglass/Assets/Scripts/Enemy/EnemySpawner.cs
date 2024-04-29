using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject enemyPrefab = null;
    [SerializeField] PlayerProperties playerProps = null;

    [SerializeField] string enemyExample = "Goblin Example";
    public int id = -1;
    public int maxSpawns = 1;
    public int currentSpawns = 0;
    public bool isActive = false;

    private void OnValidate()
    {
        if (!enemyPrefab) { enemyPrefab = GameObject.Find(enemyExample); }
        if (!playerProps) { playerProps = GameObject.Find("Player").GetComponent<PlayerProperties>(); }
    }
    void Update()
    {
        if (isActive && currentSpawns < maxSpawns)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        // Acessa o componente EnemyProperties e modifica atributos
        EnemyProperties enemyProperties = enemyInstance.GetComponent<EnemyProperties>();
        EnemyHuntPlayer enemyHuntProps = enemyInstance.GetComponent<EnemyHuntPlayer>();
        enemyProperties.spawnerId = id;
        enemyProperties.CalculateMaxHP(playerProps.level);
        enemyHuntProps.shouldHunt = true;
        Debug.Log($"Spawnou inimigo (id:{enemyProperties.spawnerId} level:{enemyProperties.level} maxHP:{enemyProperties.maxHP})");

        enemyInstance.SetActive(true);
        currentSpawns += 1;
    }
}