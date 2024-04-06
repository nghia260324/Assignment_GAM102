using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static Health instance;
    public Image healthBar;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateHealth(int currentHealth,int maxHealth)
    {
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }
}
