using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Guns : MonoBehaviour
{
    public GameObject bullet;
    public Transform BulletTransform;
    [Space]

    public AudioClip din;
    public AudioSource audio;

    [Header("guns fire rate")]
    public float startTimeFire;
    private float TimeFire;
    public bool isSingle = false;
    public bool isInfinity = false;

    public static int addAmmo = 25;
    public int magazine = 30;
    private int cMagazine;
    public static int maxAmmo = 30;
    public float realodingTime = 1f;
    public bool isRealoding = false;
    private bool canRealoading = true;

    [SerializeField]
    private Text ammoCount;

    public Animator animator;

    void Start()
    {
        if (isSingle)
        {
            addAmmo = 15;
            magazine = 12;
        }
        TimeFire = startTimeFire;
        cMagazine = magazine;
    }


    void Update()
    {
        if (isRealoding)
        {
            return;
        }


        if (Input.GetKey(KeyCode.Mouse0) && !isSingle && magazine > 0)
        {
            if (TimeFire <= 0)
            {
                audio.PlayOneShot(din);
                Instantiate(bullet, BulletTransform.position, transform.rotation);
                
                TimeFire = startTimeFire;
                --magazine;
            }
            else
            {
                TimeFire -= Time.deltaTime;
               
            }
        }
        else if(Input.GetKeyDown(KeyCode.Mouse0) && isSingle && magazine > 0)
        {
            if (TimeFire <= 0)
            {
                audio.PlayOneShot(din);
                Instantiate(bullet, BulletTransform.position, transform.rotation);
                
                TimeFire = startTimeFire;
                --magazine;

            }
            else
            {
                TimeFire -= Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && maxAmmo > 0 && magazine != cMagazine || magazine == 0 && maxAmmo > 0 && magazine != cMagazine)
        {
            StartCoroutine(Reload());
        }
        ammoCount.text = (isInfinity) ? magazine + " / " + "Infinity" : magazine + " / " + maxAmmo;

    }
    
    private IEnumerator Reload()
    {

        animator.SetBool("IsReloading", true);
        canRealoading = false;
        isRealoding = true;
                        
        if (!isInfinity && !canRealoading)
        {
            int reason = cMagazine - magazine;
            if (maxAmmo > reason)
            {
                maxAmmo -= reason;
                magazine = cMagazine;
            }
            else
            {
                magazine += maxAmmo;
                maxAmmo = 0;
            }
        }
        else if(isInfinity && !canRealoading) { magazine = cMagazine; }
        yield return new WaitForSeconds(realodingTime);
        animator.SetBool("IsReloading", false);
        isRealoding = false;
        canRealoading = true;
    }
    

}
