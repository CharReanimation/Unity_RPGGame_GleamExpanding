using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    // Public Fields
    public static PlayerWeapon Instance { get; private set; }

    public PlayerWeaponType weaponType = PlayerWeaponType.Rifle;
    public PlayerWeaponType WeaponType
    {
        get => weaponType;
        set => weaponType = value;
    }


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


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
