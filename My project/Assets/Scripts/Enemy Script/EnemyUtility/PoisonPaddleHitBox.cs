using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a child game object of enemy
// This is a hitbox that enables itself if and only if the enemy is attacking
// NOTE: the enabling feature is achieved ONLY TRHOUGH ANIMATION PROPERTIES OF KEY FRAMES (i.e enable the collider on appropriate frames)

public class PoisonPaddleHitBox : MonoBehaviour
{
    PoisonPaddle parent;

    private void Start()
    {
        parent = GetComponentInParent<PoisonPaddle>();
    }

    

    // If the hitbox detects collision with player, calculate knockback force and apply OnHit() to player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector2 dir = other.transform.position - gameObject.GetComponentInParent<Transform>().position;
            Vector2 knockback = Vector3.Normalize(dir) * parent.poisonKnockbackForce;
           
            IEnumerator Hit()
            {
                yield return new WaitForSeconds (1f);
                other.GetComponent<Player>().OnHit(parent.damage, knockback);
                yield return new WaitForSeconds (1f);
                other.GetComponent<Player>().OnHit(parent.damage, knockback);
                yield return new WaitForSeconds (1f);
                other.GetComponent<Player>().OnHit(parent.damage, knockback);
            }
        
            other.GetComponent<Player>().OnHit(parent.damage, knockback);
            StartCoroutine(Hit());
            
        }
    }
    
}

