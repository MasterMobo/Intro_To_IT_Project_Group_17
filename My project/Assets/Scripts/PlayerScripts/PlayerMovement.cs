using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles all the movement (walking, running, dashing) 
public class PlayerMovement : MonoBehaviour
{
    public Player player;

    public float walkingSpeed = 3f;
    public float runningSpeed = 5f;
    public float maxMovementSpeed = 20f;
    public float dashForce = 100f;
    public float dashTimeWindow = 0.015f;
    public float dashTimeElapsed = 0f;
    public bool movementLocked = false;

    // Start is called before the first frame update


    public void LockMovement()
    {
        movementLocked = true;
    }

    public void UnlockMovement()
    {
        movementLocked = false;
    }



}
