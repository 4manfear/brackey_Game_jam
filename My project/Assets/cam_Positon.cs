using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_Positon : MonoBehaviour
{
    public Transform cam_head;  // The target position and rotation (e.g., the player's head or another object)

    private void Update()
    {
        // Set the position of the camera to match the cam_head's position
        this.transform.position = cam_head.position;

        this.transform.rotation = Quaternion.LookRotation(cam_head.forward);


    }
}
