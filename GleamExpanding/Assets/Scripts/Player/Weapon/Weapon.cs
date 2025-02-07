using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Weapon Data
    public WeaponData weaponData;
    private float nextFireTime = 0f;


    // Attack
    public void Attack()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + weaponData.fireRate;
            Debug.Log($"Use {weaponData.weaponName} Fire! Damage: {weaponData.damage}");
        }
    }
}
