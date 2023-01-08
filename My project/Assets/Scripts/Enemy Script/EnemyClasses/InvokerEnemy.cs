using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokerEnemy : DamageableCharacter {
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public float attackDistance;
    public float startTimeBtwClone1;
    public float startTimeBtwClone2;
    public float startTimeBtwClone3;
    private float timeBtwShots;
    private float timeBtwClone1;
    private float timeBtwClone2;
    private float timeBtwClone3;
    public float startTimeBtwShots;
    public bool isFacingRight;
    public GameObject projectile; 
    private Transform player;
    public Animator animator;
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;
    public GameObject clone1;
    public GameObject clone2;
    public GameObject clone3;


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
        timeBtwClone1 = 10;
        timeBtwClone2 = 15;
        timeBtwClone3 = 20;


        
    }

    // Update is called once per frame
    void Update()
    {
       
        // Check if dead
        if (!isAlive)
        {
            StartDeath();
        }

        
       
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

        

        if (Vector2.Distance(transform.position, player.position) <= attackDistance)
        {
            if (timeBtwShots <= 0)
            {
                animator.SetTrigger("attack");
                IEnumerator Attack()
                {
                    yield return new WaitForSeconds (1f);
                    Instantiate(projectile, transform.position, Quaternion.identity);
                }
                
                StartCoroutine(Attack());
                timeBtwShots = startTimeBtwShots;
            } else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }

        if (Vector2.Distance(transform.position, player.position) <= attackDistance)
        {
            if (timeBtwClone1 <= 0)
            {
                animator.SetTrigger("attack");
                IEnumerator Clone1()
                {
                    yield return new WaitForSeconds (1f);
                    Instantiate(clone1, transform.position, Quaternion.identity);
                    Instantiate(clone1, transform.position, Quaternion.identity);
                    Instantiate(clone1, transform.position, Quaternion.identity);
                }
                StartCoroutine(Clone1());
                timeBtwClone1 = startTimeBtwClone1;
            } else
            {
                timeBtwClone1 -= Time.deltaTime;
            }
        }
        
        if (Vector2.Distance(transform.position, player.position) <= attackDistance)
        {
            if (timeBtwClone2 <= 0)
            {
                animator.SetTrigger("attack");
                IEnumerator Clone2()
                {
                    yield return new WaitForSeconds (1f);
                    Instantiate(clone2, transform.position, Quaternion.identity);
                    Instantiate(clone2, transform.position, Quaternion.identity);
                    Instantiate(clone2, transform.position, Quaternion.identity);
                }
                StartCoroutine(Clone2());
                timeBtwClone2 = startTimeBtwClone2;
            } else
            {
                timeBtwClone2 -= Time.deltaTime;
            }
        }

        if (Vector2.Distance(transform.position, player.position) <= attackDistance)
        {
            if (timeBtwClone3 <= 0)
            {
                animator.SetTrigger("attack");
                IEnumerator Clone3()
                {
                    yield return new WaitForSeconds (1f);
                    Instantiate(clone3, transform.position, Quaternion.identity);
                    Instantiate(clone3, transform.position, Quaternion.identity);
                    Instantiate(clone3, transform.position, Quaternion.identity);
                }
                StartCoroutine(Clone3());
                timeBtwClone3 = startTimeBtwClone3;
            } else
            {
                timeBtwClone3 -= Time.deltaTime;
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