using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    public int value = 1;
    // Start is called before the first frame update
    Player player;
    Rigidbody2D body;

    [SerializeField]
    float pickupRange = 1f;
    [SerializeField]
    float pullForce = 100f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position); // Distance to the player

        if (dist <= pickupRange)
        {
            Vector2 dir = Vector3.Normalize(player.transform.position - transform.position) * pullForce;
            body.AddForce(dir);
        }

        // Add item to player's inventory
        if (dist <= 0.1f)
        {
            player.inventory.gold += value;
            Destroy(gameObject);
        }
    }
}
