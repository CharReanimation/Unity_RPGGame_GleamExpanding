using UnityEngine;

public class ProximityDetector : MonoBehaviour
{
    // Private Fields
    private IPlayerProximityTrigger playerTrigger;


    // Player Is In Range
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTrigger = other.GetComponent<IPlayerProximityTrigger>();
            if (playerTrigger != null)
            {
                playerTrigger.OnPlayerEnter(gameObject);
            }
        }
    }

    // Player Is Not In Range
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTrigger = other.GetComponent<IPlayerProximityTrigger>();
            if (playerTrigger != null)
            {
                playerTrigger.OnPlayerExit();
            }
        }
    }
}
