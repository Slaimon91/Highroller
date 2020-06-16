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
    private bool isDead;
    private bool hasBlocked = false;
    private bool hasDodged = false;
    private bool hasDefended = false;
    private bool isInsideBlock = false;
    private bool isInsideDodge = false;
    private bool successBlock = false;
    private bool successDodge = false;
    private bool nearDeath = false;
    private int damageTaken = 0;
    private int damageThreshold = 0;
    private bool undershot = false;

    PlayerControls controls;
    [SerializeField] PlayerValues playerValues;
    public Transform playerHead;
    public Transform playerBody;
    public Transform gaiaPoint;
    [SerializeField] private GameObject damageText;
    [SerializeField] private GameObject healText;
    private Canvas canvas;
    [SerializeField] GameObject dodgeCollider;
    [SerializeField] GameObject blockCollider;
    [SerializeField] GameObject easyBlockCollider;

    private BattleSystem battleSystem;
    private Animator animator;
    private AudioManager audioManager;

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
        audioManager = FindObjectOfType<AudioManager>();
        CheckHealthAnimation();
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
                audioManager.Play("SuccessBlock");
                animator.SetTrigger("Block");
                Debug.Log("You blocked!");
                successBlock = true;
            }
            else
            {
                audioManager.Play("FailBlock");
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
            if (isInsideDodge)
            {
                Debug.Log("You dodged!");
                successDodge = true;
                audioManager.Play("SuccessDodge");
                animator.SetTrigger("Dodge");
            }
            else
            {
                audioManager.Play("FailDodge");
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
        ResetAction();
    }

    private void SuccessDodge()
    {
        ResetAction();
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

        if(damageToTake > damageThreshold && damageThreshold != 0)
        {
            damageToTake = damageThreshold;
        }

        if (damageToTake >= playerValues.healthPoints && undershot)
        {
            undershot = false;
            damageToTake = playerValues.healthPoints - 1;
            FindObjectOfType<Undershot>().SetActiveTurn();
        }

        if (damageToTake >= 1)
        {
            playerValues.healthPoints -= damageToTake;
            damageTaken += damageToTake;
            StartCoroutine(DamageText(damageToTake));

            audioManager.Play("TakeDamage");
            Debug.Log("You took " + (damageToTake) + " damage!");

            if (!successBlock)
            {
                animator.SetTrigger("Damage");
            }
            ResetAction();
            CheckHealthAnimation();
        }
    }

    public void HealDamage(int damage)
    {
        playerValues.healthPoints += damage;
        StartCoroutine(HealText(damage));
        audioManager.Play("HealHP");
        animator.SetTrigger("Heal");
        if (playerValues.healthPoints > playerValues.maxHealthPoints)
        {
            playerValues.healthPoints = playerValues.maxHealthPoints;
        }
        CheckHealthAnimation();
    }

    private void CheckHealthAnimation()
    {
        if((float)playerValues.healthPoints / (float)playerValues.maxHealthPoints <= 0.1)
        {
            nearDeath = true;
            animator.SetFloat("IdleState", 2);
        }
        else
        {
            if(nearDeath)
            {
                nearDeath = false;
                animator.SetFloat("IdleState", 0);
            }
        }
    }

    public void ResetAction()
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

    public IEnumerator MissText(string missText)
    {
        var text = Instantiate(damageText, canvas.transform);
        text.GetComponent<TextMeshProUGUI>().text = missText;

        yield return new WaitForSeconds(1);

        Destroy(text.gameObject);
    }

    public void EnableEasyBlocking()
    {
        blockCollider.SetActive(false);
        easyBlockCollider.SetActive(true);
    }

    public int GetDamageTaken()
    {
        return damageTaken;
    }

    public void SetDamageThreshold(int newThreshold)
    {
        damageThreshold = newThreshold;
    }

    public void SetUndershot(bool status)
    {
        undershot = status;
    }

    public void AbsorbGaia()
    {
        animator.SetTrigger("AbsorbGaia");
    }
}
