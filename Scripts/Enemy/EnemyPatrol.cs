using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Moverment params")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [SerializeField] private float idleDuration;
    private float idleTimer;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x) { MoveInDirection(-1); }
            else { DirectionChange(); }
        }
        else
        {
            if(enemy.position.x <= rightEdge.position.x) { MoveInDirection(1); }
            else { DirectionChange(); }
        }
    }

    private void OnDisable()
    {
        animator.SetBool("wolk", false);
    }

    private void DirectionChange()
    {
        animator.SetBool("wolk", false);

        idleTimer += Time.deltaTime;
        if(idleTimer > idleDuration)
        movingLeft = !movingLeft;
    }

    private void MoveInDirection(int direction)
    {
        idleTimer = 0;
        animator.SetBool("wolk", true);

        transform.rotation = movingLeft ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.Euler(0f, 0f, 0f);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y,enemy.position.z);
    }
}
