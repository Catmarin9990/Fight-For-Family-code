using System.Collections;
using UnityEngine;

public class Skills : MonoBehaviour
{
    [Header("HP settings")]
    [SerializeField] private PlayerScript2D player;
    [SerializeField] private Health playerHealth;
    [SerializeField] private float healCoolDown = 10f;
    private bool canHeal = true;
    [SerializeField] private int recovery = 25;
    [Space]
    [Header("Shield settings")]
    private bool canActivateShild = true;
    [SerializeField] float shildCooldown = 8f;
    [SerializeField] private float shildActiveTime = 5f;

    [Space]
    [Header("Beam settings")]
    private bool canUseBeam = true;
    public bool useingBeam = false;
    [SerializeField] private float beamUsingTime;
    [SerializeField] private float beamColldown;
    [Space]
    [Header("Audio settings")]
    [SerializeField] private AudioClip healSound;
    [SerializeField] private AudioClip shieldActivationSound;
    [SerializeField] private AudioSource audio;
    [Space]
    [Header("Animation settings")]
    [SerializeField] private Animator ShieldAnim;
    [SerializeField] private Animator HealAnim;
    [SerializeField] private Animator BeamAnim;

    public IEnumerator HealSkill()
    {
        if (canHeal)
        {
            canHeal = false;
            HealAnim.SetTrigger("Heal");
            audio.PlayOneShot(healSound);
            playerHealth.SetHealth(recovery);
            yield return new WaitForSeconds(healCoolDown);
            canHeal = true;
        }

    }

    public IEnumerator ShieldSkill()
    {
        if (canActivateShild)
        {
            playerHealth.ShiealdImortal = true;
            canActivateShild = false;
            ShieldAnim.SetBool("Shield", true);
            audio.PlayOneShot(shieldActivationSound);
            yield return new WaitForSeconds(shildActiveTime);
            playerHealth.ShiealdImortal = false;
            ShieldAnim.SetBool("Shield", false);
            yield return new WaitForSeconds(shildCooldown);
            canActivateShild = true;
        }
       
    }

    public IEnumerator BeamSkill()
    {
        if (canUseBeam)
        {
            playerHealth.dashImortal = true;
            canUseBeam = false;
            useingBeam = true;
            BeamAnim.SetBool("useBeam", true);
            yield return new WaitForSeconds(beamUsingTime);
            BeamAnim.SetBool("useBeam", false);
            useingBeam = false;
            playerHealth.dashImortal = false;
            yield return new WaitForSeconds(beamColldown);
            canUseBeam = true;
        }
        
    }

}
