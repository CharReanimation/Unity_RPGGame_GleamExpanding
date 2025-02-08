using System;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    // Public Fields
    public static KeyManager Instance;

    // Player Interaction
    public event Action OnInteractPressed;

    // Player Weapon Manage
    public event Action OnSwitchWeaponPressed;

    // Player Movement
    public event Action<float, float> OnMoveInput; // Move
    public event Action<bool> OnRunPressed; // Run Key
    public event Action<string> OnDodgePressed; // Dodge key

    // Private Fields
    private float lastHorizontal = 0;
    private float lastVertical = 0;




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

        // Handle Player Move Key Input
        HandlePlayerMoveKeyInput();
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

        // Press 'Alt': Dodge
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (OnDodgePressed != null)
            {
                string dodgeDirection = GetDodgeDirection();
                OnDodgePressed.Invoke(dodgeDirection);
            }
        }
    }


    // Handle Player Move Key Input
    private void HandlePlayerMoveKeyInput()
    {
        float horizontal = Input.GetAxis("Horizontal"); // A (-1) / D (+1)
        float vertical = Input.GetAxis("Vertical"); // W (+1) / S (-1)

        lastHorizontal = horizontal;
        lastVertical = vertical;

        OnMoveInput?.Invoke(horizontal, vertical);

        // Run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        OnRunPressed?.Invoke(isRunning);
    }


    // Get Dodge Direction
    private string GetDodgeDirection()
    {
        if (lastVertical > 0) return "Forward";
        if (lastVertical < 0) return "Backward";
        if (lastHorizontal < 0) return "Left";
        if (lastHorizontal > 0) return "Right";
        return "None";
    }
}
