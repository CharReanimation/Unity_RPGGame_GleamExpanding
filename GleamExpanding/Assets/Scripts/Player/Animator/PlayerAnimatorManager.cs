using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    // Public Fields
    [Header("Animator Component")]
    public Animator animator;

    [Header("Animator Controllers")]
    public RuntimeAnimatorController handAnimatorController; // Hand
    public RuntimeAnimatorController swordAnimatorController; // Sword
    public RuntimeAnimatorController rifleAnimatorController; // Rifle


    [Header("Player Scripts")]
    public MonoBehaviour playerHandScript; // Hand Script
    public MonoBehaviour playerSwordScript; // Sword Script
    public MonoBehaviour playerRifleScript; // Rifle Script


    [Header("Player Weapon")]
    public PlayerWeaponManager playerWeaponManager;
    private string currentWeaponType;




    // Start: Switch Weapon Animator First
    private void Start()
    {
        // Get Components
        GetComponents();

        // Swtich Weapon
        if (playerWeaponManager.GetCurrentWeaponInfo().weaponType == null) // No Weapon Type
        {
            // Current Weapon
            Debug.Log("No Current Weapon Type!");
        }
        else
        {
            // Switch Weapon
            SwitchWeaponAnimator(currentWeaponType);
        }
        

        // Current Weapon
        Debug.Log("Current Weapon Type: " + currentWeaponType);
    }


    // Get Components
    private void GetComponents()
    {
        // Get Components
        playerWeaponManager = GetComponent<PlayerWeaponManager>();


        // Debug: Log Error
        if (playerWeaponManager == null)
        {
            Debug.LogError("Player Weapon is not assigned!");
        }

        if (animator == null || rifleAnimatorController == null || swordAnimatorController == null)
        {
            Debug.LogError("Animator or AnimatorController is not assigned!");
        }
    }




    // Switch Weapon Animator
    public void SwitchWeaponAnimator(string weaponType)
    {
        switch (weaponType)
        {
            case "Hand":
                // Handle Switch Weapon
                HandleSwitchWeapon(weaponType, handAnimatorController, playerHandScript); // Hand
                break;

            case "Sword_Large":
                // Handle Switch Weapon
                HandleSwitchWeapon(weaponType, swordAnimatorController, playerSwordScript); // Sword_Large
                break;

            case "Rifle":
                // Handle Switch Weapon
                HandleSwitchWeapon(weaponType, rifleAnimatorController, playerRifleScript); // Rifle
                break;
        }


        // Debug Log
        Debug.Log("Current Using: " + weaponType);
    }


    // Handle Switch Weapon
    private void HandleSwitchWeapon(string weaponType, RuntimeAnimatorController currentAnimatorController, MonoBehaviour currentWeaponScript)
    {
        animator.runtimeAnimatorController = currentAnimatorController;
        EnableOnlyOneScript(currentWeaponScript);
        Debug.Log("Current Weapon Type: " + weaponType);
    }




    private void EnableOnlyOneScript(MonoBehaviour activeScript)
    {
        // Deactive All Scripts
        playerHandScript.enabled = false;
        playerSwordScript.enabled = false;
        playerRifleScript.enabled = false;

        // Active Current Script
        if (activeScript != null)
        {
            activeScript.enabled = true;
        }
    }
}
