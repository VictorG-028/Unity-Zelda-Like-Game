using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField] GameObject player = null;
    [SerializeField] PlayerProperties playerProps = null;
    [SerializeField] NavMeshAgent playerNav = null;
    [SerializeField] Vector3 startPosition = Vector3.zero;
    [SerializeField] EnemySpawnerManager enemySpawnerManager = null;
    [SerializeField] TextMeshProUGUI pauseText = null;


    private void OnValidate()
    {
        if (!player) { player = GameObject.Find("Player"); }
        if (!playerProps) { playerProps = player.GetComponent<PlayerProperties>(); }
        if (!playerNav) { playerNav = player.GetComponent<NavMeshAgent>(); }
        if (startPosition == Vector3.zero) { startPosition = player.transform.position; }
        if (!enemySpawnerManager) { enemySpawnerManager = gameObject.GetComponent<EnemySpawnerManager>(); }
        if (!pauseText) { pauseText = GameObject.Find("Pause Text TMP").GetComponent<TextMeshProUGUI>(); }
    }

    private void Update()
    {
        if (playerProps.canPause && Input.GetKeyDown(KeyCode.Escape)) 
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            pauseText.text = pauseText.text.Equals("Pause") ? "" : "Pause";
        }
    }

    public void Restart()
    {
        //// Deleta o caminho que o agente tem guardado
        //playerNav.isStopped = true;
        //playerNav.ResetPath();
        //playerNav.isStopped = false;

        //// Kill all enemies
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //foreach (GameObject enemy in enemies)
        //{
        //    EnemyProperties eProps = enemy.GetComponent<EnemyProperties>();
        //    if (eProps.spawnerId > -1) { enemySpawnerManager.EnemyDeath(eProps.spawnerId); }
        //    Destroy(enemy);
        //}

        //// Restart spawner switch
        //GameObject[] uncompletedSwitches = GameObject.FindGameObjectsWithTag("UncompletedSwitch");
        //foreach (GameObject us in uncompletedSwitches)
        //{
        //    us.GetComponent<SwitchWhenAttacked>().resetState();
        //}

        //// Restart movables to initialState
        //GameObject[] movables = GameObject.FindGameObjectsWithTag("Movable");
        //foreach (GameObject movable in movables)
        //{
        //    var (position, rotation) = movable.GetComponent<StartPosition>().GetInitialState();
        //    movable.transform.position = position;
        //    movable.transform.rotation = rotation;
        //    movable.transform.localScale = Vector3.one;
        //}

        //// Restart pressure plate
        //GameObject[] uncompletedPlates = GameObject.FindGameObjectsWithTag("UncompletedPressurePlate");
        //foreach (GameObject up in uncompletedPlates)
        //{
        //    up.GetComponent<PressurePlate>().ResetState();
        //}

        //// Put default value to each propertie
        //player.transform.position = startPosition;

        //// Actions
        //playerProps.canJump = false;
        //playerProps.canMove = true;
        //playerProps.canAttack = true;

        //// Special properties
        //playerProps.iFrame = false;

        //// Stats
        //playerProps.level = 1;
        //playerProps.xpNeeded = 0;
        //playerProps.previousXpNeeded = 0;
        //playerProps.xp = 0;
        //playerProps.HP = 4;
        //playerProps.maxHP = 4;
        //playerProps.mana = 0;

        //// Bag of itens
        //playerProps.bombs = 0;
        //playerProps.arrows = 0;

        // Go To Death Screen Scene
        SceneManager.LoadScene(2);
    }
}
