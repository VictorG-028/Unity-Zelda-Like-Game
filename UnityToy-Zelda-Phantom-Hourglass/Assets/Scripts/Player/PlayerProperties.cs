using System.Collections;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    [SerializeField] GameState gameState = null;
    [SerializeField] HeartContainersController updateHeartContainers = null;

    private void OnValidate()
    {
        if (!gameState) { gameState = GameObject.Find("GameCore").GetComponent<GameState>(); }
        if (!updateHeartContainers) { updateHeartContainers = GameObject.Find("Canvas Manager").GetComponent<HeartContainersController>(); }
    }

    // Actions
    public bool canJump = false;
    public bool canMove = true;
    public bool canAttack = true;
    public bool canSwitchHold = true;
    public bool canPause = true;

    // Special properties
    public bool iFrame = false;
    public string usingName = "Basic Attack";

    // Stats
    public int level = 1;
    public float xpNeeded = 0;
    public float previousXpNeeded = 0;
    public int xp = 0;
    public int HP = 4;
    public int maxHP = 4;
    public int mana = 0;

    // Bag of itens
    public int rupees = 0;
    public int bombs = 0;
    public int arrows = 0;

    public Vector3 LocalDirection { get; internal set; }

    // Take Damage Logic ////////////////////////////////////////////////
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && !iFrame)
        {
            HP -= 1;
            updateHeartContainers.UpdatePlayerUI();
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

    // Level Up System ////////////////////////////////////////////////
    public void ReciveXP(int xp)
    {
        this.xp += xp;

        float xpNeeded = XpNeedToLevelUp(level);
        float previousXpNeeded = XpNeedToLevelUp(level -1);

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

    public static int XpNeedToLevelUp(int currentLevel)
    {
        if (currentLevel == 0) { return 0; }

        return (currentLevel * currentLevel + currentLevel) * 5; // 0 10 30 60
    }

    public void LevelUp()
    {
        level += 1;
    }

    // Utils ////////////////////////////////////////////////
    public string GetPropAmmount(string propName)
    {
        switch (propName)
        {
            case "SlashEffect":
            case "Basic Attack":
                return "";
            case "HP":
                return HP.ToString();
            case "Bombs":
                return bombs.ToString();
            case "Arrows":
                return arrows.ToString();
            case "Mana":
                return mana.ToString();
            case "Rupees":
                return rupees.ToString();
            default:
                Debug.LogError($"Propriedade {propName} não reconhecida!");
                return "Error";
        }
    }

    public void IncrementPropByName(string propName)
    {
        switch (propName)
        {
            case "HP":
                HP++;
                break;
            case "Bombs":
                bombs++;
                break;
            case "Arrows":
                arrows++;
                break;
            case "Mana":
                mana++;
                break;
            case "Rupees":
                rupees++;
                break;
            default:
                Debug.LogError($"Propriedade {propName} não reconhecida!");
                break;
        }
    }
}
