using UnityEngine;

public class PickableWeapon : MonoBehaviour, IInteractable
{
    public WeaponData weaponData; // Weapon Data
    private InteractableOBJ interactableOBJ;



    // Void Start
    private void Start()
    {
        // InteractableOBJ
        interactableOBJ = GetComponent<InteractableOBJ>();
        if (interactableOBJ == null)
        {
            interactableOBJ = gameObject.AddComponent<InteractableOBJ>();
        }

        // Set Properties: Weapon, isPickable, !isInteractable
        interactableOBJ.SetProperties("Weapon", true, false);
    }




    // Interact
    public void Interact()
    {
        if (interactableOBJ.isPickable)
        {
            PlayerWeaponManager playerWeaponManager = FindObjectOfType<PlayerWeaponManager>();
            if (playerWeaponManager != null)
            {
                playerWeaponManager.PickupWeapon(weaponData);
                Destroy(gameObject); // Destroy
            }
        }
    }
}
