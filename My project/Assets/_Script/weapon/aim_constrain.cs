using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class aim_constrain : MonoBehaviour
{
    public MultiAimConstraint MAC;

    public Transform aimtracker;

    public float distanceofaimtracker;


    private void Update()
    {
        Vector3 screensenter = new Vector3(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screensenter);

        // Set the aimtracker position to a point 10 units along the ray
        float distanceFromCamera = distanceofaimtracker; // Adjust this value as needed
        aimtracker.position = ray.GetPoint(distanceFromCamera);

        Debug.DrawRay(ray.origin, ray.direction * distanceFromCamera, Color.red, 0.01f);
    }


}
