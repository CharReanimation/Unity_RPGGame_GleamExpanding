using UnityEngine;

public class PlayerInteraction : MonoBehaviour, IPlayerProximityTrigger
{
    private IInteractable currentInteractable;


    private void Start()
    {
        if (KeyManager.Instance != null)
        {
            KeyManager.Instance.OnInteractPressed += HandleInteractKey; // Enable Key Listening
        }
    }


    private void OnDestroy()
    {
        if (KeyManager.Instance != null)
        {
            KeyManager.Instance.OnInteractPressed -= HandleInteractKey; // Disable Key Listening
        }
    }




    // Press 'E': Interact With Object
    private void HandleInteractKey()
    {
        if (currentInteractable != null) 
        {
            currentInteractable.Interact();
        }
    }




    // Player Is In Range
    public void OnPlayerEnter(GameObject interactableObj)
    {
        currentInteractable = interactableObj.GetComponent<IInteractable>();

        if (currentInteractable != null)
        {
            Debug.Log($"[INFO] Player entered interaction range: {interactableObj.name}");
        }
    }

    // Player Is Not In Range
    public void OnPlayerExit()
    {
        if (currentInteractable != null)
        {
            Debug.Log($"[INFO] Player left interaction range: {currentInteractable}");
        }

        currentInteractable = null;
    }
}
