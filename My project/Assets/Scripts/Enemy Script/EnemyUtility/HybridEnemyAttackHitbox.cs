using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a child game object of enemy
// This is a hitbox that enables itself if and only if the enemy is attacking
// NOTE: the enabling feature is achieved ONLY TRHOUGH ANIMATION PROPERTIES OF KEY FRAMES (i.e enable the collider on appropriate frames)

public class HybridEnemyAttackHitbox : MonoBehaviour
{
    HybridEnemy parent;
    Vector3 positionOffset; // Vector to keep track of when to flip the x-axis according to the parent's direction (The local position needs to be offset first in the inspector)

    private void Start()
    {
        parent = GetComponentInParent<HybridEnemy>();
        positionOffset = transform.localPosition;
    }

    private void Update()
    {
        // Flip the x-axis according to parent
        if (parent.isFacingRight)
        {
            positionOffset.x = Mathf.Abs(positionOffset.x);
        }
        else
        {
            positionOffset.x = -Mathf.Abs(positionOffset.x);
        }
        transform.localPosition = positionOffset;
    }

    // If the hitbox detects collision with player, calculate knockback force and apply OnHit() to player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector2 dir = other.transform.position - gameObject.GetComponentInParent<Transform>().position;
            Vector2 knockback = Vector3.Normalize(dir) * parent.enemyKnockbackForce;
            other.GetComponent<Player>().OnHit(parent.enemyAttackDamage, knockback);
        }
    }
}
