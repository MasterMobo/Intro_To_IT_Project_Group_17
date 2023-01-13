using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Child class of Enemy
// AttackingEnemy is an Enemy that has speacial animations and functions for attacking (ex: slime)

public class HybridEnemy : Enemy
{

    public float attackDistance; // The range within which the enemy will attack
    public float shootDistance;
    public float enemyAttackDamage; // Damage of the attack (Note: different from enemyCollisionDamage) 
    
    private float timeBtwShots;
    public float startTimeBtwClones;
    private bool isShot;
    private float timeBtwClones;

    public GameObject projectile;
    public GameObject clone;

    
    private void Start()
    {
        // Getting components
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        detectionZone = GetComponentInChildren<DetectionZone>();
        timeBtwClones = startTimeBtwClones;
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
                    isShot = true;
                }
                else if (Vector3.Distance(transform.position, detectedPosition) > attackDistance && Vector3.Distance(transform.position, detectedPosition) <= shootDistance)
                {
                    
                    while (isShot)
                    {
            
                        Instantiate(projectile, transform.position, Quaternion.identity);
                        animator.SetTrigger("rangedAttack");
                        isShot = false;
                        
                    
                    }
                }
        if (timeBtwClones <= 0)
        {
                    if (clone != null)
                    {
                        Instantiate(clone, transform.position, Quaternion.identity);
                        Instantiate(clone, transform.position, Quaternion.identity);
                    }
                    timeBtwClones = startTimeBtwClones;
                
        } else
        {
            timeBtwClones -= Time.deltaTime;
        }
            
            }

        }

        // If the velocity is not zero, start move animation
        animator.SetBool("isMoving", body.velocity != Vector2.zero);

        checkFlipDirection();
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

