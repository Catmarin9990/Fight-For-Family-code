using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Slider slider;
    public Health playerHealth;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    private void Start()
    {
        SetMaxHealth(playerHealth.health);
    }

    private void Update()
    {
        SetHealth(playerHealth.health);
    }
}
