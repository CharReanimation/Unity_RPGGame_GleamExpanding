using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    // Public Fields
    public List<WeaponData> availableWeapons = new List<WeaponData>(); // Store Player Weapon
    public Transform weaponHolder; // Weapon Holder

    // Private Fields
    public Weapon currentWeapon { get; private set; }




    private void Start()
    {
        
    }




    // Pick Up Weapon
    public void PickupWeapon(WeaponData weaponData)
    {
        if (!availableWeapons.Contains(weaponData))
        {
            availableWeapons.Add(weaponData);
        }

        // Equip Weapon
        EquipWeapon(weaponData);
    }

    // Equip Weapon
    public void EquipWeapon(WeaponData weaponData)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        GameObject weaponObj = Instantiate(weaponData.weaponPrefab, weaponHolder);
        currentWeapon = weaponObj.GetComponent<Weapon>();
        currentWeapon.weaponData = weaponData;
    }

    // Drop Weapon
    public void DropWeapon()
    {
        if (currentWeapon != null)
        {
            GameObject droppedWeapon = Instantiate(currentWeapon.weaponData.weaponPrefab, transform.position, Quaternion.identity);
            droppedWeapon.AddComponent<PickableWeapon>().weaponData = currentWeapon.weaponData;

            availableWeapons.Remove(currentWeapon.weaponData);
            Destroy(currentWeapon.gameObject);
            currentWeapon = null;
        }
    }


    // Get Current Weapon Info
    public WeaponData GetCurrentWeaponInfo()
    {
        if (currentWeapon != null)
        {
            return currentWeapon.weaponData;
        }
        return null;
    }


    // Attack
    public void Attack()
    {
        if (currentWeapon != null)
        {
            currentWeapon.Attack();
        }
    }
}
