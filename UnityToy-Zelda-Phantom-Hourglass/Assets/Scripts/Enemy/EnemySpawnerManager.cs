using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] EnemySpawner[] spawners;
    [SerializeField] PlayerProperties playerProps;

    void OnValidate()
    {
        if (!playerProps) { playerProps = GameObject.Find("Player").GetComponent<PlayerProperties>(); }
    }

    private void Start()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].id = i;
        }
    }

    public void EnemyDeath(int id)
    {
        spawners[id].currentSpawns -= 1;
    }

    public void EnemyDeath(int id, int xpGiven)
    {
        spawners[id].currentSpawns -= 1;
        Debug.Log($"Player recebeu {xpGiven}xp");
        playerProps.ReciveXP(xpGiven);
    }

    //public void TurnOff(int id, string action)
    //{
    //    spawners[id].isActive = true;
    //}

    //public void TurnOn(int id, string action)
    //{
    //    spawners[id].isActive = false;
    //}
}
