using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ZombieWaveManager : MonoBehaviour
{
    

    public Daynight_cycle daynight_Cycle;          // Reference to the day-night cycle
    public GameObject zombiePrefab;                // Reference to the zombie prefab
    public GameObject Player_Object;               // Reference to the player object
    public Transform[] Zombi_Spawnpoint;           // Spawn points for zombies
    public int baseZombiCount = 20;                // Number of zombies in the first wave
    private int currentwave = 0;                   // Track the current wave
    private int zombicount;                        // Number of zombies in the current wave
    private List<GameObject> activeZombies = new List<GameObject>(); // Track active zombies
    public bool wave_in_Progess;                   // To track if a wave is in progress
    private int zombiesKilled = 0;                 // Counter for the number of zombies killed

    private void Start()
    {
        BakeNavMesh(); // Automatically bake the NavMesh at the start
    }

    private void Update()
    {
        // Check if it's nighttime and a wave is not yet in progress
        if (!daynight_Cycle.isDay && !wave_in_Progess)
        {
            StartNightWave(); // Start a new wave when night starts
        }

        // Check if it's daytime and the wave is still in progress
        if (daynight_Cycle.isDay && wave_in_Progess)
        {
            EndNightWave(); // End the wave when day starts
        }

        // If the wave is in progress, check if all zombies are dead
        if (wave_in_Progess && activeZombies.Count == 0)
        {
            wave_in_Progess = false; // Wave ends when all zombies are dead
        }

        // Update the number of zombies killed
        UpdateZombiesKilled();
    }

    private void StartNightWave()
    {
        currentwave++; // Increasing the wave number
        zombicount = baseZombiCount + (currentwave - 1) * 10; // Increasing the zombie count by 10 each wave
        Spawinging_Of_zombi(); // Spawning the zombies
        wave_in_Progess = true;
    }

    private void EndNightWave()
    {
        // Clean up any remaining zombies in the active list
        foreach (GameObject zombie in activeZombies)
        {
            // Check if the zombie is not null (i.e., it hasn't already been destroyed)
            if (zombie != null)
            {
                // Destroy the zombie game object
                Destroy(zombie);
            }
        }

        // Clear the list of active zombies to remove all references
        activeZombies.Clear();

        // Set the flag indicating that the current wave is no longer in progress
        wave_in_Progess = false;
    }

    private void Spawinging_Of_zombi()
    {
        for (int i = 0; i < zombicount; i++)
        {
            // Select a random spawn point
            Transform spawnPoint = Zombi_Spawnpoint[Random.Range(0, Zombi_Spawnpoint.Length)];

            // Instantiate the zombie at the spawn point
            GameObject zombie = Instantiate(zombiePrefab, spawnPoint.position, Quaternion.identity);

            // Set the zombie's destination to the player's position
            NavMeshAgent navAgent = zombie.GetComponent<NavMeshAgent>();
            if (navAgent != null)
            {
                navAgent.SetDestination(Player_Object.transform.position);
            }

            // Add the zombie to the list of active zombies
            activeZombies.Add(zombie);
        }
    }

    // Update the number of zombies killed by checking the active zombies list
    private void UpdateZombiesKilled()
    {
        // Use a temporary list to store zombies to be removed
        List<GameObject> toRemove = new List<GameObject>();

        foreach (GameObject zombie in activeZombies)
        {
            // If the zombie is null (destroyed), count it as killed
            if (zombie == null)
            {
                zombiesKilled++; // Increment the zombie kill counter
                toRemove.Add(zombie); // Add to the removal list
            }
        }

        // Remove all null zombies from the active list
        foreach (GameObject zombie in toRemove)
        {
            activeZombies.Remove(zombie);
        }
    }

    // Automatically bake the NavMesh to ensure walkable paths for zombies
    private void BakeNavMesh()
    {
        NavMeshSurface[] surfaces = FindObjectsOfType<NavMeshSurface>();
        foreach (var surface in surfaces)
        {
            surface.BuildNavMesh(); // Build the NavMesh for each surface
        }
    }

}
