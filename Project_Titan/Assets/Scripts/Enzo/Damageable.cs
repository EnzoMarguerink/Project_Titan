using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] protected bool invincible = false;
    [SerializeField] protected float health = 100f;
    [SerializeField] protected float maxHealth = 100f;
    protected bool died;

    [Header("Armor")]
    [SerializeField] protected bool armorEnabled = true;
    [SerializeField] protected float armorHealth = 100f;
    [SerializeField] protected float armorProtectionProcent = 50f;
    [SerializeField] protected float armorStrength = 10f;
    [SerializeField] protected float maxArmorHealth = 100f;

    private void Start()
    {
        died = false;
    }

    private void Update()
    {
    }


    #region GetFunctions

    /// Returns The current health
    public float GetHealth()
    {
        return health;
    }

    /// Returns The current armor
    public float GetArmor()
    {
        return armorHealth;
    }

    /// Returns The Max Health
    public float GetMaxHealth()
    {
        return maxHealth;
    }

    /// Returns The Max Armor
    public float GetMaxArmor()
    {
        return maxArmorHealth;
    }

    #endregion

    #region DoFunctions

    /// Removes Health
    public void DoDamage(float damage, string removedBy)
    {
        if (!invincible)
        {
            if (armorEnabled)
            {
                if (armorHealth > 0)
                {
                    armorHealth -= damage / armorStrength;
                    OnArmorHealthLost(damage / armorStrength);
                    float _damageProtected = (damage / 100) * armorProtectionProcent;
                    damage -= _damageProtected;
                }
            }

            OnHealthLost(damage, removedBy);

            health -= damage;
            health = Mathf.Clamp(health, 0, maxHealth);

            if (health <= 0 && died == false)
            {
                OnDeath();
                OnDeath(removedBy);
                died = true;
            }
        }
    }

    /// Does the damage over the time
    public void DoDamage(float damage, float overTime, string removedBy)
    {
        // Zorgt dat je een noramale void kan roepen inplaats van start Courutine
        StartCoroutine(IDoDamage(damage, overTime, removedBy));
    }

    ///  Does the damage over the time
    private IEnumerator IDoDamage(float damage, float overTime, string removedBy)
    {
        float timer = 0;
        float startHealth = this.health;

        while (timer < overTime)
        {
            DoDamage((Time.deltaTime / overTime) * damage, removedBy);

            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        health = startHealth = damage;
    }

    #endregion

    #region AddFunctions
    /// Returns The current health
    public void AddHealth(float _health)
    {
        health += _health;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    /// Returns The current armor
    public void AddArmorHealth(float _armorHealth)
    {
        armorHealth += _armorHealth;
        armorHealth = Mathf.Clamp(armorHealth, 0, maxArmorHealth);
    }
    #endregion

    #region OnFuncitons
    
    /// Is Called When health Is 0 or lower with a name
    
    protected virtual void OnDeath(string diedBy)
    {
        Debug.Log("RIP");
    }
   
    /// Is Called When health Is 0 or lower
    protected virtual void OnDeath() { }

    /// Is Called When Health Is Removed
    protected virtual void OnHealthLost(float healthLost, string hitBy) { }

    /// Is Called When Health Is Removed
    protected virtual void OnArmorHealthLost(float armorHealthLost) { }
    #endregion
}
