using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour, IPlayerModule
{
    // Public Fields
    [Header("Animator Component")]
    public Animator animator;

    [Header("Animator Controllers")]
    public RuntimeAnimatorController rifleAnimatorController; // Rifle
    public RuntimeAnimatorController swordAnimatorController; // Sword

    [Header("Player Scripts")]
    public MonoBehaviour playerRifleScript; // Rifle Script
    public MonoBehaviour playerSwordScript; // Sword Script


    [Header("Player Weapon")]
    public PlayerWeapon playerWeapon;




    // Start
    private void Start()
    {
        // Get Components
        GetComponents();

        // Swtich Weapon
        SwitchWeapon(playerWeapon.WeaponType);
    }


    // Get Components
    private void GetComponents()
    {
        // Get Components
        playerWeapon = GetComponent<PlayerWeapon>();


        // Debug: Log Error
        if (playerWeapon == null)
        {
            Debug.LogError("Player Weapon is not assigned!");
        }

        if (animator == null || rifleAnimatorController == null || swordAnimatorController == null)
        {
            Debug.LogError("Animator or AnimatorController is not assigned!");
        }
    }




    // Handle Module: Void Update
    public void HandleModule()
    {
        // Update Scripts

    }




    // Switch Weapon
    public void SwitchWeapon(PlayerWeaponType weaponType)
    {
        switch (weaponType)
        {
            case PlayerWeaponType.Rifle:
                animator.runtimeAnimatorController = rifleAnimatorController;
                EnableOnlyOneScript(playerRifleScript);
                break;

            case PlayerWeaponType.Sword_Large:
                animator.runtimeAnimatorController = swordAnimatorController;
                EnableOnlyOneScript(playerSwordScript);
                break;
        }

        // Debug Log
        Debug.Log("Current Using: " + weaponType);
    }

    private void EnableOnlyOneScript(MonoBehaviour activeScript)
    {
        // Deactive All Scripts
        playerRifleScript.enabled = false;
        playerSwordScript.enabled = false;

        // Active Current Script
        if (activeScript != null)
        {
            activeScript.enabled = true;
        }
    }
}
