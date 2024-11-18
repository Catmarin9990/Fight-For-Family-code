using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponHold : MonoBehaviour
{
    public bool hold = false;
    public float distance = 10f;
    RaycastHit2D hit;
    public Transform holdPoint;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!hold)
            {
                Physics2D.queriesStartInColliders = false;
                hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

                if (hit.collider != null)
                {
                    hold = true;
                }
            }

            if (hold)
            {
                hit.collider.gameObject.transform.position = holdPoint.position;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine (transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}
