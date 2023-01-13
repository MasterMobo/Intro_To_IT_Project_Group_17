using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a child of game object parent
// This checks for objects nearby the enemy

public class DetectionZone : MonoBehaviour
{
    public CircleCollider2D detectionZoneCollider;
    public List<Collider2D> detected; // List of all detected objects
    Enemy parent; // DONT KNOW IF THIS SHOULD BE ONLY ENEMY? COULD BE TOWER TOO? WILL FIGURE OUT LATTER

    void Start()
    {
        parent = GetComponentInParent<Enemy>();

        // Set the radius of the zone collider to the parent's aggroDistance
        detectionZoneCollider.radius = parent.aggroDistance;
    }

    // If an object (player or tower) enters the zone, add it to the detected list
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Tower") {
            detected.Add(other);
         }
    }

    // If an object exits the zone, remove it from the detected list
    private void OnTriggerExit2D(Collider2D other)
    {
        detected.Remove(other);
    }
}
