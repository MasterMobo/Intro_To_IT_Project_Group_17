using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Child class of Enemy
// NonAttackingEnemy is an Enemy that has no animation or functions for attacking (ex: slime)
// No extra functionality yet

public class NonAttackingEnemy : Enemy
{
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

    // Update is called once per frame
    void Update()
    {
        // Check if dead
        if (!isAlive)
        {
            StartDeath();
        }

        if (detectionZone.detected.Count > 0)
        {
            MoveTowards(detectionZone.detected[0].transform.position);
            FlipXAccordingTo(player.transform.position - transform.position);
        }
        else
        {
            FlipXAccordingTo(body.velocity);
        }

        // If the velocity is not zero, start move animation
        animator.SetBool("isMoving", body.velocity != Vector2.zero);

        
    }
}
