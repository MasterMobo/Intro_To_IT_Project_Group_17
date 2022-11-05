using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float knockbackForce;
    public Vector3 positionOffset;

    public float coolDown = 0.2f;
    public float coolDownElapsed = 0f;
    public bool isCoolingDown = false;

    public Player player;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public void UpdatePosition()
    {
        if (player.isFacingRight)
        {
            spriteRenderer.flipX = false;
            positionOffset.x = Mathf.Abs(positionOffset.x);
        }
        else
        {
            spriteRenderer.flipX = true;
            positionOffset.x = -Mathf.Abs(positionOffset.x);
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, positionOffset, Time.deltaTime * 5f);
    }

    // Rotate the weapon according to target direction (no idea how this works)
    public void RotateAccordingTo(Vector3 target)
    {
        Quaternion rotation = Quaternion.LookRotation(-target, Vector3.forward);
        rotation.x = 0f;
        rotation.y = 0f;
        transform.rotation = rotation;
    }

    public void CheckCooldown()
    {
        if (isCoolingDown)
        {
            coolDownElapsed += Time.fixedDeltaTime;
        }

        if (coolDownElapsed > coolDown)
        {
            isCoolingDown = false;
            coolDownElapsed = 0f;
        }
    }

    public virtual void Attack()
    {
        if (!isCoolingDown && player.isAlive)
        {
            animator.SetTrigger("attack");
            isCoolingDown = true;
        }
    }

}
