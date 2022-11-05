using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a templete for all objects that have health, armor and can take damage
public class DamageableCharacter : MonoBehaviour
{
    public float maxHealth;
    public float _currentHealth; // health of object, determines whether object is alive or dead
    public float armor = 1; // resistance to damage (1 to infinity)
    public float knockbackResistance = 1; // resistance to knockback (1 to infinity)
    public bool isAlive = true; // true is health > 0, false is health <= 0
    

    // Setters and getters for _health
    public float currentHealth 
    {
        set
        {
            _currentHealth = value;
            // check _health every time it gets assigned, if < 0, object is dead
            if (_currentHealth > maxHealth)
            {
                _currentHealth = maxHealth;
            }
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                isAlive = false;
            }
        }
        get
        {
            return _currentHealth;
        }
    }
    public void InitializeHealth() // Remember to call on Start();
    {
        currentHealth = maxHealth;
    }

    // Damage taken is invertly proportional to armor value
    public void TakeDamage(float damage)
    {
        currentHealth -= damage/armor;
    }

    // This function should be called every time the object is hit (ex: player hit enemy -> enemy.OnHit())
    public virtual void OnHit(float damage, Vector3 knockBack)
    {
        TakeDamage(damage);
    }
}
