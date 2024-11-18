using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionHeal : MonoBehaviour
{
    public int healColision = 10;
    public string healTag;

    private void OnCollisionEnter2D(Collision2D coll)
    {
        Health health = coll.gameObject.GetComponent<Health>();
        health.SetHealth(healColision);
        Destroy(gameObject);
    }
}
