using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour, IActivable, IResetable
{

    [SerializeField] GameObject enemyPrefab = null;
    [SerializeField] PlayerProperties playerProps = null;

    // Must be manually initilized
    [SerializeField] string enemyExample = "Goblin Example";

    // Control
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
        NavMeshAgent navMeshAgent = enemyInstance.GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;
        Debug.Log($"Spawnou inimigo (id:{enemyProperties.spawnerId} level:{enemyProperties.level} maxHP:{enemyProperties.maxHP})");

        enemyInstance.SetActive(true);
        currentSpawns += 1;
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deativate()
    { 
        isActive = false; 
    }
}