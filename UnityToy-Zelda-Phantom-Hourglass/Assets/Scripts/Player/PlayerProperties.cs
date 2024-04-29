using System.Collections;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    [SerializeField] GameState gameState = null;

    private void OnValidate()
    {
        if (!gameState) { gameState = GameObject.Find("GameCore").GetComponent<GameState>(); }
    }

    // Actions
    public bool canJump = false;
    public bool canMove = true;
    public bool canAttack = true;

    // Special properties
    public bool iFrame = false;

    // Stats
    public int level = 1;
    public float xpNeeded = 0;
    public float previousXpNeeded = 0;
    public int xp = 0;
    public int HP = 4;
    public int maxHP = 4;
    public int mana = 0;

    // Bag of itens
    public int bombs = 0;
    public int arrows = 0;

    public Vector3 LocalDirection { get; internal set; }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && !iFrame)
        {
            HP -= 1;
            Debug.Log($"O inimigo atacou o jogador! {HP}/{maxHP}");
            iFrame = true;

            if (HP == 0)
            {
                gameState.Restart();
            }

            StartCoroutine(DisableIFrameAfterDelay(2.0f)); // Desativa iFrame após 2 segundos
        }
    }

    private IEnumerator DisableIFrameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        iFrame = false;
    }

    public void ReciveXP(int xp)
    {
        this.xp += xp;

        float xpNeeded = xpNeedToLevelUp(level);
        float previousXpNeeded = xpNeedToLevelUp(level -1);

        if (this.xp >= xpNeeded)
        {
            Debug.Log("Player subiu de level");
            LevelUp();
            this.xpNeeded = xpNeeded;
            this.previousXpNeeded = previousXpNeeded;
        } else
        {
            Debug.Log($"{this.xp}/{xpNeeded}");
        }

        //float fillAmmount = (xp - previousXpNeeded) / (xpNeeded - previousXpNeeded);

        //if (fillAmmount == 0)
        //{
        //    fillAmmount = 0;
        //}
    }

    public static int xpNeedToLevelUp(int currentLevel)
    {
        if (currentLevel == 0) { return 0; }

        return (currentLevel * currentLevel + currentLevel) * 5; // 0 10 30 60
    }

    public void LevelUp()
    {
        level += 1;
    }
}
