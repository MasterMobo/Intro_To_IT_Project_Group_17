using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;
    public int index;
    Image image;
    void Start()
    {
        image = GetComponent<Image>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        image.preserveAspect = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.inventory.inventory[index] != null)
        {
            image.sprite = player.inventory.inventory[index].GetComponent<SpriteRenderer>().sprite;
        }
    }
}
