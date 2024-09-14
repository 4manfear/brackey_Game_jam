using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using TMPro;

public class Daynight_cycle : MonoBehaviour
{

    public bool isDay = true;                    // Start with day
    public TextMeshProUGUI timerText;            // Reference to the TextMeshPro UI element
    public Light directionalLight;               // Reference to the Directional Light (Sun)
    public float rotationSpeed;            // Speed at which the sun rotates
    public float dayDuration;              // Duration of the day in seconds (90 seconds)

    private float currentTime;                   // Tracks the remaining time

    private void Start()
    {
        currentTime = dayDuration;               // Set the initial time to the day duration
        StartCoroutine(TimerCountdown());        // Start the timer countdown
    }

    private void Update()
    {
        // Update the UI text to show the current time in MM:SS format
        timerText.text = FormatTime(currentTime);

        // Rotate the sun based on day or night
        RotateSun();

        // Check if the day has ended
        if (currentTime <= 0 && isDay)
        {
            EndDay();                            // Switch to night when the timer reaches 0
        }
    }

    private IEnumerator TimerCountdown()
    {
        // Countdown loop
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;                       // Reduce the time by 1 second each loop
        }
    }

    private void EndDay()
    {
        isDay = false;                           // Set the day to false, indicating night has begun
        timerText.text = "Night Time!";          // Display "Night Time!" on the UI
        currentTime = dayDuration;               // Reset timer for the next cycle
        StartCoroutine(TimerCountdown());        // Start the night timer countdown
        // Trigger additional night-start behavior here, e.g., spawn zombies
    }

    // Rotate the sun during day or night
    private void RotateSun()
    {
        if (isDay)
        {
            // Rotate the sun to simulate it being overhead during the day
            Quaternion targetRotation = Quaternion.Euler(90, 0, 0); // 90 degrees for overhead sun
            directionalLight.transform.rotation = Quaternion.RotateTowards(
                directionalLight.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Rotate the sun to simulate it being below the horizon at night
            Quaternion targetRotation = Quaternion.Euler(-90, 0, 0); // -90 degrees for night sun
            directionalLight.transform.rotation = Quaternion.RotateTowards(
                directionalLight.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // Helper function to format the time as MM:SS
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);     // Format as MM:SS
    }
}