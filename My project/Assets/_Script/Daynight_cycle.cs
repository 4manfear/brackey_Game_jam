using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Daynight_cycle : MonoBehaviour
{
    
    public bool isDay;
    public Light directionalLight; // Ensure this is set to a Directional Light

    public float rotationSpeed = 10f; // Speed at which the sun rotates

    private void Update()
    {
        if (isDay)
        {
            // Rotate the sun to simulate it being overhead
            // Rotate the sun upward
            Quaternion targetRotation = Quaternion.Euler(90, 0, 0); // 90 degrees on X-axis for overhead
            directionalLight.transform.rotation = Quaternion.RotateTowards(directionalLight.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Rotate the sun to simulate it being below the horizon
            // Rotate the sun downward
            Quaternion targetRotation = Quaternion.Euler(-90, 0, 0); // -90 degrees on X-axis for below horizon
            directionalLight.transform.rotation = Quaternion.RotateTowards(directionalLight.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}