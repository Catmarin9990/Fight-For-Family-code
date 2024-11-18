using UnityEngine;

public class ColisionDamage : MonoBehaviour
{
    public int colisionDamage = 10;
    public string collisionTag;
    

    private void OnTriggerEnter2D(Collider2D coll)
    {
        Health health = coll.gameObject.GetComponent<Health>();
        if (coll.gameObject.tag == collisionTag && gameObject.tag == "bullet")
        {
            health.takeHit(colisionDamage);
            Destroy(gameObject);
        }
        else if(coll.gameObject.tag == collisionTag)
        {
            health.takeHit(colisionDamage);
        }
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
