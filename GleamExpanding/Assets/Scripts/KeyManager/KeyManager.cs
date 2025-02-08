using System;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    // Public Fields
    public static KeyManager Instance;
    public event Action OnInteractPressed;
    public event Action OnSwitchWeaponPressed;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        // Handle Key Input
        HandleKeyInput();
    }




    // Handle Key Input
    private void HandleKeyInput()
    {
        // Press 'E': Interact Object
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteractPressed?.Invoke();
        }

        // Press 'Q': Switch Weapon
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnSwitchWeaponPressed?.Invoke();
        }
    }
}
