using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public float fireForce = 20f;
    public int passThrough = 0; // How many objects the projectile can pass through before disappearing

    public GameObject projectile;

    private void Start()
    {

        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        positionOffset = new Vector3(0.05f, transform.localPosition.y);

    }

    private void FixedUpdate()
    {
        RotateAccordingTo(player.mouseDirection);

        UpdatePosition();

        CheckCooldown();
    }



    public override void Attack()
    {
        if (!isCoolingDown && player.isAlive)
        {
            // Make new projectile
            GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);

            // Set the projectile's weapon to this weapon (to pass down weapon stats)
            newProjectile.GetComponent<PlayerProjectile>().weapon = GetComponent<RangedWeapon>();

            // Get the rigid body component of the new projectile and add a force to it
            Rigidbody2D newProjectileBody = newProjectile.GetComponent<Rigidbody2D>();
            newProjectileBody.AddForce(player.mouseDirection * fireForce);

            if (animator != null)
            {
                // Play shoot animation
                animator.SetTrigger("shoot");
            }

            isCoolingDown = true;
        }
    }
}

