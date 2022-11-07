using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Child class of DamageableCharacter
// Main script for player game object

public class Player : DamageableCharacter
{
    
    Vector2 movementInput;// This is the input vector, a unit vector representing the direction of the input (-1 <= x <= 1; -1 <= y <= 1)
    [SerializeField]
    float walkingSpeed = 3f;
    [SerializeField]
    float runningSpeed = 5f;
    [SerializeField]
    float maxMovementSpeed = 20f;
    [SerializeField]
    float dashForce = 100f;
    bool movementLocked = false;

    public float maxStamina = 100f;
    public float _currentStamina;
    public float staminaRecoverySpeed = 0.5f;
    public float dashStaminaCost = 30f;
    public float runningStaminaCost = 2f;
    public float lowestStaminaToRun = 10f; // The lowest amount of stamina in order for the character to run (to prevent infinite running)
    public bool recoveringFromZeroStamina = false;


    public float currentStamina
    {
        set
        {
            _currentStamina = value;
            if (_currentStamina > maxStamina)
            {
                _currentStamina = maxStamina;
            }
            else if (_currentStamina < 0)
            {
                _currentStamina = 0;
            }
        }
        get
        {
            return _currentStamina;
        }
    }


    public float maxMana = 100f;
    public float _currentMana;

    public float currentMana
    {
        set
        {
            _currentMana = value;
            if (_currentMana > maxMana)
            {
                _currentMana = maxMana;
            }
            else if (_currentMana < 0)
            {
                _currentMana = 0;
            }
        }
        get
        {
            return _currentMana;
        }
    }


    public float invulnerableTimeWindow = 0.7f;
    float invulnerableTimeElapsed = 0f;
    bool isInvulnerable = false;

    public Vector3 mouseDirection; // Mouse direction reletive to player (tail at player, tip at mouse)
    public bool isFacingRight;

    bool isRunningPressed = false;
    bool isDashingPressed = false;

    Animator animator;
    Rigidbody2D body;
    SpriteRenderer spriteRenderer;
    Collider2D playerCollider;
    public GameObject currentItem;

    Inventory inventory;

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


        InitializeHealth();
        currentStamina = maxStamina;
        currentMana = maxMana;
    }


    //====================================================================
    // Update is called every frame (To get player input)
    //====================================================================
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            isRunningPressed = true;
        }

        // Switch item when TAB is pressed
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            inventory.ChangeItem();
        }


        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            isDashingPressed = true;
        }
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

        CheckInvulnerability();

        CalculateMouseDirection();

        RecoverStamina();


        // Check if movement is locked
        if (!movementLocked)
        {
            // Only move the player if input vector is not 0 
            if (movementInput != Vector2.zero)
            {
                // Start moving animation
                animator.SetBool("isMoving", true);

                // If SHIFT is pressed (player is running), add force scaled by running speed
                if (isRunningPressed && !recoveringFromZeroStamina)
                {
                    body.AddForce(movementInput * runningSpeed);
                    currentStamina -= runningStaminaCost;
                    animator.speed = 1.7f;
                }
                // If not, add force scaled by walking speed
                else
                {
                    body.AddForce(movementInput * walkingSpeed);
                    animator.speed = 1;
                }


                // If SPACE was pressed during movement input, dash in the direction of movement input
                if (isDashingPressed && currentStamina >= dashStaminaCost)
                {
                    body.AddForce(movementInput * dashForce);
                    currentStamina -= dashStaminaCost;
                }

                // Restrain the velocity
                body.velocity = Vector3.ClampMagnitude(body.velocity, maxMovementSpeed);

            }


            // If SPACE was pressed during NO movement input, dash in the direction of mouse
            else if (isDashingPressed && currentStamina >= dashStaminaCost)
            {
                body.AddForce(mouseDirection * dashForce);
                body.velocity = Vector3.ClampMagnitude(body.velocity, maxMovementSpeed);
                currentStamina -= dashStaminaCost;
            }


            // If movement is NOT locked, but there's no input
            else
            {
                // Make transition from walking to idle animation
                animator.SetBool("isMoving", false);
                
            }
        }


        FlipXAccordingTo(mouseDirection);
        ResetPressedValues();
    }


    //====================================================================
    // Input Functions
    //====================================================================

    // Update the input vector everytime there is an input
    void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }

    // Trigger attack animation
    void OnFire()
    {
        if (currentItem != null)
        {
            if (currentItem.tag == "Weapon")
            {
                StartAttack();
                animator.SetTrigger("swordAttack");
            }
        }
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

    void CheckInvulnerability()
    {
        if (isInvulnerable)
        {
            invulnerableTimeElapsed += Time.fixedDeltaTime;
        }

        if (invulnerableTimeElapsed > invulnerableTimeWindow)
        {
            isInvulnerable = false;
            invulnerableTimeElapsed = 0f;
        }
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

    void RecoverStamina()
    {
        if (currentStamina == 0)
        {
            recoveringFromZeroStamina = true;
        }

        currentStamina += staminaRecoverySpeed;

        if (currentStamina > lowestStaminaToRun)
        {
            recoveringFromZeroStamina = false;
        }
    }

    void ResetPressedValues()
    {
        isRunningPressed = false;
        isDashingPressed = false;
    }

    // Player was hit
    public override void OnHit(float damage, Vector3 knockBack)
    {
        // Check invunerability before applying hit
        if (!isInvulnerable)
        {
            base.OnHit(damage, knockBack);
            body.AddForce(knockBack);
            animator.SetTrigger("isHit");
            isInvulnerable = true;
        }
    }

    // Lock the movement when attacking
    public void StartAttack()
    {
        LockMovement();
        currentItem.GetComponent<Weapon>().Attack();
    }

    void LockMovement()
    {
        movementLocked = true;
    }

    void UnlockMovement()
    {
        movementLocked = false;
    }

    public void StartDeath()
    {
        animator.SetTrigger("dead");
        playerCollider.enabled = false;
        LockMovement();
    }


}
