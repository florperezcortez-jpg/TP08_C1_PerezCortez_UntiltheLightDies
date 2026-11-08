using UnityEngine.InputSystem;
using UnityEngine;


public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private WeaponData weapon;
    [SerializeField] private Transform firePoint;
    [SerializeField] private InputActionReference attack;
    public WeaponData Weapon => weapon;

    private float nextFireTime;
    private Animator animator;
    private int damageBonus;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        attack.action.Enable();
    }

    private void OnDisable()
    {
        attack.action.Disable();
    }

    private void Update()
    {
        if (attack.action.WasPressedThisFrame() && Time.time >= nextFireTime)

        {
            nextFireTime = Time.time + weapon.fireRate;
            Shoot();

        }

    }
    public void AddDamageBonus(int amount)
    {
        damageBonus += amount;
    }
    public void SetWeapon (WeaponData newWeapon)
    {
        weapon = newWeapon;
    }
    private void Shoot()
    {
        if(weapon == null)
        {
            Debug.LogError("PlayerCombat: falta asignar weápon", this);
            return;
        }
        if(firePoint == null)
        {
            Debug.LogError("PlayerCombat: falta asignar firepoint", this);
            return;
        }
        if (weapon.projectilePrefab == null)
        {
            Debug.LogError("PlayerCombat: Weapon no tiene Projectile Prefab.", this);
            return;
        }
        if (animator == null)
        {
            Debug.LogError("PlayerCombat: falta el Animator en Player.", this);
            return;
        }
        
        animator.SetTrigger("Attack");
        GameObject projectile = Instantiate(weapon.projectilePrefab, firePoint.position, weapon.projectilePrefab.transform.rotation);
       
        Projectile projectileScript = projectile.GetComponent<Projectile>();


        if (projectileScript == null)
        {
            Debug.LogError("PlayerCombat: el prefab no tiene el componente Projectile.", projectile);
            Destroy(projectile);
            return;
        }

        float direction = Mathf.Sign(transform.localScale.x);
        projectileScript.Launch(direction, weapon, damageBonus);

        //if(AudioManager.Instance != null)
       // AudioManager.Instance.PlayShoot();
    }
}
