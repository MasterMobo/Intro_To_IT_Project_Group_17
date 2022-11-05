using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    float aliveTimeWindow = 2f;
    float aliveTimeElapsed = 0;
    int currentPassThrough = 0;
    public RangedWeapon weapon;
    public GameObject hitFX;

    private void Start()
    {
        transform.RotateAround(transform.position, new Vector3(0, 0, 1), 0f); ;
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
            GameObject newhitFX = Instantiate(hitFX, transform.position, Quaternion.identity);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (currentPassThrough < weapon.passThrough)
            {
                // Calculate knockback force and apply damage to enemy
                Vector2 knockback = weapon.player.mouseDirection * weapon.knockbackForce;
                other.GetComponent<Enemy>().OnHit(weapon.damage, knockback);
                currentPassThrough++;
            }
            else
            {
                Vector2 knockback = weapon.player.mouseDirection * weapon.knockbackForce;
                other.GetComponent<Enemy>().OnHit(weapon.damage, knockback);

                SpawnHitEffects();
                Destroy(gameObject);
            }
        }

        else if (other.tag == "CollisionObject")
        {
            SpawnHitEffects();
            Destroy(gameObject);
        }
    }

}
