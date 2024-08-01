using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Initialtext : MonoBehaviour
{
    public Canvas uiText; // Assign the Text component in the Inspector
    public float displayDuration = 5f; // Time in seconds before the text disappears

    void Start()
    {
        // Start the coroutine that hides the text after the specified duration
        StartCoroutine(HideTextAfterDelay(displayDuration));
    }

    IEnumerator HideTextAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Disable the Text component to make it disappear
        uiText.enabled = false;
    }
}
