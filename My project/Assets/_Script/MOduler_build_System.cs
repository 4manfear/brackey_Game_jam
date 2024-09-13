using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOduler_build_System : MonoBehaviour
{
    [SerializeField]
    private GameObject cube1, cube2, cube3; // Buildable prefabs

    private GameObject previewGameObject; // Preview object for placement

    [SerializeField]
    private LayerMask placement_Layer; // Layer mask for valid placement surfaces

    [SerializeField]
    private float preview_offset ; // Offset to place objects above the ground

    private GameObject currentBuildablePrefab; // Current prefab to place

    private void Update()
    {
        // Handle object selection
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetBuildable(cube1); // Select cube1
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetBuildable(cube2); // Select cube2
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetBuildable(cube3); // Select cube3

        // Handle preview and placement if a prefab is selected
        if (currentBuildablePrefab != null)
        {
            HandlePreview();

            if (Input.GetKeyDown(KeyCode.G)) // Place object on left mouse click
            {
                PlaceBuildable();
            }
        }
    }

    // Set the current buildable prefab
    private void SetBuildable(GameObject buildablePrefab)
    {
        currentBuildablePrefab = buildablePrefab;
        if (previewGameObject != null)
        {
            Destroy(previewGameObject); // Destroy old preview if exists
        }
        CreatePreviewObject();
    }

    // Create a preview object to show where the buildable will be placed
    private void CreatePreviewObject()
    {
        if (currentBuildablePrefab != null)
        {
            previewGameObject = Instantiate(currentBuildablePrefab);
            previewGameObject.GetComponent<Collider>().enabled = false; // Disable collider for preview
        }
    }

    // Handle preview placement
    private void HandlePreview()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placement_Layer))
        {
            Vector3 placementPosition = hit.point + Vector3.up * preview_offset;
            if (previewGameObject != null)
            {
                previewGameObject.transform.position = placementPosition;
                previewGameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }
        }
    }

    // Place the buildable object at the preview location
    private void PlaceBuildable()
    {
        if (previewGameObject != null)
        {
            Instantiate(currentBuildablePrefab, previewGameObject.transform.position, previewGameObject.transform.rotation);
            Destroy(previewGameObject); // Remove the preview object after placement
        }
    }

}
