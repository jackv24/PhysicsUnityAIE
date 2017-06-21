using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public int currentHealth = 100;
    public int maxHealth = 100;

    [Space()]
    public Slider healthSlider;

    void Update()
    {
        float value = (float)currentHealth / maxHealth;

        if (healthSlider && healthSlider.value != value)
        {
            healthSlider.value = value;
        }
    }

    public void RemoveHealth(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            currentHealth = 0;

            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
