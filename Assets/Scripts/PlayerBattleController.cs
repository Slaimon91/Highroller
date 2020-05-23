using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;

public class PlayerBattleController : MonoBehaviour
{
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;
    [SerializeField] TextMeshProUGUI healthText;
    private GameObject healthTextGameObject;
    bool isDead;

    void Start()
    {
        healthTextGameObject = GameObject.FindGameObjectWithTag("HealthText");
        healthText = healthTextGameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Change Scene Hax"))
        {
            FindObjectOfType<LevelLoader>().LoadOverworldScene();
        }

        healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();

        if(currentHealth <= 0)
        {
            if(isDead)
            {
                return;
            }

            Dead();
        }
    }

    private void Dead()
    {
        isDead = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void HealDamage(int damage)
    {
        currentHealth += damage;
        if(currentHealth>maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
