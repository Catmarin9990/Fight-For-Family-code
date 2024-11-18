using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject fireBall;
    [SerializeField] Transform fireBallPos;
    [SerializeField] GameObject bigFireBall;
    [SerializeField] Transform bigFireBallPos;
    [SerializeField] GameObject teleportEffect;
    [SerializeField] Transform teleportPos;
    [SerializeField] GameObject fire;
    [SerializeField] Transform firePos;
    private bool canUse = true;
    private bool canFire = true;

    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] Transform[] teleportPositions;
    private Animator Anim;
    private Health health;

    private Transform player;

    [SerializeField] AudioSource bossAudio;
    [SerializeField] AudioClip teleportSound;
    [SerializeField] AudioClip fireBallSound;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Anim = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    private void Update()
    {
        if (canUse && PlayerInSight()) { Anim.SetBool("fire", true); }
        else if(!PlayerInSight() && canUse)
        {
            transform.Rotate(0, 180, 0);
            Anim.SetBool("fire", true);
        }
        if (health.health <= 500 && canFire) { StartCoroutine(fireSkill()); }
    }

    public IEnumerator initialize()
    {
        canUse = false;
        yield return new WaitForSeconds(0.25f);
        bossAudio.PlayOneShot(fireBallSound);
        Instantiate(fireBall, fireBallPos.position, transform.rotation);
        yield return new WaitForSeconds(0.25f);
        bossAudio.PlayOneShot(fireBallSound);
        Instantiate(fireBall, fireBallPos.position, transform.rotation);
        yield return new WaitForSeconds(0.25f);
        Anim.SetBool("fire", false);
    }

    private IEnumerator teleport()
    {
        if (health.health <= 1000) { StartCoroutine(BigFireBallSkill()); }
        int rnd = Random.Range(0, teleportPositions.Length);
        Vector3 pos = new (teleportPositions[rnd].position.x, teleportPos.position.y, 0);
        bossAudio.PlayOneShot(teleportSound);
        Instantiate(teleportEffect, pos , transform.rotation);
        yield return new WaitForSeconds(0.5f);
        transform.position = teleportPositions[rnd].position;
        canUse = true;
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

    public IEnumerator BigFireBallSkill()
    {
        Vector3 pos = new(player.transform.position.x, bigFireBallPos.position.y, 0f);
        Instantiate(bigFireBall, pos, bigFireBall.transform.rotation);
        yield return new WaitForSeconds(1f);
        pos = new(player.transform.position.x, bigFireBallPos.position.y, 0f);
        Instantiate(bigFireBall, pos, bigFireBall.transform.rotation);
    }

    private IEnumerator fireSkill()
    {
        canFire = false;
        Vector3 pos = new(player.transform.position.x, firePos.position.y, 0f);
        Instantiate(fire, pos, fire.transform.rotation);
        yield return new WaitForSeconds(4f);
        canFire = true;
    }
}
