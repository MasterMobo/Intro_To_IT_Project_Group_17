using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Child class of Enemy
// AttackingEnemy is an Enemy that has speacial animations and functions for attacking (ex: slime)

public class BossEnemy : Enemy
{

    public float attackDistance; // The range within which the enemy will attack
    public float shootDistance;
    public float enemyAttackDamage; // Damage of the attack (Note: different from enemyCollisionDamage) 
    
    private float timeBtwShots;
    public float startTimeBtwClones;
    private bool isShot;
    private bool TitanSummoned;
    private float timeBtwClones;
    private Transform playerpos;

    public GameObject projectile;
    public GameObject clone;
    public GameObject titan;


    
    private void Start()
    {
        // Getting components
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        playerpos = GameObject.FindWithTag("Player").transform;
        detectionZone = GetComponentInChildren<DetectionZone>();
        timeBtwClones = startTimeBtwClones;
        TitanSummoned = true;
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
                        animator.SetTrigger("attack");
                        isShot = false;
                        
                    
                    }
                }
        if (timeBtwClones <= 0)
        {
            Instantiate(clone, playerpos.position, Quaternion.identity);
            Instantiate(clone, playerpos.position, Quaternion.identity);
            timeBtwClones = startTimeBtwClones;
        } else
        {
            timeBtwClones -= Time.deltaTime;
        }
            
            }

        }

        // If the velocity is not zero, start move animation
        animator.SetBool("isMoving", body.velocity != Vector2.zero);

        if (currentHealth <= maxHealth/4)
        {
            while (TitanSummoned)
            {
                SummonTitan();
                TitanSummoned = false;
                acceleration = 0;
                maxMovementSpeed = 0;
            }
        }
        checkFlipDirection();
        IEnumerator ReleaseMovement()
        {
            yield return new WaitForSeconds (18f);
            acceleration = 100;
            maxMovementSpeed = 200;
            armor = 1;
        }
        StartCoroutine(ReleaseMovement());

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
    void SummonTitan()
    {
        
            Instantiate(titan, transform.position, Quaternion.identity);
            armor += 50;
    
    }
}

