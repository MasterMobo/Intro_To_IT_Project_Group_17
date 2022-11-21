using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    Player player;
    Rigidbody2D body;

    [SerializeField]
    float pickupRange = 0.7f;
    [SerializeField]
    float pullForce = 100f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        body = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position); // Distance to the player

        // Pull the item towards the player
        if (dist <= pickupRange && transform.parent == null && player.inventory.IsAvailable())
        {
            Vector2 dir = Vector3.Normalize(player.transform.position - transform.position) * pullForce;
            body.AddForce(dir);
        }

        // Add item to player's inventory
        if (dist <= 0.1f && transform.parent == null && player.inventory.IsAvailable())
        {
            transform.parent = player.transform;
            player.inventory.AddItem(Instantiate(gameObject));
            Destroy(gameObject);
        }

        // Update the item position
        if (transform.parent != null)
        {
            transform.position = player.transform.position;
        }
    }
}
