using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_Switcher : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon1, weapon2, weapon3, hand;

    
    public Animator anim;

    public bool pistol, rifel;

    private GameObject activeWeapon;  // Tracks the currently active weapon

    private void Start()
    {
        // Set the default weapon (hand) at the beginning
        SetActiveWeapon(hand);
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Switch weapons using keys 1, 2, 3, and 4
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveWeapon(weapon1);  // Select weapon 1
            anim.SetBool("pistol", true);  // Set pistol animation
            anim.SetBool("Rifel", false);  // Disable Rifle animation
            pistol = true;
            rifel = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveWeapon(weapon2);  // Select weapon 2
            anim.SetBool("pistol", false);  // Disable pistol animation
            anim.SetBool("Rifel", true);    // Set Rifle animation
            pistol = false;
            rifel = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveWeapon(weapon3);  // Select weapon 3
            anim.SetBool("pistol", false);  // Disable pistol animation
            anim.SetBool("Rifel", false);   // Disable Rifle animation
            pistol = false;
            rifel = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetActiveWeapon(hand);  // Select hand (unarmed)
            anim.SetBool("pistol", false);  // Disable pistol animation
            anim.SetBool("Rifel", false);   // Disable Rifle animation
            pistol = false;
            rifel = false;
        }
    }

    // This function sets the currently active weapon and disables the others
    private void SetActiveWeapon(GameObject selectedWeapon)
    {
        // Deactivate all weapons first
        weapon1.SetActive(false);
        weapon2.SetActive(false);
        weapon3.SetActive(false);
        hand.SetActive(false);

        // Activate the selected weapon
        selectedWeapon.SetActive(true);

        // Update the active weapon reference
        activeWeapon = selectedWeapon;
    }
}
