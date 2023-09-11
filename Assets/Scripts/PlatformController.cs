using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private bool canDescend = false;

    private void Update()
    {
        // Check if the player presses down
        if (Input.GetAxis("Vertical") < 0)
        {
            canDescend = true;
            StartCoroutine(DescendPlatform());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Reset the canDescend flag when the player exits the platform collider
        if (other.CompareTag("Player"))
        {
            canDescend = false;
        }
    }

    private IEnumerator DescendPlatform()
    {
        // Wait for a short delay to give time to jump over the platform
        yield return new WaitForSeconds(0.1f);

        // Check if the player is still pressing down and can descend
        if (canDescend)
        {
            // Disable the platform's collider so the player can pass through
            GetComponent<Collider2D>().enabled = false;

            // Wait for a short duration to let the player descend
            yield return new WaitForSeconds(0.5f);

            // Enable the platform's collider again
            GetComponent<Collider2D>().enabled = true;
        }
    }
}
