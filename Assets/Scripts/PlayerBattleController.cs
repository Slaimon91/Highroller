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
    PlayerControls controls;
    [SerializeField] PlayerValues playerValues;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.ChangeSceneHax.performed += ctx => LoadSceneHax();
    }

    void Start()
    {
        healthTextGameObject = GameObject.FindGameObjectWithTag("HealthText");
        healthText = healthTextGameObject.GetComponent<TextMeshProUGUI>();
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
}
