using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    public int weaponSwitch = 0;
    private int cWeaponSwitch;
    [SerializeField] Guns pistol;
    [SerializeField] Guns AR;
    [SerializeField]  private Skills skills;


    void Start()
    {
        SelectWeapon();
    }


    void Update()
    {
        if (skills.useingBeam) 
        { 
            cWeaponSwitch = weaponSwitch; 
            weaponSwitch = 0; 
            SelectWeapon(); 
            return; 
        }
        if (pistol.isRealoding && weaponSwitch == 1 || AR.isRealoding && weaponSwitch == 2) { return; }

        int ChoosedWeapon = weaponSwitch;
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponSwitch = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            weaponSwitch = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponSwitch = 2;
        }

        if (ChoosedWeapon != weaponSwitch)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == weaponSwitch) 
                weapon.gameObject.SetActive(true);
            else 
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
