using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public GameObject projectilePrefab;
    public float fireRate = 0.4f;
    public int damage = 1;
    public float projectileSpeed = 10f;
    public float projectileLifetime = 2f;

    public float projectileTilt = 30f;
}
