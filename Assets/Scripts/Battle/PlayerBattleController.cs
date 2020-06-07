using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerBattleController : MonoBehaviour
{
    //[SerializeField] int currentHealth;
    //[SerializeField] int maxHealth;
    [SerializeField] TextMeshProUGUI healthText;
    private GameObject healthTextGameObject;
    bool isDead;
    bool hasBlocked = false;
    bool hasDodged = false;
    bool hasDefended = false;
    PlayerControls controls;
    [SerializeField] PlayerValues playerValues;
    public Transform playerHead;
    public Transform playerBody;

    private BattleSystem battleSystem;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.ChangeSceneHax.performed += ctx => LoadSceneHax();
        controls.Gameplay.Dodge.performed += ctx => DodgePushed();
        controls.Gameplay.Block.performed += ctx => BlockPushed();
    }

    void Start()
    {
        healthTextGameObject = GameObject.FindGameObjectWithTag("HealthText");
        healthText = healthTextGameObject.GetComponent<TextMeshProUGUI>();
        battleSystem = FindObjectOfType<BattleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = playerValues.healthPoints.ToString() + " / " + playerValues.maxHealthPoints.ToString();

        if(playerValues.healthPoints <= 0)
        {
            if(isDead)
            {
                return;
            }

            Dead();
        }
    }

    void LoadSceneHax()
    {
        FindObjectOfType<LevelLoader>().LoadOverworldScene();
    }

    void BlockPushed()
    {
        if (battleSystem.state == BattleState.ENEMYTURN && !hasDefended)
        {
            hasDefended = true;
            hasBlocked = true;
        }
    }

    void DodgePushed()
    {
        if (battleSystem.state == BattleState.ENEMYTURN && !hasDefended)
        {
            hasDefended = true;
            hasDodged = true;
        }
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    private void Dead()
    {
        isDead = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hej");
        EnemyBattleBase collidingEnemy = null;
        if((collidingEnemy = other.gameObject.GetComponent<EnemyBattleBase>()) != null)
        {
            TakeDamage(collidingEnemy.GetDamageAmount());
            Debug.Log("You took " + collidingEnemy.GetDamageAmount() + " damage!");
        }
        else if ((collidingEnemy = other.gameObject.GetComponentInParent<EnemyBattleBase>()) != null)
        {
            TakeDamage(collidingEnemy.GetDamageAmount());
            Debug.Log("You took " + collidingEnemy.GetDamageAmount() + " damage!");
            Destroy(collidingEnemy);
        }
        else
        {
            Debug.Log("No damage component found!");
        }
    }

    public void EnteredBlockZone(GameObject other)
    {
        Debug.Log("Entering blockzone");
    }

    public void LeavingBlockZone(GameObject other)
    {
        Debug.Log("Leaving blockzone");
    }

    public void EnteredDodgeZone(GameObject other)
    {
        Debug.Log("Entering dodgezone");
    }

    public void LeavingDodgeZone(GameObject other)
    {
        Debug.Log("Leaving dodgezone");
    }

    public void TakeDamage(int damage)
    {
        playerValues.healthPoints -= damage;
    }

    public void HealDamage(int damage)
    {
        playerValues.healthPoints += damage;
        if(playerValues.healthPoints > playerValues.maxHealthPoints)
        {
            playerValues.healthPoints = playerValues.maxHealthPoints;
        }
    }

    public void EnemyTurnStart()
    {
        hasDefended = false;
        hasBlocked = false;
        hasDodged = false;
    }
}
