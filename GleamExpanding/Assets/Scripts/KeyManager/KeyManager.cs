using System;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    // Public Fields
    public static KeyManager Instance;
    public event Action OnInteractPressed;




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
        // Press 'E'
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteractPressed?.Invoke();
        }
    }
}
