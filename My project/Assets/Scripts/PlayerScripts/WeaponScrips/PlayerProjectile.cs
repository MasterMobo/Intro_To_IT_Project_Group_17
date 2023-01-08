using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    float aliveTimeWindow = 2f;
    float aliveTimeElapsed = 0;
    int currentPassThrough = 0;
    public bool isRotated;
    public Weapon weapon;
    public GameObject hitFX;

    private void Start()
    {
        if (isRotated)
        {
            transform.RotateAround(transform.position, new Vector3(0, 0, 1), 90f); 
        }
    }

    private void FixedUpdate()
    {
        if (aliveTimeElapsed > aliveTimeWindow)
        {
            Destroy(gameObject);
        }
        aliveTimeElapsed += Time.fixedDeltaTime;        
    }

    void SpawnHitEffects()
    {
            Instantiate(hitFX, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("Enemy"))
        {
            // Calculate knockback force and apply damage to enemy
            Vector2 knockback = weapon.player.mouseDirection * weapon.knockbackForce;
            other.GetComponent<DamageableCharacter>().OnHit(weapon.damage, knockback);

            SpawnHitEffects();


            if (currentPassThrough >= weapon.passThrough)
            {
                Destroy(gameObject);
            }

            currentPassThrough += 1;

        }

        else if (other.tag == "CollisionObject")
        {
            SpawnHitEffects();
            Destroy(gameObject);
        }

    }

}
