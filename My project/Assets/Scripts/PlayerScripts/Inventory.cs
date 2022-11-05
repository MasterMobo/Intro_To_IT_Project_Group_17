using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Player player;
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

    public void ChangeItem()
    {
        Destroy(selectedItem);
        selectedItemIndex++;

        if (selectedItemIndex > inventory.Count - 1)
        {
            selectedItemIndex = 0;
        }

        if (inventory[selectedItemIndex] != null)
        {
            selectedItem = Instantiate(inventory[selectedItemIndex], player.transform.position, Quaternion.identity);
            selectedItem.transform.parent = player.transform;
        }
    }
}


