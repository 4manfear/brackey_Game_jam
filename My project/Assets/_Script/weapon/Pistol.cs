using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField]
    private Camera maincam;

    [SerializeField]
    private GameObject bullet_Prefab;

    [SerializeField]
    private Transform muzzel;

    [SerializeField]
    private float bullet_Speed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            pistolshotting();
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
        }
    }
}
