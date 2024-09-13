using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_Switcher : MonoBehaviour
{
    [SerializeField]
    private GameObject weapon1, weapon2, weapon3, hand;

    private GameObject activeWeapon;  // Tracks the currently active weapon

    private void Start()
    {
        // Set the default weapon (hand) at the beginning
        SetActiveWeapon(hand);
    }

    private void Update()
    {
        // Switch weapons using keys 1, 2, 3, and 4
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveWeapon(weapon1);  // Select weapon 1
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveWeapon(weapon2);  // Select weapon 2
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveWeapon(weapon3);  // Select weapon 3
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetActiveWeapon(hand);  // Select hand (unarmed)
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
