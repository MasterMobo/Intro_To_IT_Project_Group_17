using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : DamageableCharacter {
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public float shootDistance;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public bool isFacingRight;
    public GameObject projectile; 
    private Transform player;
    public Animator animator;
    public Rigidbody2D body;

    public SpriteRenderer spriteRenderer;


    public override void OnHit(float damage, Vector3 knockBack)
    {
        base.OnHit(damage, knockBack);
        body.AddForce(knockBack);
        animator.SetTrigger("isHit");
    }

    // Function for checking if enemy is facing left or right
    public void checkFlipDirection()
    {
        // If facing left, flip x-axis of sprite
        if (body.velocity.x < 0)
        {
            spriteRenderer.flipX = true;

        };
        // If facing right, don't flip x-axis of sprite
        if (body.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        };

        // Set the isFacingRight variable accordingly after checking
        isFacingRight = !spriteRenderer.flipX;
    }

    public void StartDeath()
    {
        // Start death animation
        animator.SetTrigger("dead");
    }

    // End death process (Usually an event in the death animation)
    public void EndDeath()
    {
        Destroy(gameObject);
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;


        
    }

    // Update is called once per frame
    void Update()
    {
       
        // Check if dead
        if (!isAlive)
        {
            StartDeath();
        }
        checkFlipDirection();
        
        // If the velocity is not zero, start move animation
        animator.SetBool("isMoving", body.velocity != Vector2.zero);
      
        
       
       if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            MoveTowards(player.position);
        } else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;

        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            MoveBackwards(player.position);
        }

        
        if (Vector2.Distance(transform.position, player.position) <= shootDistance)
        {
            if (timeBtwShots <= 0)
            {
                animator.SetTrigger("attack");
                IEnumerator Attack()
                {
                    yield return new WaitForSeconds (0.5f);
                    Instantiate(projectile, transform.position, Quaternion.identity);
                }
                
                StartCoroutine(Attack());
                timeBtwShots = startTimeBtwShots;
            } else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
        
        checkFlipDirection();
    }
    public void MoveTowards(Vector3 target)
    {
        // Calculate direction from object to target
        Vector2 direction = target - transform.position;

        body.AddForce(direction * speed * Time.fixedDeltaTime);
    }
    public void MoveBackwards(Vector3 target)
    {
        // Calculate direction from object to target
        Vector2 direction = target - transform.position;

        body.AddForce(direction * -speed * Time.fixedDeltaTime);
    }
}