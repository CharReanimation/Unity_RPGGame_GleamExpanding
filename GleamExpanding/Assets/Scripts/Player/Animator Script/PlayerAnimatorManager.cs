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
    private Weapon currentWeapon => playerWeaponManager.currentWeapon;
    private WeaponData currentWeaponData => playerWeaponManager.currentWeapon.weaponData;




    // Start: Switch Weapon Animator First
    private void Start()
    {
        // Get Components
        GetComponents();

        // Deactivate All Animator Script
        DeactivateAllAnimatorScript();

        // Switch Weapon
        if (currentWeapon == null) // No Weapon
        {
            // Current Weapon
            Debug.Log("No Current Weapon Type!");

            // Handle Switch Weapon: Hand
            HandleSwitchWeapon(handAnimatorController, playerHandScript); // Hand
        }
        else
        {
            // Switch Weapon
            SwitchWeaponAnimator(currentWeaponData.weaponType);

            // Current Weapon
            Debug.Log("Current Weapon Type: " + currentWeaponData.weaponType);
        }
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
                HandleSwitchWeapon(handAnimatorController, playerHandScript); // Hand
                break;

            case "Sword_Large":
                // Handle Switch Weapon
                HandleSwitchWeapon(swordAnimatorController, playerSwordScript); // Sword_Large
                break;

            case "Rifle":
                // Handle Switch Weapon
                HandleSwitchWeapon(rifleAnimatorController, playerRifleScript); // Rifle
                break;
        }


        // Debug Log
        Debug.Log("Current Using: " + weaponType);
    }


    // Handle Switch Weapon
    private void HandleSwitchWeapon(RuntimeAnimatorController currentAnimatorController, MonoBehaviour currentWeaponScript)
    {
        animator.runtimeAnimatorController = currentAnimatorController;
        EnableOnlyOneScript(currentWeaponScript);

        // Force Refresh
        GetComponent<PlayerModuleController>().ForceRefresh();
    }




    // Enable Only One Script
    private void EnableOnlyOneScript(MonoBehaviour activeScript)
    {
        // Deactivate All Animator Script
        DeactivateAllAnimatorScript();

        // Active Current Script
        if (activeScript != null)
        {
            activeScript.enabled = true;
        }
    }


    // Deactivate All Animator Script
    private void DeactivateAllAnimatorScript()
    {
        // Deactive All Scripts
        if (playerHandScript != null) playerHandScript.enabled = false;
        if (playerSwordScript != null) playerSwordScript.enabled = false;
        if (playerRifleScript != null) playerRifleScript.enabled = false;
    }
}
