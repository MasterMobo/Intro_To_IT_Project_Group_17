using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Child class of Enemy
// AttackingEnemy is an Enemy that has speacial animations and functions for attacking (ex: slime)

public class AttackingEnemy : Enemy
{

    public float attackDistance; // The range within which the enemy will attack
    public float enemyAttackDamage; // Damage of the attack (Note: different from enemyCollisionDamage) 
    public float enemyAttackKnockbackForce; // Knockback force when enemy attacks other objects

    private void Start()
    {
        // Getting components
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        detectionZone = GetComponentInChildren<EnemyDetectionZone>();

        InitializeHealth();
    }

    void Update()
    {
        // Check if dead
        if (!isAlive)
        {
            StartDeath();
        }

        // Check if movement is locked
        if (!movementLocked)
        {
            // Check if the detection zone has one or more objects
            if (detectionZone.detected.Count > 0)
            {
                Vector3 detectedPosition = detectionZone.detected[0].transform.position;
                MoveTowards(detectedPosition);

                // If object is within attack range, attack!
                if (Vector3.Distance(transform.position, detectedPosition) <= attackDistance)
                {
                    StartAttack();
                }

                FlipXAccordingTo(player.transform.position - transform.position);
            } else 
            {
                FlipXAccordingTo(body.velocity);
            }

            
        }

        // If the velocity is not zero, start move animation
        animator.SetBool("isMoving", body.velocity != Vector2.zero);

        
    }


    // Start attack process, lock movement
    void StartAttack()
    {
        LockMovement();
        animator.SetTrigger("attack");

    }

    // End attack process, unlock movement (Usually an event in the attack animation)
    void EndAttack()
    {
        UnlockMovement();
    }
}

