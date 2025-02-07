using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModuleController : MonoBehaviour
{
    // Private Fields
    private IPlayerModule[] playerModules;



    // Start Frame
    void Start()
    {
        // Get Components
        GetComponents();
    }




    // Get Components
    private void GetComponents()
    {
        // IPlayerModule
        playerModules = GetComponentsInChildren<IPlayerModule>();

        if (playerModules.Length == 0)
        {
            Debug.LogError("Not Components Found In Player");
        }
    }




    // Each Frame
    void Update()
    {
        // Handle Modules
        HandleModules();
    }




    // Handle Modules
    private void HandleModules()
    {
        // Handle Module
        foreach (var module in playerModules)
        {
            module.HandleModule();
        }
    }
}
