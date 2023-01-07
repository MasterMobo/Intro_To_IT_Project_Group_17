using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This script handles all player input

public class PlayerInput : MonoBehaviour
{
    public Player player;

    public Vector2 movementInput;// This is the input vector, a unit vector representing the direction of the input (-1 <= x <= 1; -1 <= y <= 1)

    public bool isRunningPressed = false;
    public bool isDashingPressed = false;

    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };


    //====================================================================
    // Update is called every frame (To get player input)
    //====================================================================
    private void Update()
    {
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                player.inventory.ChangeItem(i);
            }
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            player.inventory.PreviousItem();
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            player.inventory.NextItem();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunningPressed = true;
        }

        // Switch item when TAB is pressed
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            
        }


        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
                isDashingPressed = true;
           
        }
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
        if (player.currentItem != null)
        {
            if (player.currentItem.tag == "Weapon")
            {
                player.StartAttack();
                player.animator.SetTrigger("swordAttack");
            }
            else if (player.currentItem.tag == "Consumable")
            {
                player.currentItem.GetComponent<Consumable>().Use();
            }
        }
    }



    public void ResetPressedValues()
    {
        isRunningPressed = false;
            isDashingPressed = false;


    }
}
