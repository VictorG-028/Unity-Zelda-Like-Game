using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyProperties : MonoBehaviour
{
    [SerializeField] EnemySpawnerManager enemySpawnerManager = null;

    // Actions
    public bool canJump = false;
    public bool canMove = true;
    public bool canAttack = true;

    // Stats
    public string enemyName = "";
    public int level = 0;
    public int baseHP = 1;
    public int maxHP = 1;
    public int currentHP = 1;
    public int spawnerId = -1; // -1 if not spawned by a spawner, spawnedId of spawner otherwise
    public int xpGiven = 5;

    // Prevent Bug
    private bool once = true;

    private void OnValidate()
    {
        if (!enemySpawnerManager) { enemySpawnerManager = GameObject.Find($"GameCore").GetComponent<EnemySpawnerManager>(); }
    }

    public void CalculateMaxHP(int newLevel)
    {
        this.level = newLevel;
        this.maxHP = baseHP + level;
        this.currentHP = maxHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"Collider {other} entrou no colider do {enemyName}");
        if (other.CompareTag("Attack"))
        {
            currentHP -= 1;
            Debug.Log($"O jogador atacou {enemyName}! ({currentHP}/{maxHP})HP");

            if (once && currentHP < 0)
            {
                once = false; // Prevent from calling enemySpawnerManager multiple times
                Destroy(gameObject);
                if (spawnerId > -1) { enemySpawnerManager.EnemyDeath(spawnerId, this.xpGiven); }
            }
        }
    }
}
