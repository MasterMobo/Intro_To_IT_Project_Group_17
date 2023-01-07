using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoisonEnemy : MonoBehaviour {

   public float speed;
   public float damage;
   public float projectileKnockbackForce;
   public GameObject hitFX;
   public GameObject paddle;
   private Transform player;
   private Vector2 target;
   


   void SpawnHitEffects()
    {
        GameObject newhitFX = Instantiate(hitFX, transform.position, Quaternion.identity);
        Destroy(newhitFX);
    }
   
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);

    }

    // Update is called once per frame
    void Update()
    {
       transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

       if (transform.position.x == target.x && transform.position.y == target.y) 
       {
            SpawnHitEffects();
            Instantiate(paddle, transform.position, Quaternion.identity);
            Destroy(gameObject);
       } 
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector2 dir = other.transform.position - gameObject.GetComponentInParent<Transform>().position;
            Vector2 knockback = Vector3.Normalize(dir) * projectileKnockbackForce;
            other.GetComponent<Player>().OnHit(damage,knockback);
            SpawnHitEffects();
            Instantiate(paddle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void DestroyProjectile() 
    {
        Destroy(gameObject);
    }



}
