using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public Collider2D weaponCollider;

    private void Start()
    {

        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        positionOffset = new Vector3(0.05f, transform.localPosition.y);

    }

    private void FixedUpdate()
    {
        if (transform.parent != null)
        {
            RotateAccordingTo(player.mouseDirection);

            UpdatePosition();

            CheckCooldown();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("Enemy"))
        {
            // Calculate knockback force and apply damage to enemy
            Vector2 knockback = player.mouseDirection * knockbackForce;
            other.GetComponent<DamageableCharacter>().OnHit(damage, knockback);
        }
    }

}
