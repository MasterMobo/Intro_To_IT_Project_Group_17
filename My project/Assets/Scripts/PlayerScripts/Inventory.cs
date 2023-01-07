using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Player player;
    public int size = 4;
    public List<GameObject> inventory = new List<GameObject>();
    

    GameObject selectedItem;
    public int selectedItemIndex = 0;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (inventory[selectedItemIndex] != null)
        {
            selectedItem = Instantiate(inventory[selectedItemIndex], transform.position, Quaternion.identity);
            selectedItem.transform.parent = transform;
        }
    }

    private void Update()
    {
        player.currentItem = selectedItem;
    }

    public bool IsAvailable()
    {
        for (int i = 0; i < size; i++)
        {
            if (inventory[i] == null)
            {
                return true;
            }
        }
        return false;
    }

    public void AddItem(GameObject item)
    {
        for (int i = 0; i < size; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                return;
            }
        }
    }

    public void RemoveCurrent()
    {
        inventory[selectedItemIndex] = null;
    }

    public void NextItem()
    {
        Destroy(selectedItem);
        selectedItemIndex++;

        if (selectedItemIndex >= size)
        {
            selectedItemIndex = 0;
        }

        if (inventory[selectedItemIndex] != null)
        {
            selectedItem = Instantiate(inventory[selectedItemIndex], player.transform.position, Quaternion.identity);
            selectedItem.transform.parent = player.transform;
        }
    }

    public void PreviousItem()
    {
        Destroy(selectedItem);
        selectedItemIndex--;

        if (selectedItemIndex < 0)
        {
            selectedItemIndex = size - 1;
        }

        if (inventory[selectedItemIndex] != null)
        {
            selectedItem = Instantiate(inventory[selectedItemIndex], player.transform.position, Quaternion.identity);
            selectedItem.transform.parent = player.transform;
        }
    }

    public void ChangeItem(int i)
    {
        if (i >= size)
        {
            return;
        }
        Destroy(selectedItem);

        if (inventory[i] != null)
        {
            selectedItem = Instantiate(inventory[i], player.transform.position, Quaternion.identity);
            selectedItem.transform.parent = player.transform;  
        }

        selectedItemIndex = i;
    }

    
}


