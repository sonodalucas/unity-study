using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : TakeDamage
{
    [SerializeField] private float maxMana;
    [SerializeField] private Slider manaBar;
    [SerializeField] private float timeToRecoverMana;
    [SerializeField] private float manaRecoverNumber;
    [SerializeField] private TextMeshProUGUI coinText;

    private float mana;
    private float timer;
    private int coins;
    
    private void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
        mana = maxMana;
        manaBar.maxValue = maxMana;
        manaBar.value = mana;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToRecoverMana)
        {
            timer = 0;
            mana += manaRecoverNumber;
            if (mana > maxMana)
            {
                mana = maxMana;
            }

            manaBar.value = mana;
        }
    }

    public void SpendMana(float value)
    {
        mana -= value;
        manaBar.value = mana;
    }

    public bool HasEnoughMana(float value)
    {
        return mana >= value;
    }

    public void IncrementCoins(int value)
    {
        coins += value;
        coinText.text = coins.ToString();
    }
}
