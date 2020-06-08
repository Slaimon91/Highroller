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
    bool isInsideBlock = false;
    bool isInsideDodge = false;
    bool successBlock = false;
    bool successDodge = false;
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
        healthText.text = playerValues.healthPoints.ToString() + "/" + playerValues.maxHealthPoints.ToString();

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

            if (isInsideBlock)
            {
                Debug.Log("You blocked!");
                successBlock = true;
            }
            else
            {
                Debug.Log("You failed the block!");
            }
        }
    }

    void DodgePushed()
    {
        if (battleSystem.state == BattleState.ENEMYTURN && !hasDefended)
        {
            hasDefended = true;
            hasDodged = true;
            if(isInsideDodge)
            {
                Debug.Log("You dodged!");
                successDodge = true;
            }
            else
            {
                Debug.Log("You failed the dodge!");
            }
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
        EnemyBattleBase collidingEnemy = null;
        if((collidingEnemy = other.gameObject.GetComponent<EnemyBattleBase>()) != null)
        {
            if(successBlock)
            {
                TakeDamage(collidingEnemy.GetDamageAmount() - 1);
                Debug.Log("You took " + (collidingEnemy.GetDamageAmount() - 1) + " damage!");
            }
            else if (successDodge)
            {

            }
            else
            {
                TakeDamage(collidingEnemy.GetDamageAmount());
                Debug.Log("You took " + collidingEnemy.GetDamageAmount() + " damage123!");
            }
            
        }
        else if ((collidingEnemy = other.gameObject.GetComponentInParent<EnemyBattleBase>()) != null)
        {
            if (successBlock)
            {
                TakeDamage(collidingEnemy.GetDamageAmount() - 1);
                Debug.Log("You took " + (collidingEnemy.GetDamageAmount() - 1) + " damage!");
            }
            else if (successDodge)
            {

            }
            else
            {
                TakeDamage(collidingEnemy.GetDamageAmount());
                Debug.Log("You took " + collidingEnemy.GetDamageAmount() + " damage!");
            }
            
            Destroy(other.gameObject);
        }
        else
        {
            Debug.Log("No damage component found!");
        }
        isInsideBlock = false;
        isInsideDodge = false;
    }

    public void EnteredBlockZone(GameObject other)
    {
        //Debug.Log("Entering blockzone");
        isInsideBlock = true;
    }

    public void LeavingBlockZone(GameObject other)
    {
        
    }

    public void EnteredDodgeZone(GameObject other)
    {
        //Debug.Log("Entering dodgezone");
        isInsideDodge = true;
    }

    public void LeavingDodgeZone(GameObject other)
    {
        
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
        successBlock = false;
        successDodge = false;
    }
}
