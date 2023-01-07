using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Player player;
    public int size = 4;
    public List<GameObject> inventory = new List<GameObject>();
    

    GameObject selectedItem;
    int selectedItemIndex = 0;

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

    public void ChangeItem(int i)
    {
        if (i >= size)
        {
            return;
        }
        Destroy(selectedItem);
/*        selectedItemIndex++;

        if (selectedItemIndex > inventory.Count - 1)
        {
            selectedItemIndex = 0;
        }*/

        if (inventory[i] != null)
        {
            selectedItem = Instantiate(inventory[i], player.transform.position, Quaternion.identity);
            selectedItem.transform.parent = player.transform;
            selectedItemIndex = i;
        }
    }
}


