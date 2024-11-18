using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 0f;
    public int destroy;

    public int rotateUp;
    public int rotatedown;


    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Invoke("DestroyBullet", destroy);
        transform.Rotate(transform.rotation.x, transform.rotation.y, Random.Range(rotatedown, rotateUp));
    }
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
