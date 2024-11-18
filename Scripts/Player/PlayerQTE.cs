using System.Collections;
using UnityEngine;

public class PlayerQTE : MonoBehaviour
{
    [SerializeField] private AnimationTrigger trigger;
    [SerializeField] private GameOver restart;
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            trigger.ispressed = true;
            trigger.key = 'x';
            StartCoroutine(IsPressed());
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            trigger.ispressed = true;
            trigger.key = 'l';
            StartCoroutine(IsPressed());
        }
        if (health.isDefited && Input.GetKeyDown(KeyCode.R))
        {
            restart.FadeToGameOver();
        }
    }
    private IEnumerator IsPressed()
    {
        yield return new WaitForSeconds(5f);
        trigger.ispressed = false;
    }
}
