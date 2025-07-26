using UnityEngine;
// Script to make plane appear when user walks over trigger. + audio.
public class WarningZoneController : MonoBehaviour
{
    // Assign your "WarningFloor_Visual" GameObject here in the Inspector
    public GameObject warningFloorVisual;

    // Assign your AudioSource component here (from the WarningTriggerZone GameObject)
    public AudioSource audioSource;

    public string playerTag = "Player"; // Ensure your VR player object is tagged "Player"

    void Start()
    {
        // Ensure the visual floor is hidden at the start
        if (warningFloorVisual != null)
        {
            warningFloorVisual.SetActive(false);
        }
        else
        {
            Debug.LogError("WarningFloorVisual GameObject is not assigned in the Inspector!", this);
        }

        // Get the AudioSource component if not already assigned in the Inspector
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("No AudioSource found on this GameObject or assigned in Inspector! Audio will not play.", this);
            }
        }
    }

    // Called when another collider enters this trigger
    void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the player
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Player entered warning zone! Making floor visible and playing audio.");

            // Make the visual floor appear
            if (warningFloorVisual != null)
            {
                warningFloorVisual.SetActive(true);
            }

            // Play the audio clip
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();
            }
        }
    }

    // Called when another collider exits this trigger
    void OnTriggerExit(Collider other)
    {
        // Check if the exiting object is the player
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Player exited warning zone! Making floor invisible and stopping audio.");

            // Make the visual floor disappear
            if (warningFloorVisual != null)
            {
                warningFloorVisual.SetActive(false);
            }

            // Stop the audio clip if it's playing
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}