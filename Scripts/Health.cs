 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public bool dashImortal;
    public bool ShiealdImortal;

    public bool isDammaged = false;
    public bool isDefited = false;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject gameOver;

    [SerializeField] private AudioSource sound;
    [SerializeField] private AudioClip die;

    public void takeHit(int damage)
    {
        if (dashImortal) { return; }
        if (ShiealdImortal) { return; }
        isDammaged = true;
        health -= damage;
        if (health <= 0)
        {
            if (isDefited) { return; }
            isDefited = true;
            
            if (gameObject.tag == "Player") 
            {
                sound.PlayOneShot(die);
                gameOver.SetActive(true); 
            }
            if(gameObject.tag == "enemy") 
            {
                if (GetComponent<Enemy_Classic>() != null) { GetComponent<Enemy_Classic>().enabled = false; sound.PlayOneShot(die); }
                animator.SetBool("die", true);
            }
            if(GetComponent <Boss>() != null)
            {
                GetComponent <Boss>().enabled = false;
            }
        }
    }

    public void Die() 
    {
        Destroy(gameObject);
    }

    public void SetHealth(int bonusHealth)
    {
        health += bonusHealth;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    
}
