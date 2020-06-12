using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerBattleController : MonoBehaviour
{
    private TextMeshProUGUI healthText;
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
    public Transform gaiaPoint;
    public GameObject damageText;
    public GameObject healText;
    private Canvas canvas;

    private BattleSystem battleSystem;
    Animator animator;

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
        animator = GetComponent<Animator>();
        canvas = FindObjectOfType<Canvas>();
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
            animator.SetTrigger("Block");

            if (isInsideBlock && !isInsideDodge)
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
            
        if ((collidingEnemy = other.gameObject.GetComponent<EnemyBattleBase>()) != null)
        {
            if(successBlock)
            {
                SuccessBlock(collidingEnemy);
            }
            else if (successDodge)
            {
                SuccessDodge();
            }
            else
            {
                TakeDamage(collidingEnemy.GetDamageAmount());
                collidingEnemy.CollideWithPlayer();
            }
            
        }
        else if ((collidingEnemy = other.gameObject.GetComponentInParent<EnemyBattleBase>()) != null)
        {
            if (successBlock)
            {
                SuccessBlock(collidingEnemy);
            }
            else if (successDodge)
            {
                SuccessDodge();
            }
            else
            {
                TakeDamage(collidingEnemy.GetDamageAmount());
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

    private void SuccessBlock(EnemyBattleBase enemy)
    {
        int reduction = 1;

        foreach (AbilityBase ability in battleSystem.battleAbilites)
        {
            reduction += ability.Block();
        }

        TakeDamage(enemy.GetDamageAmount() - reduction);
    }

    private void SuccessDodge()
    {

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
        int reduction = 0;
        int damageToTake = 0;

        foreach(AbilityBase ability in battleSystem.battleAbilites)
        {
            reduction += ability.TakeDamage();
        }

        damageToTake = damage - reduction;
        if (damageToTake < 0)
        {
            damageToTake = 0;
        }

        playerValues.healthPoints -= damageToTake;
        StartCoroutine(DamageText(damage));
        EnemyTurnStart();
        Debug.Log("You took " + (damageToTake) + " damage!");
    }

    public void HealDamage(int damage)
    {
        playerValues.healthPoints += damage;
        StartCoroutine(HealText(damage));
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

    IEnumerator DamageText(int damage)
    {
        var text = Instantiate(damageText, canvas.transform);
        text.GetComponent<TextMeshProUGUI>().text = "-" + damage;

        yield return new WaitForSeconds(1);

        Destroy(text.gameObject);
    }

    IEnumerator HealText(int damage)
    {
        var text = Instantiate(healText, canvas.transform);
        text.GetComponent<TextMeshProUGUI>().text = "+ " + damage;

        yield return new WaitForSeconds(1);

        Destroy(text.gameObject);
    }
}
