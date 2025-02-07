using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Properties")]
    public string weaponType;
    public string weaponName;
    public string weaponDescription;
    public int damage;
    public float fireRate;

    [Header("Game Object")]
    public GameObject weaponPrefab;
}
