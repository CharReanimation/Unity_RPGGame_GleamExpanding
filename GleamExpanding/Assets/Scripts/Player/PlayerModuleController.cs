using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModuleController : MonoBehaviour
{
    // Private Fields
    private IPlayerModule[] playerModules;
    private List<IPlayerModule> activeModules = new List<IPlayerModule>();


    // Start Frame
    void Start()
    {
        // Get Components
        GetComponents();

        // Refresh Modules
        RefreshModules();
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
            // Handle Activated Module
            if (((MonoBehaviour)module).enabled)
            {
                module.HandleModule();
            }
        }
    }


    // Refresh Modules
    private void RefreshModules()
    {
        activeModules.Clear();
        playerModules = GetComponentsInChildren<IPlayerModule>();

        foreach (var module in playerModules)
        {
            if (((MonoBehaviour)module).enabled)
            {
                activeModules.Add(module);
            }
        }
    }


    // Fore Refresh
    public void ForceRefresh()
    {
        RefreshModules();
    }
}
