using System.Collections;
using UnityEngine;

public class Enemy_Classic : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool FacingRight = false;

    [SerializeField] private Transform BulletTransform;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float startTimeFire;
    [SerializeField] private float speed;
    private float timeFire;
    private float hitRemeningTime = 1f;
    private bool isPlayerBeInSight = false;

    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float retreateDistance;
    [SerializeField] private float distance;

    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    private Animator animator;
    [SerializeField] Health health;

    private GameObject player;

    [SerializeField] private AudioSource sound;
    [SerializeField] private AudioClip fire;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeFire = startTimeFire;
    }

    private void Update()
    {
        if(health.isDammaged) return;

        if (player.transform.position.x > transform.position.x && !FacingRight)
        {
            Flip();
        }
        else if (player.transform.position.x < transform.position.x && FacingRight)
        {
            Flip();
        }

        if (isPlayerBeInSight)
        {

            if (Vector2.Distance(transform.position, player.transform.position) > range * distance)
            {
                animator.SetBool("wolk", true);
                animator.SetBool("retreate", false);
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.transform.position) < range * distance && (Vector2.Distance(transform.position, player.transform.position)) > retreateDistance)
            {
                transform.position = this.transform.position;
                animator.SetBool("wolk", false);
                animator.SetBool("retreate", false);

            }
            else if ((Vector2.Distance(transform.position, player.transform.position)) < retreateDistance && !FacingRight)
            {
                animator.SetBool("wolk", true);
                animator.SetBool("retreate", true);
                transform.position = new Vector2(transform.position.x - -speed * Time.deltaTime, transform.position.y);
            }
            else if((Vector2.Distance(transform.position, player.transform.position)) < retreateDistance && FacingRight)
            {
                animator.SetBool("wolk", true);
                animator.SetBool("retreate", true);
                transform.position = new Vector2(transform.position.x + -speed * Time.deltaTime, transform.position.y);
            }
        }

        if (PlayerInSight())
        {
            isPlayerBeInSight = true;
            animator.SetBool("isFireing", true);
            if (timeFire <= 0)
            {
                Bullet.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                sound.PlayOneShot(fire);
                Instantiate(Bullet, BulletTransform.position, transform.rotation);

                timeFire = startTimeFire;
            }
            else
            {
                timeFire -= Time.deltaTime;
            }
        }
        else
        {
            animator.SetBool("isFireing", false);
        }

    }

    private bool PlayerInSight()
    {
       RaycastHit2D hit = Physics2D.BoxCast(
       boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
       new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
       0,
       Vector2.left,
       0,
       playerLayer);
        

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private IEnumerator DamageAnim()
    {
        
        if (animator != null)
        {
            animator.SetBool("heart", true);
            yield return new WaitForSeconds(hitRemeningTime);
            animator.SetBool("heart", false);
            health.isDammaged = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (health.isDammaged)
        {
            StartCoroutine(DamageAnim());
        }
    }

    private void Flip()
    {
        
        FacingRight = !FacingRight;

        transform.Rotate(0, 180, 0);
    }
}
