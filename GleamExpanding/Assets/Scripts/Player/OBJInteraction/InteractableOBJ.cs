using UnityEngine;

public class InteractableOBJ : MonoBehaviour
{
    [Header("Interactable Object Properties")]
    public string objectType;
    public bool isPickable;
    public bool isInteractable;


    private bool sufficientScriptAssigned;
    private ProximityDetector proximityDetector;


    // Start
    private void Start()
    {
        // Can Interact
        if (isInteractable || isPickable)
        {
            proximityDetector = GetComponent<ProximityDetector>();
            sufficientScriptAssigned = (proximityDetector != null);
        }
        else
        {
            sufficientScriptAssigned = true;
        }

        // Debug
        if (!sufficientScriptAssigned)
            Debug.LogWarning($"[WARNING] {gameObject.name} is missing required ProximityDetector script for interaction!", this);
    }




    // Set Properties
    public void SetProperties(string type, bool pickable, bool interactable)
    {
        objectType = type;
        isPickable = pickable;
        isInteractable = interactable;
    }
}
