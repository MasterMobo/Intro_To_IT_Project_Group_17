using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Child class of DamageableCharacter
// This is the template for all enemies, which can move, damage other object and die.

public class Enemy : DamageableCharacter
{
    // REMEMBER TO ADJUST THESE VARIABLE IN THE INSPECTOR OF PREFAB
    public float acceleration; // How fast the object can get to maximum speed
    public float maxMovementSpeed; // The maximum speed of the object
    public bool movementLocked; // Boolean for locking and unlocking movement
    public bool isFacingRight; // Boolean for tracking direction of object (to flip the object is necessary)

    public float aggroDistance; // The range within which the enemy will start to move towards target
    public float enemyCollisionDamage; // Damage when enemy collides into other objects
    public float enemyKnockbackForce; // Knockback force when enemy collides into other objects

    // Components
    public Animator animator;
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    public GameObject player;
    public DetectionZone detectionZone; // Child gameObjects for dectecting objects nearby (this has a seperate script, refer to: EnemyDetectionZone.cs)


    // Move the enemy towards input target
    public void MoveTowards(Vector3 target)
    {
        // Calculate direction from object to target
        Vector2 direction = target - transform.position;

        body.AddForce(direction * acceleration * Time.fixedDeltaTime);
    }

    // Enemy collides with other object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D other = collision.collider;

        // If it collided with the player, call OnHit on player
        if (other.tag == "Player")
        {
            Vector2 dir = other.transform.position - gameObject.GetComponentInParent<Transform>().position;
            Vector2 knockback = Vector3.Normalize(dir) * enemyKnockbackForce;
            other.GetComponent<Player>().OnHit(enemyCollisionDamage, knockback);
        }
    }

    // Enemy was hit by another object
    public override void OnHit(float damage, Vector3 knockBack)
    {
        base.OnHit(damage, knockBack);
        body.AddForce(knockBack);
        animator.SetTrigger("isHit");
    }

    // Function for checking if enemy is facing left or right
    public void checkFlipDirection()
    {
        // If facing left, flip x-axis of sprite
        if (body.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        };
        // If facing right, don't flip x-axis of sprite
        if (body.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        };

        // Set the isFacingRight variable accordingly after checking
        isFacingRight = !spriteRenderer.flipX;
    }

    // Function for locking the movement
    public void LockMovement()
    {
        movementLocked = true;
    }

    // Function for unlocking the movement
    public void UnlockMovement()
    {
        movementLocked = false;
    }

    // Start death process
    public void StartDeath()
    {
        // Start death animation
        animator.SetTrigger("dead");
    }

    // End death process (Usually an event in the death animation)
    public void EndDeath()
    {
        DropLoot loot = GetComponentInParent<DropLoot>();
        if (loot != null)
        {
            loot.SpawnLoot();
        }

        Destroy(gameObject);
    }

}
