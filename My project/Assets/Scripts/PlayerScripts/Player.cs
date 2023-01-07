using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Child class of DamageableCharacter
// Main script for player game object

public class Player : DamageableCharacter
{
    public Vector3 mouseDirection; // Mouse direction reletive to player (tail at player, tip at mouse)
    public bool isFacingRight;

    public PlayerInput playerInput;
    public PlayerState playerState;
    public PlayerMovement playerMovement;

    public Animator animator;
    Rigidbody2D body;
    SpriteRenderer spriteRenderer;
    Collider2D playerCollider;
    public GameObject currentItem;

    public Inventory inventory;

    //====================================================================
    // Start is called first frame update
    //====================================================================
    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        inventory = GetComponent<Inventory>();

        playerInput = GetComponent<PlayerInput>();
        playerState = GetComponent<PlayerState>();
        playerMovement = GetComponent<PlayerMovement>();

        InitializeHealth();
    }





    //====================================================================
    // Update is called every fixed amount of time
    //====================================================================
    private void FixedUpdate()
    {
        // Check if player is alive
        if (!isAlive)
        {
            StartDeath();
        }

        CalculateMouseDirection();

        
        FlipXAccordingTo(mouseDirection);

        // Check if movement is locked
        if (!playerMovement.movementLocked)
        {
            // Only move the player if input vector is not 0 
            if (playerInput.movementInput != Vector2.zero)
            {
                // Start moving animation
                animator.SetBool("isMoving", true);

                // If SHIFT is pressed (player is running), add force scaled by running speed
                if (playerInput.isRunningPressed && !playerState.recoveringFromZeroStamina)
                {
                    body.AddForce(playerInput.movementInput * playerMovement.runningSpeed);
                    playerState.currentStamina -= playerState.runningStaminaCost;
                    animator.speed = 1.7f;
                    FlipXAccordingTo(body.velocity);
                }
                // If not, add force scaled by walking speed
                else
                {
                    body.AddForce(playerInput.movementInput * playerMovement.walkingSpeed);
                    animator.speed = 1;
                }
               

                // If SPACE was pressed during movement input, dash in the direction of movement input
                if (playerInput.isDashingPressed && playerState.currentStamina >= playerState.dashStaminaCost)
                {
                    body.AddForce(playerInput.movementInput * playerMovement.dashForce);
                    playerState.currentStamina -= playerState.dashStaminaCost;
                }

                // Restrain the velocity
                body.velocity = Vector3.ClampMagnitude(body.velocity, playerMovement.maxMovementSpeed);

            }


            // If movement is NOT locked, but there's no input
            else
            {
                // Make transition from walking to idle animation
                animator.SetBool("isMoving", false);
                
            }
        }


        
        playerInput.ResetPressedValues();
    }





    //====================================================================
    // Utility Functions
    //====================================================================

    void CalculateMouseDirection()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseDirection = Camera.main.ScreenToWorldPoint(mouseScreenPosition) - transform.position;
        mouseDirection.z = 0f;
        mouseDirection = Vector3.Normalize(mouseDirection);
    }



    // Set sprite direction according to target direction  
    void FlipXAccordingTo(Vector3 target)
    {
        if (target.x < 0 && isAlive)
        {
            spriteRenderer.flipX = true;
        }
        else if (target.x > 0 && isAlive)
        {
            spriteRenderer.flipX = false;
        }
        // We dont check x = 0 because there hasn't been an input yet, so we don't want to change the flipping

        isFacingRight = !spriteRenderer.flipX;
    }





    // Player was hit
    public override void OnHit(float damage, Vector3 knockBack)
    {
        // Check invunerability before applying hit
        if (!playerState.isInvulnerable)
        {
            base.OnHit(damage, knockBack);
            body.AddForce(knockBack);
            animator.SetTrigger("isHit");
            playerState.isInvulnerable = true;
        }
    }

    // Lock the movement when attacking
    public void StartAttack()
    {
        playerMovement.LockMovement();
        currentItem.GetComponent<Weapon>().Attack();
    }

    public void StartDeath()
    {
        playerMovement.LockMovement();
        animator.SetTrigger("dead");
        playerCollider.enabled = false;
        GameObject.Find("GameStateManager").GetComponent<GameStateManager>().GameOverScreen();
    }


}
