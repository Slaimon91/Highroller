﻿using System.Collections;
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
    private bool sleepTimer = false;

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

        if(battleSystem.state == BattleState.PLAYERTURN)
        {
            CheckHealthAnimation();
            if (!sleepTimer)
            {
                sleepTimer = true;
               // StartCoroutine(SleepyPlayer());
            }
        }
    }

    IEnumerator SleepyPlayer()
    {
        yield return new WaitForSeconds(120f);
        if(animator.GetFloat("IdleState") != 2)
        {
            animator.SetFloat("IdleState", 1);
        }
    }

    public void WakeUpSleep()
    {
        sleepTimer = false;
        StopCoroutine(SleepyPlayer());
        if (animator.GetFloat("IdleState") != 2)
        {
            animator.SetFloat("IdleState", 0);
        }
    }

    public void BlockPushed()
    {
        if (isDead)
            return;
        WakeUpSleep();
        if (battleSystem.state == BattleState.ENEMYTURN && !hasDefended)
        {
            hasDefended = true;
            hasBlocked = true;
            animator.SetTrigger("Block");

            if (isInsideBlock)
            {
                audioManager.Play("SuccessBlock");

                Debug.Log("You blocked!");
                successBlock = true;
            }
            else
            {
                //audioManager.Play("FailBlock");
                Debug.Log("You failed the block!");
            }
        }
    }

    public void DodgePushed()
    {
        if (isDead)
            return;
        WakeUpSleep();
        if (battleSystem.state == BattleState.ENEMYTURN && !hasDefended)
        {
            hasDefended = true;
            hasDodged = true;
            animator.SetTrigger("Dodge");
            if (isInsideDodge)
            {
                Debug.Log("You dodged!");
                successDodge = true;
                audioManager.Play("SuccessDodge");

            }
            else
            {
                //audioManager.Play("FailDodge");
                Debug.Log("You failed the dodge!");
            }
        }
    }

    private void Dead()
    {
        SaveSystem.ResetTemp(playerValues.currentSavefile);
        SavefileDisplayData data = SaveSystem.Load<SavefileDisplayData>("/" + playerValues.currentSavefile + "/" + "SavefileDisplay");
        if (data == default)
        {
            SaveSystem.ResetSavefile(playerValues.currentSavefile);
            FindObjectOfType<LevelLoader>().LoadOverworldSceneFromMenu("OW_FOD");
        }
        else
        {
            FindObjectOfType<LevelLoader>().LoadOverworldSceneFromMenu(data.location);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBattleBase collidingEnemy = null;
            
        //was an enemy
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
        //was a projectile
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
            
            //Destroy(other.gameObject);
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
        //ResetAction();
    }

    private void SuccessDodge()
    {
        StartCoroutine(ResetAction());
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
        if (isDead)
            return;
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

            if (!successBlock)
            {
                animator.SetTrigger("Damage");
                audioManager.Play("TakeDamage");
            }

            CheckHealthAnimation();
        }
        StartCoroutine(ResetAction());
    }

    public void HealDamage(int damage)
    {
        if (isDead)
            return;
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
        if(playerValues.healthPoints <= 0)
        {
            animator.SetBool("isDead", true);
            isDead = true;
        }

        else if((float)playerValues.healthPoints / (float)playerValues.maxHealthPoints <= 0.1)
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

    public IEnumerator ResetAction()
    {
        yield return new WaitForSeconds(0.2f);
        hasDefended = false;
        hasBlocked = false;
        hasDodged = false;
        successBlock = false;
        successDodge = false;
    }

    IEnumerator DamageText(int damage)
    {
        var text = Instantiate(damageText, canvas.transform);
        text.GetComponent<TextMeshProUGUI>().text = "" + damage;

        yield return new WaitForSeconds(1);

        Destroy(text.gameObject);
    }

    IEnumerator HealText(int damage)
    {
        var text = Instantiate(healText, canvas.transform);
        text.GetComponent<TextMeshProUGUI>().text = "" + damage;

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

    public IEnumerator TextRise(GameObject textObject)
    {
        yield return null;
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
}
