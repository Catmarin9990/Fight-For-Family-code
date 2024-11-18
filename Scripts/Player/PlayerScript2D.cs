using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerScript2D : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Player Movement Settings")]
    [SerializeField] private VectorValue pos;
    [Range(0, 10f)] public float speed = 1f;
    [Range(0, 35f)] public float jumpForce = 8f;
    private float HorizonalMove = 0f;
    private bool FacingRight = true;

    [Space]
    [Header("Ground Checker Settings")]
    public bool isGrounded = false;
    [SerializeField] UnityEngine.Transform foots;
    [Range(-5, 5f)] public float checkGroundRadius = 0.3f;
    [SerializeField] LayerMask whatIsGround;


    [Space]
    [Header("dashing settings")]
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;    
    private bool canDash = true;
    public bool isDashing = false;

    [Header("Player Animation Settings")]
    public Animator animator;

    [Space]
    [Header("Player Skills Settings")]
    [SerializeField] Skills skills;
    public Health playerHealth;

    [Space]
    public GameObject magazineObject;
    public GameObject arMagazineObject;
    public Transform MagazineTransform;
    public Transform ARMagazineTransform;
    [SerializeField] private SwitchWeapon switchWeapon;
    [SerializeField] private GameOver restart;
    [SerializeField] private Guns ar;
    [SerializeField] private Guns pistol;



    [Space]
    [Header("Audio")]
    public AudioClip Beam;
    public AudioClip jump;
    public AudioSource audio;


    void Start()
    {
        transform.position = pos.initialValue;
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (isDashing) { return; }
        if (skills.useingBeam) { transform.position = this.transform.position; return; }

        if (isGrounded == false) { animator.SetBool("Jumping", true); }
        else { animator.SetBool("Jumping", false); }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space) || isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            audio.PlayOneShot(jump);
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
        HorizonalMove = Input.GetAxisRaw("Horizontal") * speed;

        animator.SetFloat("HorisontalMove", Mathf.Abs(HorizonalMove));

        if (HorizonalMove < 0 && FacingRight) { Flip(); }
        else if (HorizonalMove > 0 && !FacingRight) { Flip(); }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) { StartCoroutine(Dash()); }

        if (Input.GetKeyDown(KeyCode.F)) { StartCoroutine(skills.HealSkill()); }
        if (Input.GetKeyDown(KeyCode.Q)) { StartCoroutine(skills.ShieldSkill()); }
        if (Input.GetKeyDown(KeyCode.Z) && !ar.isRealoding && !pistol.isRealoding) { HorizonalMove = 0; StartCoroutine(skills.BeamSkill()); }
        if (Input.GetKeyDown(KeyCode.R) && playerHealth.isDefited) { restart.FadeToGameOver(); }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Vector3 position = new Vector3(-104f, -6.02f, 0f);
            pos.initialValue = position;
            SceneManager.LoadScene(0);
        }
    }
    Vector2 targetVelocity;

	private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        targetVelocity = new Vector2(HorizonalMove * 10f, rb.velocity.y);
        rb.velocity = targetVelocity;

        isGrounded = Physics2D.OverlapCircle(foots.position, checkGroundRadius, whatIsGround);
    }

    public void Flip()
    {
        FacingRight = !FacingRight;

        transform.Rotate(0, 180, 0);
    }

    private IEnumerator Dash()
    {
        playerHealth.dashImortal = true;
        canDash = false;
        isDashing = true;
        animator.SetBool("IsDashing", true);
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if (FacingRight)
        {
            rb.velocity = new Vector2(1 * dashingPower, 0f);
        }
        else rb.velocity = new Vector2(-1 * dashingPower, 0f);

        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("IsDashing", false);
        playerHealth.dashImortal = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    

    private void PistolCreatMagazine()
    {
        if (switchWeapon.weaponSwitch == 1)
            Instantiate(magazineObject, MagazineTransform.position, transform.rotation);
    }
    private void ARCreatMagazine()
    {
        if (switchWeapon.weaponSwitch == 2)
            Instantiate(arMagazineObject, ARMagazineTransform.position, Quaternion.Euler(0,0,-38f));
    }
    public void BeamSound()
    {
        audio.PlayOneShot(Beam);
    }
}
