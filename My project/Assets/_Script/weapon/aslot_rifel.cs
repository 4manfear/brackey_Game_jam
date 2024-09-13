using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aslot_rifel : MonoBehaviour
{
    [SerializeField]
    private Camera maincam;

    [SerializeField]
    private GameObject bullet_Prefab;

    [SerializeField]
    private Transform muzzel;

    [SerializeField]
    private Transform rifle;

    [SerializeField]
    private float bullet_Speed;

    [SerializeField]
    private float recoil_Force = 1f;

    public bool isFiring;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        // Store the original position and rotation of the rifle
        originalPosition = rifle.localPosition;
        originalRotation = rifle.localRotation;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            isFiring = true;
            pistolshotting();
        }
        else
        {
            isFiring = false;
        }

        // Return the rifle to its original position when not firing
        if (!isFiring)
        {
            rifle.localPosition = Vector3.Lerp(rifle.localPosition, originalPosition, recoil_Force * Time.deltaTime);
            rifle.localRotation = Quaternion.Lerp(rifle.localRotation, originalRotation, recoil_Force * Time.deltaTime);
        }
    }

    void pistolshotting()
    {
        RaycastHit hit;
        Vector3 screensenter = new Vector3(Screen.width / 2f, Screen.height / 2f);
        Ray ray = maincam.ScreenPointToRay(screensenter);

        // Draw the ray in the Scene view for debugging
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            GameObject bulletshooting = Instantiate(bullet_Prefab, muzzel.position, Quaternion.identity);
            Rigidbody bulletrb = bulletshooting.GetComponent<Rigidbody>();

            // Calculate direction from the muzzle to the hit point
            Vector3 direction = (hit.point - muzzel.position).normalized;

            bulletrb.AddForce(direction * bullet_Speed, ForceMode.Impulse);

            // Apply recoil
            ApplyRecoil();
        }
    }

    void ApplyRecoil()
    {
        // Apply recoil force to the rifle
        rifle.localPosition -= rifle.forward * recoil_Force * Time.deltaTime;
        rifle.localRotation *= Quaternion.EulerRotation(-recoil_Force * Time.deltaTime, 0, 0);
    }
}

